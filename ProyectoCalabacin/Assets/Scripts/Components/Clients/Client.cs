using System.Linq;
using Components.KitchenComponents;
using Components.Orders;
using ScriptableObjects;
using UnityEngine;

namespace Components.Clients
{
    public class Client : MonoBehaviour
    {
        public int Id { get; set; }
        public Order ClientOrder { get; private set; }
        public bool IsWaitingForOrder { get; private set; }

        public Client(int id)
        {
            Id = id;
            ClientOrder = OrderManager.Instance.CreateOrder();
            IsWaitingForOrder = true;
        }
        
        private void Start()
        {
            // Initialize the client with an order
            ClientOrder = OrderManager.Instance.CreateOrder();
            IsWaitingForOrder = true;
            GetComponentInChildren<IngredientBoxFiller>().FillBox(this);
        }

        public void PlaceOrder()
        {
            // Logic to place an order
            IsWaitingForOrder = false;
        }

        public void ReceiveOrder(PaellaContainer paella)
        {
            if(CheckIfCorrectOrder(paella))
            {
                if (CheckIfIngredientIsRaw(paella))
                {
                    CancelOrder();
                }
                else
                {
                    if (CheckIfIngredientIsBurnt(paella))
                    {
                        CurrencyManager.Instance.AddCurrency(paella.IngredientsInPaella.Count * 3); 
                    }
                    else
                    {
                        CurrencyManager.Instance.AddCurrency(paella.IngredientsInPaella.Count * 10); 
                    }
                }
            }
            else
            {
                CancelOrder();
            }
            ClientManager.Instance.ReleaseSpawnPoint(Id);
            Destroy(this.gameObject);
        }

        private bool CheckIfCorrectOrder(PaellaContainer paella)
        {
            var contadorLista = paella.IngredientsInPaella.GroupBy(x => x.IngredientData).ToDictionary(g => g.Key, g => g.Count());

            return contadorLista.Count == ClientOrder.Ingredients.Count &&
                   contadorLista.All(kv => ClientOrder.Ingredients.ContainsKey(kv.Key) && ClientOrder.Ingredients[kv.Key] == kv.Value);
            
        }
        
        private bool CheckIfIngredientIsRaw(PaellaContainer paella)
        {
            // Check if any ingredient with HasToBeCooked property in the order is raw (status 0)
            return paella.IngredientsInPaella.Any(ingredient => ingredient.CurrentStatus == 0 && ingredient.IngredientData.HasToBeCooked);
        }
        
        private bool CheckIfIngredientIsBurnt(PaellaContainer paella)
        {
            return paella.IngredientsInPaella.Any(ingredient => ingredient.CurrentStatus == 2 && ingredient.IngredientData.HasToBeCooked);
        }

        public void CancelOrder()
        {
            LifeManager.Instance.LoseLife(1);
        }
    }
}