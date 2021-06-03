using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ReinciaPartida()
    {

        GerenciadorDoJogo.gm.setVidas(3);
        Application.LoadLevel("PrimaryScene");

    }
}
