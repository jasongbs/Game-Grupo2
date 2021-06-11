using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pecaQuadrada : MonoBehaviour
{
    private Vector2 InitPos;
    private Vector2 FinalPos;
    private int way = 1;//0 - Desce e 1- Sobe
   
    public int direcao = 0;//0 - Desce e 1- Sobe

    [SerializeField] Vector2 DestPos;

    void Start()
    {
        if (direcao == 1)
            way = 1;
        else
            way = 0;
    }

    void Update()
    {
        DestPos = new Vector2(0, 0.5f);
        InitPos = transform.position;
        float PosY = transform.position.y;

        if ( PosY >= -1.3f)
        {
            way = 0;
        }
        else if (PosY <= -3.4f)
        {
            way = 1;
        }

        if (way == 1)
        {
            FinalPos = InitPos + DestPos * Time.deltaTime;
        }
        else if(way == 0)
        {
           
            FinalPos = InitPos - DestPos * Time.deltaTime;
        }
        transform.position = FinalPos;
    }
}