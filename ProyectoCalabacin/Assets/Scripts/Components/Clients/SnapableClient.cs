using Components.KitchenComponents;
using Components.KitchenComponents.SnapableArea;

namespace Components.Clients
{
    public class SnapableClient : ASnapableArea
    {
        public override bool CanAcceptIngredient(Draggeable draggeable)
        {
            if (draggeable.CompareTag("Paella") && base.CanAcceptIngredient(draggeable))
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
            
            GetComponent<Client>().ReceiveOrder(draggeable.GetComponent<PaellaContainer>());
            Destroy(draggeable.gameObject);
        }
    }
}