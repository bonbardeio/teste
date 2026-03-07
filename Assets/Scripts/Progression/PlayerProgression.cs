using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileFPS.Progression
{
    /// <summary>
    /// XP, níveis, desbloqueio de armas e melhorias de atributos.
    /// </summary>
    public class PlayerProgression : MonoBehaviour
    {
        [Serializable]
        public class WeaponUnlock
        {
            public string weaponId;
            public int requiredLevel;
        }

        [SerializeField] private List<WeaponUnlock> unlocks;
        [SerializeField] private int baseXpToLevel = 100;

        public int Level { get; private set; } = 1;
        public int CurrentXp { get; private set; }
        public float DamageMultiplier { get; private set; } = 1f;
        public float ReloadMultiplier { get; private set; } = 1f;
        public float AccuracyMultiplier { get; private set; } = 1f;

        private readonly HashSet<string> unlockedWeapons = new();

        public void AddXp(int amount)
        {
            CurrentXp += amount;
            while (CurrentXp >= NeededXpForNextLevel())
            {
                CurrentXp -= NeededXpForNextLevel();
                Level++;
                ApplyLevelBonus();
                UnlockWeaponsByLevel();
            }
        }

        public bool IsWeaponUnlocked(string weaponId) => unlockedWeapons.Contains(weaponId);

        private int NeededXpForNextLevel() => baseXpToLevel + ((Level - 1) * 50);

        private void ApplyLevelBonus()
        {
            // Bônus pequenos para progressão contínua.
            DamageMultiplier += 0.03f;
            ReloadMultiplier = Mathf.Max(0.6f, ReloadMultiplier - 0.02f);
            AccuracyMultiplier += 0.02f;
        }

        private void UnlockWeaponsByLevel()
        {
            foreach (var unlock in unlocks)
            {
                if (Level >= unlock.requiredLevel)
                    unlockedWeapons.Add(unlock.weaponId);
            }
        }
    }
}
