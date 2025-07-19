using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject Creditos;
    [SerializeField] private GameObject Instrucciones;
    [SerializeField] private GameObject UI;
    [SerializeField] private AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        Creditos.SetActive(false);
        Instrucciones.SetActive(false);
        UI.SetActive(false);
    }

    // Update is called once per frame
    public void BotonJugar() 
    {
        Debug.Log("A paellear");
        MainMenu.SetActive(false);
        UI.SetActive(true);
    }
    public void BotonOpciones()
    {
        Debug.Log("Opciones");
        OptionsMenu.SetActive(true);
    }
    public void BotonInstrucciones()
    {
        Debug.Log("A leer");
        Instrucciones.SetActive(true);
    }
    public void BotonCreditos()
    {
        Debug.Log("L' equip");
        Creditos.SetActive(true);
    }
    public void BotonSalir()
    {
        Debug.Log("Salir del mariscon");
        Application.Quit();
    }


    // Volver
    public void VolverOpciones()
    {
        OptionsMenu.SetActive(false);
    }
    public void VolverCreditos()
    {
        Creditos.SetActive(false);
    }
    public void VolverInstrucciones()
    {
        Instrucciones.SetActive(false);
    }
}
