using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaellaLogic : MonoBehaviour
{
    public Transform destino;
    public float velocidad = 5f;
    private bool enMovimiento = true;

    [SerializeField] public float radio = 3f;
    public Vector2 posicionColocada => transform.position;
    private List<GameObject> objetosColocados = new List<GameObject>();


    void Update()
    {
        if (enMovimiento)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

            if (transform.position == destino.position)
            {
                enMovimiento = false;
                PaellaController.Instance.PaellaLista();
            }
        }
    }

    public bool EstaDentro(Vector2 punto)
    {
        return Vector2.Distance(punto, transform.position) <= radio;
    }

    public void AnadirObjeto(GameObject obj)
    {
        if (!objetosColocados.Contains(obj))
        {
            objetosColocados.Add(obj);
        }
    }

    public void QuitarObjeto(GameObject obj)
    {
        objetosColocados.Remove(obj);
    }

    public void Limpiar()
    {
        foreach (var obj in objetosColocados)
        {
            Destroy(obj);
        }
        objetosColocados.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

}
