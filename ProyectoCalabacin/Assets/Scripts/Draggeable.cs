using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggeable : MonoBehaviour
{
    Vector2 diferencia = Vector2.zero;
    private bool arrastrandoExternamente = false;

    private void OnMouseDown()
    {
        diferencia = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - diferencia;
    }
    public void IniciarArrastre()
    {
        diferencia = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        arrastrandoExternamente = true;
    }

    private void Update()
    {
        if (arrastrandoExternamente && Input.GetMouseButton(0))
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - diferencia;
        }

        if (arrastrandoExternamente && Input.GetMouseButtonUp(0))
        {
            arrastrandoExternamente = false;

            Collider2D colPropio = GetComponent<Collider2D>();
            colPropio.enabled = false;

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("DropZoneTag"))
            {
                DropZone zona = hit.collider.GetComponent<DropZone>();
                if (zona != null && zona.EstaDentro(mouseWorldPos))
                {
                    Vector2 offset = Random.insideUnitCircle * 0.75f;
                    transform.position = (Vector2)zona.transform.position + offset;

                    zona.AñadirObjeto(gameObject);
                    return;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
