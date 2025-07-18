using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaellaController : MonoBehaviour
{
    public static PaellaController Instance { get; private set; }
    public bool paellaEnPreparacion;
    public GameObject paella, botonDesechar, botonEnviar;
    private GameObject paellaObj;

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }

    public void paellaLibre()
    {
        paellaEnPreparacion = false;
    }

    public void paellaOcupada()
    {
        paellaEnPreparacion = true;
    }

    public void PrepararPaella()
    {
        if (paellaEnPreparacion == false)
        {
            paellaObj = Instantiate(paella, transform.position, Quaternion.identity);
            paellaOcupada();
        }

    }

    public void PaellaLista()
    {
        botonDesechar.SetActive(true);
        botonEnviar.SetActive(true);
    }

    public void Enviar()
    {
        Debug.Log("Paella Enviada");
        paellaLibre();
        Destroy(paellaObj);
        botonDesechar.SetActive(false);
        botonEnviar.SetActive(false);
    }

    public void Desechar()
    {
        Debug.Log("Paella Desechada");
        paellaLibre();
        Destroy(paellaObj);
        botonDesechar.SetActive(false);
        botonEnviar.SetActive(false);
    }
}
