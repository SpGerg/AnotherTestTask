using Characters;
using Inventories.Items.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventories.Items
{
    [CreateAssetMenu(fileName = "New item data", menuName = "Item data", order = 53)]
    [Serializable]
    public class ItemData : ScriptableObject
    {
        public string Name { get => _name; }

        public string Description { get => _description; }

        public Statistic[] Statistics { get => _statistics; }

        public Sprite Icon { get => _icon; }

        public ItemType Type { get => _type; }

        public SuitType SuitType { get => _suitType; }

        public string EquipAnimationName { get => _equipAnimationName; }

        [SerializeField]
        private string _name;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Statistic[] _statistics;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private ItemType _type;

        [SerializeField]
        private SuitType _suitType;

        [SerializeField]
        [Description("Parameter name for weapon")]
        private string _equipAnimationName;
    }
}
