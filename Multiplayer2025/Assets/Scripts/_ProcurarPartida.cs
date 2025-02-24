using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _ProcurarPartida : MonoBehaviour
{
    public void ProcurarPartida(int indice)
    {
        SceneManager.LoadScene(indice);        
    }
}
