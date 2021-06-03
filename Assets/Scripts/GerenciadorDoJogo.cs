using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GerenciadorDoJogo : MonoBehaviour
{

    public static GerenciadorDoJogo gm;

    private int vidas=3;
    
    void Awake()
    {
        if(gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
       /* else
        {
            Destroy(gameObject);
        }*/ 
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
        SceneManager.LoadScene("PrimaryScene");

    }

    public void ReinciaPartida()
    {
        setVidas(3);
        SceneManager.LoadScene("PrimaryScene");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
