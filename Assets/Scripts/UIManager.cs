using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void ReinciaPartida()
    {

        GerenciadorDoJogo.gm.setVidas(3);
        Application.LoadLevel("PrimaryScene");
    }

    public void voltarMenu()
    {
        Debug.Log("Criando Scena");
        Application.LoadLevel("Inicio");
    }


    public void mostraComoJogar()
    {
        Debug.Log("Criando Scena");
        Application.LoadLevel("Inicio - ComoJogar");

    }

    

}
