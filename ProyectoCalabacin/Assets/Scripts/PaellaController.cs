using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaellaController : MonoBehaviour
{
    public static PaellaController Instance { get; private set; }
    public bool paellaEnPreparacion, paellaLista = false;
    public GameObject paella, botonDesechar, botonEnviar;
    private GameObject paellaObj;
    private bool correctCam = true;

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

    public void CorrectCamTrue()
    {
        correctCam = true;
    }
    public void CorrectCamFalse()
    {
        correctCam = false;
    }

    public void PaellaLista()
    {
        paellaLista = true;
    }

    public void Enviar()
    {
        Debug.Log("Paella Enviada");
        paellaLibre();
        Destroy(paellaObj);
        paellaLista = false;
    }

    public void Desechar()
    {
        Debug.Log("Paella Desechada");
        paellaLibre();
        Destroy(paellaObj);
        paellaLista = false;
    }

    private void Update()
    {
        if(paellaLista == true && correctCam == true)
        {
            botonDesechar.SetActive(true);
            botonEnviar.SetActive(true);
        }
        else
        {
            botonDesechar.SetActive(false);
            botonEnviar.SetActive(false);
        }
    }
}
