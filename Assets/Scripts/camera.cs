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
     
        float yPos = Mathf.Clamp(posY, -1.9f, 40.0f);
        float xPos = Mathf.Clamp(posX, -8.0f, 68.0f);
        transform.position = new Vector3(xPos, yPos, transform.position.z);

    }
}