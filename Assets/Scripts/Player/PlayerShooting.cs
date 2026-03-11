using UnityEngine;

namespace JarvisGameTemplate.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Tiro")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float projectileSpeed = 30f;
        [SerializeField] private float fireRate = 0.2f;

        private float nextFireTime;

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }

        private void Shoot()
        {
            if (projectilePrefab == null || shootPoint == null)
            {
                Debug.LogWarning("Configure projectilePrefab e shootPoint no Inspector.");
                return;
            }

            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = shootPoint.forward * projectileSpeed;
            }
        }
    }
}
