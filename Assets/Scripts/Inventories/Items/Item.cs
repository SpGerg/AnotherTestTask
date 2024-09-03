using Characters;
using Inventories.Items.Abilities;
using System.Text;
using UnityEngine;

namespace Inventories.Items
{
    public class Item : MonoBehaviour
    {
        public Character Character => _character;

        public Animator Animator => _character.Animator;

        public ItemData ItemData { get => _itemData; }

        public string StatisticsDescription { get; private set; }

        public string EquipAnimationName => _itemData.EquipAnimationName;

        public virtual ActiveAbility Ability { get; protected set; }

        [SerializeField]
        private Character _character;

        [SerializeField]
        private ItemData _itemData;

        private const int _descriptionSize = 2;

        public void Start()
        {
            //Можно сделать пул, но т.к это прототип, не буду. Да и банально лень
            var stringBuilder = new StringBuilder();

            foreach (var statistic in _itemData.Statistics)
            {
                stringBuilder.AppendLine(statistic.ToString());
            }

            StatisticsDescription = stringBuilder.ToString();
        }

        public void Initialize(Inventory inventory, ItemData itemData)
        {
            _character = _character != null ? _character : inventory.Character;
            _itemData = _itemData != null ? _itemData : itemData;
        }

        public virtual void Equip()
        {
            Character.AddStatistics(ItemData.Statistics);
        }

        public virtual void Unequip()
        {
            Character.RemoveStatistics(ItemData.Statistics);
        }

        public override string ToString()
        {
            return $"<size={_descriptionSize}>{_itemData.Description}</size>\n{StatisticsDescription}";
        }
    }
}
