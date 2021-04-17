using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    /* public float speedForce = 15.0f;


     public Vector3 offset = Vector2.zero;
     public float raidios = 10.0f;
     public Vector2 bonecoPos;

     private Vector2 _input = Vector2.zero;
     private Vector2 _direction = Vector2.zero;
     public Vector2 _moviment = Vector2.zero;
     private Rigidbody2D _body = null;
     private SpriteRenderer _renderer = null;
    */
    public Animator animator;
    public bool taNoChao = true;
    public bool taNaAgua;
    public Transform detectaChao;
    public Transform detectaAgua;
    public LayerMask oQueEhChao;
    public LayerMask oQueEhAgua;
    public float jumpForce = 8.0f;

    public Rigidbody2D rb;
    public int movespeed;
    private float direction;
    private Vector3 facingRight;
    private Vector3 facingLeft;

    public int pulosExtras = 1;

    /*
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }*/
    //
    // Start is called before the first frame update
    void Start()
    {
        //_body = GetComponent<Rigidbody2D>();
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * movespeed, rb.velocity.y);

        if(direction > 0)
        {
            //olhando para a direita
            transform.localScale = facingRight;
        }
        if (direction < 0)
        {
            //olhando para a esquerda
            transform.localScale = facingLeft;
        }

        /*
        
        taNoChao = Physics2D.OverlapCircle(this.transform.position + offset, raidios, oQueEhChao);
        _moviment = new Vector2(Input.GetAxisRaw("Horizontal") * speedForce, 0.0f);

        if (_moviment.sqrMagnitude > 0.1f)
        {
            _renderer.flipX = !(Input.GetAxis("Horizontal") > 0.0f);
        }
        */

        taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhChao);
        taNaAgua = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhAgua);

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


        if (taNaAgua)
        {
            var posicao_inicial = new Vector3(-20.117f, -1.264f, 0);
            rb.transform.position = posicao_inicial;
            Debug.Log("Morreu");
        }

        if(Input.GetAxis("Horizontal") != 0)
        {
            //esta correndo
            animator.SetBool("taCorrendo", true);
        }
        else
        {
            //esta parado
            animator.SetBool("taCorrendo", false);
        }
    }
    /*
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Inimigo"))
        {
            var posicao_inicial = new Vector3(-20.117f, -1.264f, 0);
            _body.transform.position = posicao_inicial;
            Debug.Log("Morreu");
        }
    }

    
    private void FixedUpdate()
    {
        

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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AiController aiController = collision.gameObject.GetComponent<AiController>();

        if (aiController)
        {
            currentHitPoint -= aiController.damage;
            Vector2 direction = collision.gameObject.transform.position.normalized;
            direction.y = 0f;
            m_body2d.velocity = Vector2.zero;
            m_body2d.AddForce(direction * 150, ForceMode2D.Impulse);
        }
    }*/
}
