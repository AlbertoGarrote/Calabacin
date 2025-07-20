namespace Components.KitchenComponents.SnapableArea
{
    public interface ISnapZone
    {
        bool CanAcceptIngredient(Draggeable ingredient);
        void SnapIngredient(Draggeable ingredient);
    }
}