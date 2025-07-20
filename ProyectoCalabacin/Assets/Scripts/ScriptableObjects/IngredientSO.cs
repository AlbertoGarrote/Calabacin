using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 1)]
    public class IngredientSO : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        public int Status; // 0: Raw, 1: Cooked, 2: Burnt
        public bool HasToBeCooked; // Indicates if the ingredient needs to be cooked
    }
}