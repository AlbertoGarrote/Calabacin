using System;
using Components.KitchenComponents.SnapableArea;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.KitchenComponents
{
    public class FurnaceManager : ASnapableArea
    {
        [SerializeField] private float CookTime = 5; 
        [SerializeField] private float BurnTime = 10; 
        
        private Ingredient _currentIngredient;
        private bool _furnaceIsCooking;
        private float _cookingTime;
        
        public void CookIngredient(Ingredient ingredient)
        {
            _currentIngredient = ingredient;
            _furnaceIsCooking = true;
            switch (_currentIngredient.CurrentStatus)
            {
                case 0: 
                    _cookingTime = 0;
                    break;
                case 1: 
                    _cookingTime = 5;
                    break;
                case 2: 
                    _cookingTime = 10;
                    break;
            }
        }

        public void OnMouseDown()
        {
            if(_currentIngredient != null)
            {
                if (_furnaceIsCooking)
                {
                    _currentIngredient.GetComponent<Draggeable>().IniciarArrastre();
                    _furnaceIsCooking = false;
                    _currentIngredient = null;
                    _cookingTime = 0;
                }
                return;
            }
        }

        private void Update()
        {
            if(_furnaceIsCooking)
            {
                _cookingTime += Time.deltaTime;
                if (BurnTime >= _cookingTime && _cookingTime >= CookTime && _currentIngredient.CurrentStatus != 1) 
                {
                    _currentIngredient.Cook();
                } 
                else if (_cookingTime >= BurnTime && _currentIngredient.CurrentStatus != 2)
                {
                    _currentIngredient.Burn();
                }
            }
        }
        
        public override bool CanAcceptIngredient(Draggeable draggeable)
        {
            if (draggeable.CompareTag("Ingredient") && base.CanAcceptIngredient(draggeable))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void SnapIngredient(Draggeable draggeable)
        {
            base.SnapIngredient(draggeable);

            draggeable.transform.position = transform.position;
            Ingredient ingredient = draggeable.gameObject.GetComponent<Ingredient>();
            CookIngredient(ingredient);
            
        }
        
        public override void UnsnapIngredient(Draggeable draggeable)
        {
            base.UnsnapIngredient(draggeable);
            if (_currentIngredient != null && _currentIngredient == draggeable.Ingredient)
            {
                _furnaceIsCooking = false;
                _currentIngredient = null;
                _cookingTime = 0;
            }
        }
    }
}