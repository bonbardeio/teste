using UnityEngine;
using UnityEngine.Events;

namespace MobileFPS.Player
{
    /// <summary>
    /// Sistema de vida do jogador.
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;

        public UnityEvent<float, float> OnHealthChanged;
        public UnityEvent OnPlayerDied;

        public float CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = maxHealth;
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Max(0f, CurrentHealth - damage);
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

            if (CurrentHealth <= 0f)
            {
                OnPlayerDied?.Invoke();
            }
        }

        public void HealFull()
        {
            CurrentHealth = maxHealth;
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        }
    }
}
