using System.Collections.Generic;
using Components.KitchenComponents.SnapableArea;
using Game.Audio;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Components.KitchenComponents
{
    public class PaellaContainer : ASnapableArea
    {
        public int Id {get; set;}
        public int DeliveryId {get; set;} // Delivery ID for the Paella
        public List<Ingredient> IngredientsInPaella {get; private set;}
        
        private void Awake()
        {
            IngredientsInPaella = new List<Ingredient>();
        }
        
        public override bool CanAcceptIngredient(Draggeable draggeable)
        {
            if (draggeable.CompareTag("Ingredient"))
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
            draggeable.transform.SetParent(transform);
            
            Ingredient ingredient = draggeable.gameObject.GetComponent<Ingredient>();
            IngredientsInPaella.Add(ingredient);

            var audioOptions = AudioOptionsBuilder.BuildCommon2DAudio(false, "");
            
            var soundManager = ServiceLocator.Instance.GetService<SoundManager>();
            soundManager.PlaySoundOn(this.gameObject, "TuercaSFX", audioOptions);
            
            draggeable.enabled = false; // Disable dragging after snapping
            draggeable.GetComponent<CircleCollider2D>().enabled = false; // Disable collider to prevent further interactions
            
        }
    }
}