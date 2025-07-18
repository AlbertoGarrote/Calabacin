using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour
{
    [SerializeField] GameObject ingredient;

    private void OnMouseDown()
    {
        GameObject newIngredient = Instantiate(ingredient, transform.position, Quaternion.identity);
        newIngredient.GetComponent<Draggeable>().IniciarArrastre();
    }
}
