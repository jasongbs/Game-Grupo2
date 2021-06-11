using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDoJogo : MonoBehaviour
{

    public static GerenciadorDoJogo gm;

    public int vidas=0;

    void Awake()
    {
        if(gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int getVidas()
    {
        return vidas;
    }

    public void setVidas(int vida)
    {
       
        Debug.Log("Removendo Vidas");
        vidas += vida;
        
    }

    public void iniciaPartida()
    {
        Debug.Log("Criando Scena");
        Application.LoadLevel("PrimaryScene");
    }


    public void partidaConcluida()
    {
        Debug.Log("Criando Scena");
        Application.LoadLevel("Final de Fase");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
  
}
