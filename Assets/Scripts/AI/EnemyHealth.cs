using UnityEngine;
using UnityEngine.Events;

namespace MobileFPS.AI
{
    /// <summary>
    /// Vida do inimigo e evento de morte para conceder XP.
    /// </summary>
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 60f;
        [SerializeField] private int xpReward = 20;

        public UnityEvent<int> OnEnemyKilled;

        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            OnEnemyKilled?.Invoke(xpReward);
            Destroy(gameObject);
        }
    }
}
