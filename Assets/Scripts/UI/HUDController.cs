using MobileFPS.Player;
using MobileFPS.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace MobileFPS.UI
{
    /// <summary>
    /// HUD: vida, munição e minimapa.
    /// O minimapa usa uma câmera top-down renderizando em RawImage.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private WeaponManager weaponManager;

        [Header("UI")]
        [SerializeField] private Slider healthBar;
        [SerializeField] private Text ammoText;
        [SerializeField] private RawImage minimapImage;

        private void Start()
        {
            playerHealth.OnHealthChanged.AddListener(UpdateHealth);
            if (weaponManager.CurrentWeapon != null)
                weaponManager.CurrentWeapon.OnAmmoChanged.AddListener(UpdateAmmo);

            // Mantém referência para o minimapa explícita na cena.
            minimapImage.enabled = minimapImage.texture != null;
        }

        private void UpdateHealth(float current, float max)
        {
            healthBar.value = current / max;
        }

        private void UpdateAmmo(int mag, int reserve)
        {
            ammoText.text = $"{mag} / {reserve}";
        }
    }
}
