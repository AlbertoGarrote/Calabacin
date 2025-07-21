using Patterns.Singleton;
using TMPro;
using UnityEngine;

namespace Components
{
    public class LifeManager : SingletonBehaviour<LifeManager>
    {
        public GameObject gameOverPanel;
        public TMP_Text gameOverText;
        public int CurrentLife { get; private set; }
        public int MaxLife { get; private set; } = 3;
        public TMP_Text lifeText;
        private void Start()
        {
            CurrentLife = MaxLife; // Initialize life to maximum at the start
            lifeText.text = $"x {CurrentLife}";
        }
        
        public void LoseLife(int amount)
        {
            CurrentLife -= amount;
            if (CurrentLife <= 0)
            {
                gameOverText.text = $"Puntuación: {CurrencyManager.Instance.CurrentCurrency}";
                gameOverPanel.SetActive(true);
            }
            lifeText.text = $"x {CurrentLife}";
        }
    }
}