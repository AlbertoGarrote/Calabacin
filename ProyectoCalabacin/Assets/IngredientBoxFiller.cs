using System;
using System.Collections;
using System.Collections.Generic;
using Components.Clients;
using Components.Orders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBoxFiller : MonoBehaviour
{
    [SerializeField] private OrderRowUI orderRowUI;

    public Client Client;
    
    public void FillBox(Client client)
    {
        
        foreach (var ingredient in client.ClientOrder.Ingredients)
        {
            orderRowUI = Instantiate(orderRowUI, transform);
            orderRowUI.SetImage(ingredient.Key.Sprite);
            orderRowUI.SetName(ingredient.Key.Name);
            orderRowUI.SetCant(ingredient.Value);
        }
        
    }

}
