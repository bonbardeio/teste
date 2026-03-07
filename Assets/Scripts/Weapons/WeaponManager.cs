using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MobileFPS.Weapons
{
    /// <summary>
    /// Gerencia troca de armas e integração com botões de UI.
    /// </summary>
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private List<WeaponController> weapons;
        [SerializeField] private int startingIndex;

        public UnityEvent<string> OnWeaponChanged;

        private int currentIndex;

        public WeaponController CurrentWeapon => weapons.Count == 0 ? null : weapons[currentIndex];

        private void Start()
        {
            Equip(startingIndex);
        }

        public void Equip(int index)
        {
            if (index < 0 || index >= weapons.Count)
                return;

            currentIndex = index;
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(i == currentIndex);
            }

            OnWeaponChanged?.Invoke(CurrentWeapon.Data.displayName);
        }

        public void TryShoot(bool aiming)
        {
            CurrentWeapon?.TryShoot(aiming);
        }

        public void TryReload()
        {
            CurrentWeapon?.TryReload(this);
        }
    }
}
