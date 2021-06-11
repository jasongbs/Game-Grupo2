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
      vidas += vida; 
    }

    public void iniciaPartida()
    {
        Application.LoadLevel("PrimaryScene");
    }

    public void partidaConcluida()
    {
        Application.LoadLevel("Final de Fase");
    }
  
}
