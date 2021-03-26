using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pecaQuadrada : MonoBehaviour
{
    private Vector2 InitPos;
    [SerializeField] Vector2 DestPos;
    private Vector2 FinalPos;
    private int way = 1;//0 - Desce e 1- Sobe
    
    

    void Update()
    {
        DestPos = new Vector2(0, 0.5f);
        InitPos = transform.position;
        float PosY = transform.position.y;

        if ( PosY >= 0.5f)
        {
            way = 0;
        }
        else if (PosY <= -3)
        {
            way = 1;
        }

        if (way == 1)
        {
            Debug.Log("Subindo!");
            FinalPos = InitPos + DestPos * Time.deltaTime;
        }
        else if(way == 0)
        {
           
            FinalPos = InitPos - DestPos * Time.deltaTime;
        }
        transform.position = FinalPos;
        Debug.Log(FinalPos);
    }
}

/*

public class pecaQuadrada : MonoBehaviour
{
    public float speedForce = 12.0f;
    public float jumpForce = 5.0f;
    public float sentido = 10.0f;
    public float posiAtual = 0;


    public bool IsGrounded = true;
    public Vector3 offset = Vector2.zero;
    public float raidios = 10.0f;
    public LayerMask layer;

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
        _moviment = new Vector2(Input.GetAxisRaw("Vertical") * speedForce, 0.0f);

        if (_moviment.sqrMagnitude > 0.1f)
        {
            _renderer.flipX = !(Input.GetAxis("Vertical") > 0.0f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(this.transform.position + offset, raidios, layer);

        if (_moviment.sqrMagnitude > 0.1f)
        {
            _body.AddForce(_moviment, ForceMode2D.Force);
            Debug.Log("Aplicou for�a");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((this.transform.position + offset), raidios);
    }
}
*/