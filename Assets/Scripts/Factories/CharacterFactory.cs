using Characters.Enemies;
using Inventories.Items.Enums;
using Inventories.Items.Weapons;
using Scripts.Factories;
using UnityEngine;

namespace Factories
{
    public class CharacterFactory : MonoBehaviour
    {
        [SerializeField]
        private Enemy _enemyPrefab;

        [SerializeField]
        private ItemFactory _itemFactory;

        public Enemy CreateEnemy(Vector2 position, Quaternion quaternion, CharacterStatistics characterStatistics, EnemyStatistics enemyStatistics, Weapon weapon = null)
        {
            var instance = Instantiate(_enemyPrefab, position, quaternion);
            instance.Initaliaze(characterStatistics);
            instance.InitializeEnemy(enemyStatistics, _itemFactory.Create(ItemType.Head, instance.Inventory) as Weapon);

            if (characterStatistics.RuntimeAnimatorController == null)
            {
                Debug.LogWarning($"In scriptable object with {characterStatistics.name} name animator controller is null");
            }

            if (weapon != null)
            {
                instance.CurrentWeapon = weapon;
            }

            return instance;
        }

        public Enemy CreateEnemy(Transform transform, CharacterStatistics characterStatistics, EnemyStatistics enemyStatistics)
            => CreateEnemy(transform.position, transform.rotation, characterStatistics, enemyStatistics);
    }
}