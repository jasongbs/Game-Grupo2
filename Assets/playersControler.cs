using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersControler : MonoBehaviour
{
    public float speedForce = 12.0f;
    public float jumpForce = 5.0f;

    public bool IsGrounded = true;
    public Vector3 offset = Vector2.zero;
    public float raidios = 10.0f;
    public LayerMask layer;
    public Vector2 bonecoPos;

    private Vector2 _input = Vector2.zero;
    private Vector2 _direction = Vector2.zero;
    public Vector2 _moviment = Vector2.zero;
    private Rigidbody2D _body = null;
    private SpriteRenderer _renderer = null;
    

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        _moviment = new Vector2(Input.GetAxisRaw("Horizontal") * speedForce, 0.0f);

        if (_moviment.sqrMagnitude > 0.1f)
        {
            _renderer.flipX = !(Input.GetAxis("Horizontal") > 0.0f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    
    //fazer um delay na camera. 
     /*private void LateUpdate()
     {
       transform.Translate(0, Time.deltaTime, 0);
     }*/
    

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(this.transform.position + offset, raidios, layer);

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
    /*
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
