using System.Collections;
using System.Collections.Generic;
using Components;
using ScriptableObjects;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour
{
    [SerializeField] GameObject ingredient;
    [SerializeField] IngredientSO ingredientData;

    private void OnMouseDown()
    {
        GameObject newIngredient = Instantiate(ingredient, transform.position, Quaternion.identity);
        newIngredient.GetComponent<Ingredient>().Initialize(ingredientData);
        newIngredient.GetComponent<Draggeable>().Ingredient = newIngredient.GetComponent<Ingredient>();
        newIngredient.GetComponent<Draggeable>().IniciarArrastre();
    }
}
