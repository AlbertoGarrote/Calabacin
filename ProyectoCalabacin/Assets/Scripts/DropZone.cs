using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] public float radio = 3f;
    public Vector2 posicionColocada => transform.position;

    private List<GameObject> objetosColocados = new List<GameObject>();

    public bool EstaDentro(Vector2 punto)
    {
        return Vector2.Distance(punto, transform.position) <= radio;
    }

    public void AñadirObjeto(GameObject obj)
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

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Limpiar();
        }
    }
}
