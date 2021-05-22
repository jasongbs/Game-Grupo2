using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public float deltaY;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothTimeY = 0.2f;
        smoothTimeX = 0.2f;
        deltaY = 0.4f;
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + deltaY, ref velocity.y, smoothTimeY);
        if (player.transform.position.x < -5.2d)
        {
            transform.position = new Vector3(-5.2f, -1.9f, transform.position.z);
        }else if (player.transform.position.x > 65f)
        {
            transform.position = new Vector3(65f, -1.9f, transform.position.z);
        }else 
        {
            transform.position = new Vector3(posX, -1.9f, transform.position.z);
        }


    }
}