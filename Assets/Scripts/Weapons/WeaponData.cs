using UnityEngine;

namespace MobileFPS.Weapons
{
    /// <summary>
    /// Dados de balanceamento para cada arma.
    /// Crie assets via Create > MobileFPS > Weapon Data.
    /// </summary>
    [CreateAssetMenu(menuName = "MobileFPS/Weapon Data", fileName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public string weaponId;
        public string displayName;

        [Header("Dano")]
        public float damage = 15f;
        public float fireRate = 6f;
        public float range = 60f;
        public float recoil = 1.2f;

        [Header("Munição")]
        public int magazineSize = 12;
        public bool infiniteReserveAmmo;
        public int reserveAmmo = 120;
        public float reloadTime = 1.4f;

        [Header("Dispersão")]
        public float hipSpread = 0.03f;
        public float adsSpread = 0.01f;
        public int pellets = 1;

        [Header("Feedback")]
        public AudioClip shotSound;
        public AudioClip reloadSound;
        public ParticleSystem muzzlePrefab;
        public ParticleSystem impactPrefab;
    }
}
