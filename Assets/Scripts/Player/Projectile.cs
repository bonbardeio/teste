using UnityEngine;
using JarvisGameTemplate.Enemies;

namespace JarvisGameTemplate.Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 3f;
        [SerializeField] private int damage = 10;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
