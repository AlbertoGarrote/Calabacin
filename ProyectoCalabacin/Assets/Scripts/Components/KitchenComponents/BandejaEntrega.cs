using Components.KitchenComponents.SnapableArea;
using UnityEngine;

namespace Components.KitchenComponents
{
    public class BandejaEntrega : ASnapableArea
    {
        public Transform deliveryPoint;
        public float transitionSpeed = 1f;
        private bool isAnimating = false;
        
        public override bool CanAcceptIngredient(Draggeable ingredient)
        {
            if(!ingredient.CompareTag("Ingredient") && base.CanAcceptIngredient(ingredient))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void SnapIngredient(Draggeable ingredient)
        {
            base.SnapIngredient(ingredient);

            ingredient.transform.position = transform.position;
            Camera.main.gameObject.GetComponent<Cam>().CambiarCamara(0);
            isAnimating = true;
            
        }
        
        void LateUpdate()
        {
            if(_currentIngredient == null || !isAnimating) return;
            if(_currentIngredient.transform.position == deliveryPoint.position)
            { 
                _currentIngredient.transform.SetParent(deliveryPoint);
                _currentIngredient.GetComponent<Draggeable>().PreviousPosition = deliveryPoint.position;
                _currentIngredient = null;
                return;
            }
            _currentIngredient.transform.position = Vector3.Lerp(_currentIngredient.transform.position, deliveryPoint.position, Time.deltaTime * transitionSpeed);
        }
    }
}