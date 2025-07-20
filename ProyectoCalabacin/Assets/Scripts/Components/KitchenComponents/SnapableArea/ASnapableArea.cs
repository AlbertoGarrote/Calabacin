using UnityEngine;

namespace Components.KitchenComponents.SnapableArea
{
    public abstract class ASnapableArea : MonoBehaviour, ISnapZone
    {
        protected Draggeable _currentIngredient;
        public virtual bool CanAcceptIngredient(Draggeable ingredient)
        {
            if (_currentIngredient != null)
            {
                return false;
            }

            return true;
        }

        public virtual void SnapIngredient(Draggeable ingredient)
        {
            
            if(!CanAcceptIngredient(ingredient)) return;
            _currentIngredient = ingredient;
        }
        
        public virtual void UnsnapIngredient(Draggeable ingredient)
        {
            if (_currentIngredient == ingredient)
            {
                _currentIngredient = null;
            }
        }
    }
}