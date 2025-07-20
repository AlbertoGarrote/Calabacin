using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Orders
{
    public class OrderRowUI : MonoBehaviour
    {
        
        public Image ingredientImage;
        public TMP_Text ingredientNameText;
        public TMP_Text ingredientCant;

        public void SetImage(Sprite img)
        {
            ingredientImage.sprite = img;
        }
        
        public void SetName(string name)
        {
            ingredientNameText.text = name;
        }
        
        public void SetCant(int cant)
        {
            ingredientCant.text = cant.ToString();
        }
        
    }
}