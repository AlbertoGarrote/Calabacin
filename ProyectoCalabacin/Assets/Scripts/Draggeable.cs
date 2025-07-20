using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Components.KitchenComponents;
using Components.KitchenComponents.SnapableArea;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggeable : MonoBehaviour
{
    Vector2 diferencia = Vector2.zero;
    private bool arrastrandoExternamente = false;
    public int tipo;

    public ASnapableArea CurrentSnapableArea;
    
    public Vector2 PreviousPosition { get; set; }
    
    public Ingredient Ingredient { get; set; }

    private void Awake()
    {
        if (tag.Equals("Ingredient")) 
            PreviousPosition = Vector2.zero;
        else
            PreviousPosition = transform.position;
    }

    private void OnMouseDown()
    {
        IniciarArrastre();
    }

    private void OnMouseDrag()
    {
        if(this.enabled)
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

            if (hit.collider != null && hit.collider.gameObject != gameObject 
                && hit.collider.gameObject.TryGetComponent<ASnapableArea>(out ASnapableArea snapZone) 
                && snapZone.CanAcceptIngredient(this))
            {
                ASnapableArea zona = hit.collider.GetComponent<ASnapableArea>();
                
                PreviousPosition = transform.position; // Save the position where it was snapped
                colPropio.enabled = true;
                if(CurrentSnapableArea != null)
                    CurrentSnapableArea.UnsnapIngredient(this);
                CurrentSnapableArea = zona;
                zona.SnapIngredient(this);
            }
            else
            {
                if(PreviousPosition == Vector2.zero && tag.Equals("Ingredient"))
                    Destroy(gameObject);
                else 
                {
                    transform.position = PreviousPosition;
                    colPropio.enabled = true;
                }
            }
        }
    }

}
