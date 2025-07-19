using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntregaController : MonoBehaviour
{
    public static EntregaController Instance { get; private set; }

    public TextMeshProUGUI textoResultado;
    private List<int> listaAleatoria = new List<int>();


    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        GenerarListaAleatoria();
        MostrarListaEnTexto();
    }

    void GenerarListaAleatoria()
    {
        listaAleatoria.Clear(); 

        int cantidadIngredientes = Random.Range(1, 5);

        for(int i = 0; i <= cantidadIngredientes; i++) 
        { 
            listaAleatoria.Add(Random.Range(1, 3));
        }
    }

    void MostrarListaEnTexto()
    {
        string texto = "Lista generada: ";
        foreach (int tipo in listaAleatoria)
        {
            texto += tipo + " ";
        }

        textoResultado.text = texto;
    }

    void VerificarConteoTipos(List<int> paellaIngredientes)
    {
        Dictionary<int, int> conteoGenerado = ContarTipos(listaAleatoria);
        Dictionary<int, int> conteoPaella = ContarTipos(paellaIngredientes);

        bool correcto = true;

        for (int tipo = 1; tipo <= 3; tipo++)
        {
            int valorGenerado = conteoGenerado.ContainsKey(tipo) ? conteoGenerado[tipo] : 0;
            int valorPaella = conteoPaella.ContainsKey(tipo) ? conteoPaella[tipo] : 0;

            if (valorGenerado != valorPaella)
            {
                correcto = false;
                break;
            }
        }

        if (correcto)
        {
            Debug.Log("Correcto");
        }
        else
        {
            Debug.Log("Incorrecto");
        }
    }

    Dictionary<int, int> ContarTipos(List<int> lista)
    {
        Dictionary<int, int> conteo = new Dictionary<int, int>();

        foreach (int tipo in lista)
        {
            if (!conteo.ContainsKey(tipo))
                conteo[tipo] = 0;

            conteo[tipo]++;
        }

        return conteo;
    }

    public void EntregarFinal(List<int> paellaIngredientes)
    {
        VerificarConteoTipos(paellaIngredientes);
        GenerarListaAleatoria();
        MostrarListaEnTexto();
    }
}
