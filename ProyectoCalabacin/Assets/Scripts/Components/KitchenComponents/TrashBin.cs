using Components.KitchenComponents.SnapableArea;
using UnityEngine;

namespace Components.KitchenComponents
{
    public class TrashBin : ASnapableArea
    {
        public override bool CanAcceptIngredient(Draggeable draggeable)
        {
            return true;
        }

        public override void SnapIngredient(Draggeable draggeable)
        {
            base.SnapIngredient(draggeable);

            if (draggeable.CompareTag("Paella"))
            {
                PaellaManager.Instance.RemovePaella(draggeable.GetComponent<PaellaContainer>());
            }
            
            Destroy(draggeable.gameObject);
            // Disable collider to prevent further interactions
            
        }
    }
}