using Components.KitchenComponents.SnapableArea;
using UnityEngine;

namespace Components.KitchenComponents
{
    public class BandejaEntrega : ASnapableArea
    {
        public float transitionSpeed = 1f;
        private bool isAnimating = false;
        private Vector3 deliveryPoint;
        
        public override bool CanAcceptIngredient(Draggeable ingredient)
        {
            if(!ingredient.CompareTag("Ingredient") && base.CanAcceptIngredient(ingredient) && ServiceManager.Instance.CanBeDelivered())
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
            int deliveryId = 0;
            deliveryPoint = ServiceManager.Instance.GetDeliveryPoint(ref deliveryId);
            ingredient.GetComponent<PaellaContainer>().DeliveryId = deliveryId;

            PaellaManager.Instance.CreatePaella(ingredient.GetComponent<PaellaContainer>().Id);
            ingredient.transform.position = transform.position;
            isAnimating = true;
            
        }
        
        void LateUpdate()
        {
            if(_currentIngredient == null || !isAnimating) return;
            if(_currentIngredient.transform.position == deliveryPoint)
            { 
                _currentIngredient.GetComponent<Draggeable>().PreviousPosition = deliveryPoint;
                _currentIngredient = null;
                return;
            }
            _currentIngredient.transform.position = Vector3.Lerp(_currentIngredient.transform.position, deliveryPoint, Time.deltaTime * transitionSpeed);
        }
    }
}