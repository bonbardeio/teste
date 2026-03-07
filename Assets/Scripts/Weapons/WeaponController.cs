using System.Collections;
using MobileFPS.AI;
using UnityEngine;
using UnityEngine.Events;

namespace MobileFPS.Weapons
{
    /// <summary>
    /// Controle individual da arma: tiro, recarga, recuo e efeitos.
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponData data;
        [SerializeField] private Camera fpsCamera;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform muzzlePoint;

        public UnityEvent<int, int> OnAmmoChanged;

        public WeaponData Data => data;
        public bool IsReloading { get; private set; }

        private float nextFireTime;
        private int currentMagazine;
        private int currentReserve;

        private void Awake()
        {
            currentMagazine = data.magazineSize;
            currentReserve = data.reserveAmmo;
            OnAmmoChanged?.Invoke(currentMagazine, currentReserve);
        }

        public bool TryShoot(bool aiming)
        {
            if (IsReloading || Time.time < nextFireTime)
                return false;

            if (currentMagazine <= 0)
            {
                return false;
            }

            currentMagazine--;
            nextFireTime = Time.time + (1f / data.fireRate);
            OnAmmoChanged?.Invoke(currentMagazine, currentReserve);

            PlayShotFeedback();
            FireRaycasts(aiming);
            ApplyRecoil();
            return true;
        }

        public bool TryReload(MonoBehaviour runner)
        {
            if (IsReloading || currentMagazine >= data.magazineSize)
                return false;

            if (!data.infiniteReserveAmmo && currentReserve <= 0)
                return false;

            runner.StartCoroutine(ReloadRoutine());
            return true;
        }

        private IEnumerator ReloadRoutine()
        {
            IsReloading = true;
            if (data.reloadSound != null)
            {
                audioSource.PlayOneShot(data.reloadSound);
            }

            yield return new WaitForSeconds(data.reloadTime);

            int needed = data.magazineSize - currentMagazine;
            if (data.infiniteReserveAmmo)
            {
                currentMagazine = data.magazineSize;
            }
            else
            {
                int toLoad = Mathf.Min(needed, currentReserve);
                currentMagazine += toLoad;
                currentReserve -= toLoad;
            }

            IsReloading = false;
            OnAmmoChanged?.Invoke(currentMagazine, currentReserve);
        }

        private void FireRaycasts(bool aiming)
        {
            for (int i = 0; i < data.pellets; i++)
            {
                Vector3 direction = GetSpreadDirection(aiming);
                if (!Physics.Raycast(fpsCamera.transform.position, direction, out RaycastHit hit, data.range))
                    continue;

                if (hit.collider.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage(data.damage);
                }

                if (data.impactPrefab != null)
                {
                    Instantiate(data.impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }

        private Vector3 GetSpreadDirection(bool aiming)
        {
            float spread = aiming ? data.adsSpread : data.hipSpread;
            Vector3 forward = fpsCamera.transform.forward;
            forward += fpsCamera.transform.right * Random.Range(-spread, spread);
            forward += fpsCamera.transform.up * Random.Range(-spread, spread);
            return forward.normalized;
        }

        private void PlayShotFeedback()
        {
            if (data.shotSound != null)
            {
                audioSource.PlayOneShot(data.shotSound);
            }

            if (data.muzzlePrefab != null && muzzlePoint != null)
            {
                Instantiate(data.muzzlePrefab, muzzlePoint.position, muzzlePoint.rotation);
            }
        }

        private void ApplyRecoil()
        {
            // Recuo simples deslocando a arma para trás no eixo local.
            transform.localPosition += Vector3.back * (data.recoil * 0.01f);
        }
    }
}
