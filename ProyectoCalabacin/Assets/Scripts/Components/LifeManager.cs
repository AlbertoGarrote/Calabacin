using Patterns.Singleton;
using TMPro;
using UnityEngine;

namespace Components
{
    public class LifeManager : SingletonBehaviour<LifeManager>
    {
        public int CurrentLife { get; private set; }
        public int MaxLife { get; private set; } = 3;
        public TMP_Text lifeText;
        private void Start()
        {
            CurrentLife = MaxLife; // Initialize life to maximum at the start
            lifeText.text = $"Lifes: {CurrentLife}";
        }
        
        public void LoseLife(int amount)
        {
            CurrentLife -= amount;
            if (CurrentLife < 0)
            {
                CurrentLife = 0; // Ensure life does not go below zero
            }
            lifeText.text = $"Lifes: {CurrentLife}";
        }
    }
}