using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaellaLogic : MonoBehaviour
{
    public Transform destino, destinoNormal, destinoBarra;
    public float velocidad = 5f;
    public bool enMovimiento = true;

    [SerializeField] public float radio = 3f;
    public Vector2 posicionColocada => transform.position;
    public List<int> tipoObjetosColocados = new List<int>();

    private void Awake()
    {
        destino = destinoNormal;
    }

    void Update()
    {
        if (enMovimiento)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

            if (transform.position == destino.position)
            {
                enMovimiento = false;
                PaellaController.Instance.PaellaLista();
                if (destino == destinoBarra)
                {
                    destino = destinoNormal;
                }
                else
                {
                    destino = destinoBarra;
                }
            }
        }
    }

    public bool EstaDentro(Vector2 punto)
    {
        return Vector2.Distance(punto, transform.position) <= radio;
    }

    public void AñadirObjeto(GameObject obj)
    {
        if (!tipoObjetosColocados.Contains(obj.GetComponent<Draggeable>().tipo))
        {
            tipoObjetosColocados.Add(obj.GetComponent<Draggeable>().tipo);
        }
    }

    public void QuitarObjeto(GameObject obj)
    {
        tipoObjetosColocados.Remove(obj.GetComponent<Draggeable>().tipo);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

}
