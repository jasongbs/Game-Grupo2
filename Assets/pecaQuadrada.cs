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