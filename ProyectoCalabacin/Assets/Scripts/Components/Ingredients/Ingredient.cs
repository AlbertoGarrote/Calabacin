using System;
using ScriptableObjects;
using UnityEngine;

namespace Components
{
    public class Ingredient : MonoBehaviour
    {
        public int CurrentStatus { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private  set; }
        
        private SpriteRenderer _spriteRenderer;
        
        public IngredientSO IngredientData { get; private set; }
        
        public void Initialize(IngredientSO ingredientData)
        {
            CurrentStatus = ingredientData.Status;
            Name = ingredientData.Name;
            Sprite = ingredientData.Sprite;
            IngredientData = ingredientData;
            
            _spriteRenderer.sprite = Sprite;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Cook()
        {
            if (CurrentStatus < 1)
            {
                CurrentStatus = 1;
                _spriteRenderer.color = new Color(0.8f, 0.5f, 0f, 1f); // Change color to indicate cooking
            }
        }
        
        public void Burn()
        {
            if (CurrentStatus < 2)
            {
                CurrentStatus = 2; 
                _spriteRenderer.color = new Color(0.15f, 0.12f, 0f, 1f); // Change color to indicate cooking
            }
        }
    }
}