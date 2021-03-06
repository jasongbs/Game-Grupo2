using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float speedForce = 15.0f;
    public float jumpForce = 8.0f;
    public Vector3 offset = Vector2.zero;
    public float raidios = 2.0f;
    public Vector2 bonecoPos;
    public Vector2 _moviment = Vector2.zero;
    public Animator animator;
    public bool taNoChao = true;
    public bool taNaAgua;
    public Transform detectaChao;
    public Transform detectaAgua;
    public LayerMask oQueEhChao;
    public LayerMask oQueEhAgua;
    public Rigidbody2D rb;
    public Transform FimDeJogo;
    public Transform VidaUm;
    public Transform VidaDois;
    public Transform VidaTres;
    public int pulosExtras = 1;
    public int pisca2;
    public int totalmoedas= 1;
    public Text pontuacaoText;

    private Vector2 _input = Vector2.zero;
    private Vector2 _direction = Vector2.zero;
    private SpriteRenderer _renderer = null;
    private float direction;
    private int pisca;
    private int moedas = 0;

    [SerializeField]
    private bool isDeath = false;


    private void Awake()
    {
        pontuacaoText.text = moedas.ToString() + " / " + totalmoedas.ToString();
        FimDeJogo.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        if (GerenciadorDoJogo.gm.getVidas() == 2)
        {
            VidaUm.gameObject.SetActive(false);
        }
        else if (GerenciadorDoJogo.gm.getVidas() == 1)
        {
            VidaUm.gameObject.SetActive(false);
            VidaDois.gameObject.SetActive(false);
        }
        else if (GerenciadorDoJogo.gm.getVidas() == 0)
        {
            VidaUm.gameObject.SetActive(false);
            VidaDois.gameObject.SetActive(false);
            VidaTres.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        animator = GetComponent <Animator>();
    }

    void Update()
    {
        if (!isDeath)
        {
            direction = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(direction * speedForce, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && taNoChao == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                //ativar anima??o do pulo
                animator.SetBool("taPulando", true);
            }

            if (Input.GetButtonDown("Jump") && taNoChao == false && pulosExtras > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                pulosExtras--;
                //ativar anima??o do pulo duplo
                animator.SetBool("puloDuplo", true);
            }
            if (taNoChao)
            {
                pulosExtras = 1;
                animator.SetBool("taPulando", false);
                animator.SetBool("puloDuplo", false);
            }

            taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhChao);
            taNaAgua = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhAgua);

            if (taNaAgua)
            {
                StartCoroutine(Death());
                StartCoroutine(takeDamageEffect());
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                animator.SetBool("taCorrendo", true);
            }
            else
            {
                animator.SetBool("taCorrendo", false);
            }

            _moviment = new Vector2(Input.GetAxisRaw("Horizontal") * speedForce, 0.0f);

            if (_moviment.sqrMagnitude > 0.1f)
            {
                _renderer.flipX = !(Input.GetAxis("Horizontal") > 0.0f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Inimigo"))
        {
            StartCoroutine(Death());
            StartCoroutine(takeDamageEffect());
        }

        if (collider.CompareTag("Moeda"))
        {
            moedas = moedas + 1;
            Destroy(collider.gameObject);
            pontuacaoText.text = moedas.ToString() + " / " + totalmoedas.ToString();
        }

        if (collider.CompareTag("Bau"))
        {
            Animator animatorDoBau;
            animatorDoBau = collider.GetComponent<Animator>();
            if (moedas == 10)
            {
               animatorDoBau.SetBool("PodeAbrir", true);
                StartCoroutine(Finish());
            }        
        }
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.0f);
        GerenciadorDoJogo.gm.partidaConcluida();
    }

    IEnumerator Death()
    {
        isDeath = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.0f);

        if (GerenciadorDoJogo.gm.getVidas() <= 1)
        {
            FimDeJogo.gameObject.SetActive(true);
        }
        else
        {
            GerenciadorDoJogo.gm.setVidas(-1);
            GerenciadorDoJogo.gm.iniciaPartida();
        }

    }

    //logica para anima??o de morte
    public float effectTime = 0.1f;
    IEnumerator takeDamageEffect()
    {
        pisca = 0;
        while (pisca <= pisca2)
        {
            pisca++;
            float deltaTime = 0;
            while (deltaTime <= effectTime)
            {
                deltaTime += Time.deltaTime;
                _renderer.color = Color.Lerp(Color.white, Color.black, deltaTime / effectTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            deltaTime = 0;
            while (deltaTime <= effectTime)
            {
                deltaTime += Time.deltaTime;
                _renderer.color = Color.Lerp(Color.black, Color.white, deltaTime / effectTime);
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FixedUpdate()
    {
        taNoChao = Physics2D.OverlapCircle(this.transform.position + offset, raidios, oQueEhChao);
        if (_moviment.sqrMagnitude > 0.1f)
        {
            rb.AddForce(_moviment, ForceMode2D.Force);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((this.transform.position + offset), raidios);
    }
}
