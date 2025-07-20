using System.Collections.Generic;
using Patterns.Singleton;
using ScriptableObjects;
using UnityEngine;

namespace Components.Orders
{
    public class OrderManager : SingletonBehaviour<OrderManager>
    {
        [SerializeField] private List<IngredientSO> PosibleIngredients;
        [SerializeField] private int MaxIngredientsOnPaella = 5;
        [SerializeField] private int MaxIngredientsOfSameType = 2;
        
        public Order CreateOrder()
        {
            Dictionary<IngredientSO, int> ingredients = new Dictionary<IngredientSO, int>();
            int ingredientCount = Random.Range(1, MaxIngredientsOnPaella + 1);
            for (int i = 0; i < ingredientCount; i++)
            {
                IngredientSO ingredient = GetRandomIngredient();
                if (ingredient != null)
                {
                    if (ingredients.ContainsKey(ingredient))
                    {
                        ingredients[ingredient]++;
                    }
                    else
                    {
                        ingredients[ingredient] = 1;
                    }
                }
            }
            return new Order(ingredients);
        }

        private IngredientSO GetRandomIngredient()
        {
            if (PosibleIngredients.Count == 0) return null;

            IngredientSO randomIngredient = PosibleIngredients[Random.Range(0, PosibleIngredients.Count)];
            int ingredientCount = 0;

            foreach (var ingredient in PosibleIngredients)
            {
                if (ingredient.Name == randomIngredient.Name)
                {
                    ingredientCount++;
                }
            }

            if (ingredientCount <= MaxIngredientsOfSameType)
            {
                return randomIngredient;
            }
            else
            {
                return GetRandomIngredient(); 
            }
        }
    }
}