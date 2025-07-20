using System.Collections.Generic;
using ScriptableObjects;

namespace Components.Orders
{
    public class Order
    {
        public Dictionary<IngredientSO, int> Ingredients { get; private set; }

        public Order(Dictionary<IngredientSO, int> ingredients)
        {
            Ingredients = ingredients;
        }

        public bool IsComplete()
        {
            // Logic to determine if the order is complete
            return Ingredients.Count > 0; // Placeholder logic
        }
        
        public void AddIngredient(IngredientSO ingredient)
        {
            if (Ingredients.ContainsKey(ingredient))
            {
                Ingredients[ingredient]++;
            }
        }
        
        public void RemoveIngredient(IngredientSO ingredient)
        {
            if (Ingredients.ContainsKey(ingredient))
            {
                Ingredients[ingredient]--;
                if (Ingredients[ingredient] <= 0)
                {
                    Ingredients.Remove(ingredient);
                }
            }
        }
    }
}