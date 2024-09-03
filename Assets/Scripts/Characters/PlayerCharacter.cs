using Inventories.Items.Enums;
using Inventories.Items.Weapons;
using Inventories.Items.Weapons.Bow;
using Scripts.Factories;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters
{
    public class PlayerCharacter : Character
    {
        private Cooldown _cooldown = new(60);

        public override void Initaliaze(CharacterStatistics characterStatistics)
        {
            base.Initaliaze(characterStatistics);
        }

        public void Awake()
        {
            GameInput.BowChoosed.AddListener(OnBowChoosed);
            GameInput.SwordChoosed.AddListener(OnSwordChoosed);
            GameInput.Heal.AddListener(FullHealOnHeal);

            Initaliaze(BaseStatistics);
        }

        public override void OnDisable()
        {
            GameInput.BowChoosed.RemoveListener(OnBowChoosed);
            GameInput.SwordChoosed.RemoveListener(OnSwordChoosed);
            GameInput.Heal.RemoveListener(FullHealOnHeal);

            SceneManager.LoadScene(0);

            base.OnDisable();
        }

        private void FullHealOnHeal()
        {
            if (!_cooldown.IsReady || Fight.IsInProgress)
            {
                return;
            }

            _cooldown.ForceUse();
            Health.Heal(Health.MaxHealth - Health.HealthValue);
        }

        private void OnBowChoosed()
        {
            if (Inventory.BowInSlot == null)
            {
                return;
            }

            CurrentWeapon = Inventory.BowInSlot;
        }

        private void OnSwordChoosed()
        {
            if (Inventory.SwordInSlot == null)
            {
                return;
            }

            CurrentWeapon = Inventory.SwordInSlot;
        }
    }
}
