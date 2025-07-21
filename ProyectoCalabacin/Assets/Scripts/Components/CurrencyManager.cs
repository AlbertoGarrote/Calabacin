using System;
using Patterns.Singleton;
using TMPro;
using UnityEngine;

namespace Components
{
    public class CurrencyManager : SingletonBehaviour<CurrencyManager>
    {
        public TMP_Text currencyText;
        public int CurrentCurrency { get; private set; }

        private void Start()
        {
            CurrentCurrency = 0; // Initialize currency to zero at the start
            currencyText.text = $"{CurrentCurrency}";
        }
        
        public void AddCurrency(int amount)
        {
            CurrentCurrency += amount;
            currencyText.text = $"{CurrentCurrency}";
        }
    }
}