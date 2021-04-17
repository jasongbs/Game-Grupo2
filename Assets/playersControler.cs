using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersControler : MonoBehaviour
{
    public float    speedForce = 15.0f;
    public float    jumpForce = 8.0f;
    public Vector3  offset = Vector2.zero;
    public float    raidios = 10.0f;
    public Vector2  bonecoPos;
    public Vector2  _moviment = Vector2.zero;
    public Animator animator;
    public bool     taNoChao = true;
    public bool     taNaAgua;
    public Transform detectaChao;
    public Transform detectaAgua;
    public LayerMask oQueEhChao;
    public LayerMask oQueEhAgua;
    public Rigidbody2D rb;
    public int      movespeed;
    public int      pulosExtras = 1;

    private Vector2     _input = Vector2.zero;
    private Vector2     _direction = Vector2.zero;
    private Rigidbody2D _body = null;
    private SpriteRenderer _renderer = null;
    private float       direction;
    private Vector3     facingRight;
    private Vector3     facingLeft;

    [SerializeField]
    private bool isDeath = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
 
    // Start is called before the first frame update
    void Start()
    {
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            direction = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(direction * movespeed, rb.velocity.y);
            /*
            if (direction > 0)
            {
                //olhando para a direita
                transform.localScale = facingRight;
            }
            if (direction < 0)
            {
                //olhando para a esquerda
                transform.localScale = facingLeft;
            }
            */
            if (Input.GetButtonDown("Jump") && taNoChao == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                //ativar animação do pulo
                animator.SetBool("taPulando", true);
            }

            if (Input.GetButtonDown("Jump") && taNoChao == false && pulosExtras > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                pulosExtras--;
                //ativar animação do pulo duplo
                animator.SetBool("puloDuplo", true);
            }
            if (taNoChao && rb.velocity.y == 0)
            {
                pulosExtras = 1;
                animator.SetBool("taPulando", false);
                animator.SetBool("puloDuplo", false);
            }

            taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhChao);
            taNaAgua = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhAgua);

            if (taNaAgua)
            {
                Debug.Log("Morreu");
                StartCoroutine(Death());
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                //esta correndo
                animator.SetBool("taCorrendo", true);
            }
            else
            {
                //esta parado
                animator.SetBool("taCorrendo", false);
            }

            _moviment = new Vector2(Input.GetAxisRaw("Horizontal") * speedForce, 0.0f);

            if (_moviment.sqrMagnitude > 0.1f)
            {
                _renderer.flipX = !(Input.GetAxis("Horizontal") > 0.0f);
            }
            //----------------------------------------------------------------------------------------------------
            /*
            _moviment = new Vector2(Input.GetAxisRaw("Horizontal") * speedForce, 0.0f);

            if (_moviment.sqrMagnitude > 0.1f)
            {
                _renderer.flipX = !(Input.GetAxis("Horizontal") > 0.0f);
            }

            if (Input.GetButtonDown("Jump") && taNoChao == true)
            {
                _body.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetButtonDown("Jump") && taNoChao == false && pulosExtras > 0)
            {
                _body.velocity = Vector2.up * jumpForce;
                pulosExtras--;
            }
            if (taNoChao)
            {
                pulosExtras = 1;
            }

            if (taNaAgua)
            {
                var posicao_inicial = new Vector3(-20.117f, -1.264f, 0);
                _body.transform.position = posicao_inicial;
                Debug.Log("Morreu");
            }
            */
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Inimigo"))
        {
            //var posicao_inicial = new Vector3(-20.117f, -1.264f, 0);
            //_body.transform.position = posicao_inicial;
            Debug.Log("Morreu");
            StartCoroutine(Death());
            //StartCoroutine(takeDamageEffect());
        }
    }

    IEnumerator Death()
    {
        isDeath = true;
        _body.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(Application.loadedLevel);
    }

    //public float effectTime = 0.3f;
    //IEnumerator takeDamageEffect()
    //{
    //    float deltaTime = 0;
    //    whilhe(deltaTime <= effectTime)
    //    {
    //        deltaTime += Time.deltaTime;
    //        _renderer.color = Color.lerp(Color.white, Color.black, deltaTime / effectTime);
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.1f);
    //    deltaTime = 0;
	   // whilhe(deltaTime <= effectTime)
    //    {
    //        deltaTime += Time.deltaTime;
    //        _renderer.color = Color.lerp(Color.black, Color.white, deltaTime / effectTime);
    //        yield return null;
    //    }
    //}

private void FixedUpdate()
    {
        taNoChao = Physics2D.OverlapCircle(this.transform.position + offset, raidios, oQueEhChao);

        if (_moviment.sqrMagnitude > 0.1f)
        {
            _body.AddForce(_moviment, ForceMode2D.Force);
            Debug.Log("Aplicou força");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((this.transform.position + offset), raidios);
    }
}
