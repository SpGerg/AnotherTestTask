using Characters;
using Characters.States;
using Characters.States.Enums;
using Inventories.Items;
using Inventories.Items.Enums;
using Inventories.Items.Weapons;
using Inventories.Items.Weapons.Bow;
using Scripts.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    public class Inventory : MonoBehaviour
    {
        public Weapon CurrentWeapon
        {
            get
            {
                return _currentWeapon;
            }
            set
            {
                if (StateMachine.Current is CharacterStateType.ChangingWeapon || _currentWeapon == null)
                {
                    if (_currentWeapon != null && !(StateMachine != null ? StateMachine.CurrentState as ChangingWeapon : null).IsFinished)
                    {
                        return;
                    }

                    if (_currentWeapon != null)
                    {
                        _currentWeapon.Unequip();
                    }

                    if (_currentWeapon != null && _currentWeapon.Target != null)
                    {
                        value.Target = _currentWeapon.Target;
                    }

                    _currentWeapon = value;

                    _currentWeapon.Equip();

                    if (StateMachine.LastState != null)
                    {
                        StateMachine.Current = StateMachine.LastState.Type;
                    }

                    return;
                }

                StateMachine.Current = CharacterStateType.ChangingWeapon;
                StateMachine.CurrentState.Enter();
                (StateMachine.CurrentState as ChangingWeapon).Enter(value);
            }
        }

        public Character Character => _character;

        public InventorySlot BowSlot => _bowSlot;

        public InventorySlot SwordSlot => _swordSlot;

        public InventorySlot FirstItemSlot => _firstSlot;

        public InventorySlot SecondItemSlot => _secondSlot;

        public Slot SelectedSlot => _slotSelected;

        public IReadOnlyList<Item> Items => _items;

        public SuitSlot HelmetSlot => _helmetSlot;

        public SuitSlot BreastplateSlot => _breastplateSlot;

        public SuitSlot LeggingsSlot => _leggingsSlot;

        public SuitSlot BootsSlot => _bootsSlot;

        public SuitSlot LeftSleeveSlot => _leftSleeveSlot;

        public SuitSlot RightSleeveSlot => _rightSleeveSlot;

        public Item Helmet
        {
            get
            {
                return _helmet;
            }
            set
            {
                if (_helmet != null) 
                {
                    _helmet.Unequip();
                }

                _helmet = value;

                if (_helmet != null)
                {
                    _helmet.Equip(); 
                }
            }
        }

        public Item Breastplate
        {
            get
            {
                return _breastplate;
            }
            set
            {
                if (_breastplate != null)
                {
                    _breastplate.Unequip();
                }

                _breastplate = value;

                if (_breastplate != null)
                {
                    _breastplate.Equip();
                }
            }
        }

        public Item Leggings
        {
            get
            {
                return _leggings;
            }
            set
            {
                if (_leggings != null)
                {
                    _leggings.Unequip();
                }

                _leggings = value;

                if (_leggings != null)
                {
                    _leggings.Equip();
                }
            }
        }

        public Item Boots
        {
            get
            {
                return _boots;
            }
            set
            {
                if (_boots != null)
                {
                    _boots.Unequip();
                }

                _boots = value;

                if (_boots != null)
                {
                    _boots.Equip();
                }
            }
        }

        public Item LeftSleeve
        {
            get
            {
                return _leftSleeve;
            }
            set
            {
                if (_leftSleeve != null)
                {
                    _leftSleeve.Unequip();
                }

                _leftSleeve = value;

                if (_leftSleeve != null)
                {
                    _leftSleeve.Equip();
                }
            }
        }

        public Item RightSleeve
        {
            get
            {
                return _rightSleeve;
            }
            set
            {
                if (_rightSleeve != null)
                {
                    _rightSleeve.Unequip();
                }

                _rightSleeve = value;

                if (_rightSleeve != null)
                {
                    _rightSleeve.Equip();
                }
            }
        }

        public Sword SwordInSlot
        {
            get
            {
                return _sword;
            }
            set
            {
                if (_sword == value)
                {
                    return;
                }

                _sword = value;

                if (_swordSlot != null)
                {
                    _swordSlot.Item = value;
                }
                
                if (_currentWeapon == null)
                {
                    Character.CurrentWeapon = value;
                }
            }
        }

        public Bow BowInSlot
        {
            get
            {
                return _bow;
            }
            set
            {
                if (_bow == value)
                {
                    return;
                }

                _bow = value;

                if (_bowSlot != null)
                {
                    _bowSlot.Item = value;
                }

                if (_currentWeapon == null)
                {
                    Character.CurrentWeapon = value;
                }
            }
        }

        public Item FirstItemInSlot {
            get
            {
                return _first;
            }
            set
            {
                if (_first == value)
                {
                    return;
                }

                if (_first != null)
                {
                    _first.Unequip();
                }

                _first = value;

                if (_firstSlot != null)
                {
                    _firstSlot.Item = value;
                } 

                if (_first != null)
                {
                    _first.Equip();
                }
            }
        }

        public Item SecondItemInSlot
        {
            get
            {
                return _second;
            }
            set
            {
                if (_second == value)
                {
                    return;
                }

                if (_second != null)
                {
                    _second.Unequip();
                }

                _second = value;

                if (_secondSlot != null)
                {
                    _secondSlot.Item = value;
                }

                if (_second != null)
                {
                    _second.Equip();
                }
            }
        }

        public bool IsShowPanelOpen { get; private set; }

        private CharacterStateMachine StateMachine => _character.StateMachine;
             
        [SerializeField]
        private GameObject _slots;

        [SerializeField]
        private Character _character;

        [SerializeField]
        private ItemFactory _itemFactory;

        [SerializeField]
        private List<Item> _items = new();

        [SerializeField]
        private Weapon _currentWeapon;

        [SerializeField]
        private InventorySlot _swordSlot;

        [SerializeField]
        private InventorySlot _bowSlot;

        [SerializeField]
        private InventorySlot _firstSlot;

        [SerializeField]
        private InventorySlot _secondSlot;

        [SerializeField]
        private Slot _slotSelected;

        [SerializeField]
        private InfoPanel _infoPanelPrefab;

        [SerializeField]
        private Transform _infoPanelPosition;

        [SerializeField]
        private GameObject _inventory;

        [SerializeField]
        private SuitSlot _helmetSlot;

        [SerializeField]
        private SuitSlot _breastplateSlot;

        [SerializeField]
        private SuitSlot _leggingsSlot;

        [SerializeField]
        private SuitSlot _bootsSlot;

        [SerializeField]
        private SuitSlot _leftSleeveSlot;

        [SerializeField]
        private SuitSlot _rightSleeveSlot;

        private Sword _sword;

        private Bow _bow;

        private Item _helmet;

        private Item _breastplate;

        private Item _leggings;

        private Item _boots;

        private Item _leftSleeve;

        private Item _rightSleeve;

        private Item _first;

        private Item _second;

        private InventorySlot[] _itemSlots;

        private Slot _selectedInfoPanelSlot;

        private InfoPanel _infoPanel;

        public void Awake()
        {
            if (_itemFactory == null)
            {
                _itemFactory = FindObjectOfType<ItemFactory>();
            }

            if (_slots == null)
            {
                return;
            }

            _itemSlots = _slots.GetComponentsInChildren<InventorySlot>();
        }

        public void Start()
        {
            //AddItem(ItemType.Bow);
            //AddItem(ItemType.Sword);
            //AddItem(ItemType.BasicHelmet);
        }

        public void OnDestroy()
        {
            if (_slots == null)
            {
                return;
            }
        }

        public void ToggleShow(Slot slot)
        {
            if (_infoPanel != null && _selectedInfoPanelSlot == slot)
            {
                Destroy(_infoPanel);
                _infoPanel = null;

                IsShowPanelOpen = false;

                return;
            }

            ShowInfo(slot.Item);

            IsShowPanelOpen = true;

            _selectedInfoPanelSlot = slot;
        }

        private void ShowInfo(Item item)
        {
            _infoPanel = Instantiate(_infoPanelPrefab, _infoPanelPosition.position, Quaternion.identity, _inventory.transform);
            _infoPanel.Name = item.ItemData.Name;
            _infoPanel.Description = item.ItemData.Description;
            _infoPanel.Statistics = item.StatisticsDescription;
        }

        public void UnselectAndSwapSlots(Slot slot)
        {
            var item = slot.Item;
            _slotSelected.Image.color = Color.white;

            if (slot.Item != null)
            {
                ToggleShow(slot);
            }

            slot.Item = _slotSelected.Item;
            _slotSelected.Item = item;

            _slotSelected = null;
        }

        public void SelectCurrentSlot(Slot slot)
        {
            if (_slotSelected != null)
            {
                UnselectAndSwapSlots(slot);

                return;
            }

            _slotSelected = slot;
            _slotSelected.Image.color = Color.green;

            ToggleShow(slot);
        }

        public void UnselectCurrentSlot()
        {
            _slotSelected.Image.color = Color.white;
            _slotSelected = null;
        }

        public bool AddItem(ItemType itemType)
        {
            var freeSlot = FindFreeSlot();

            if (freeSlot == null)
            {
                return false;
            }

            var instance = _itemFactory.Create(itemType, this);

            _items.Add(instance);
            freeSlot.Item = instance;

            return true;
        }

        public void RemoveItem(ItemType itemType)
        {
            if (SwordInSlot != null && SwordInSlot.ItemData.Type == itemType)
            {
                SwordInSlot = null;

                return;
            }
            else if (BowInSlot != null && BowInSlot.ItemData.Type == itemType)
            {
                BowInSlot = null;

                return;
            }
            else if (FirstItemSlot.Item != null && FirstItemSlot.Item.ItemData.Type == itemType)
            {
                FirstItemSlot.Item = null;

                return;
            }
            else if (SecondItemSlot.Item != null && SecondItemSlot.Item.ItemData.Type == itemType)
            {
                SecondItemSlot.Item = null;

                return;
            }
            else if (HelmetSlot.Item != null && HelmetSlot.Item.ItemData.Type == itemType)
            {
                HelmetSlot.Item = null;

                return;
            }
            else if (BreastplateSlot.Item != null && BreastplateSlot.Item.ItemData.Type == itemType)
            {
                BreastplateSlot.Item = null;

                return;
            }
            else if (LeggingsSlot.Item != null && LeggingsSlot.Item.ItemData.Type == itemType)
            {
                LeggingsSlot.Item = null;

                return;
            }
            else if (BootsSlot.Item != null && BootsSlot.Item.ItemData.Type == itemType)
            {
                BootsSlot.Item = null;

                return;
            }
            else if (LeftSleeveSlot.Item != null && LeftSleeveSlot.Item.ItemData.Type == itemType)
            {
                LeftSleeveSlot.Item = null;

                return;
            }
            else if (RightSleeveSlot.Item != null && RightSleeveSlot.Item.ItemData.Type == itemType)
            {
                RightSleeveSlot.Item = null;

                return;
            }

            var item = _items.FirstOrDefault(item => item.ItemData.Type == itemType);

            _items.Remove(item);
        }

        public void AddOrRemoveStatisticsOnItemChanged(Statistic[] newStatistics, Statistic[] oldStatistics)
        {
            if (oldStatistics != null)
            {
                _character.RemoveStatistics(oldStatistics);
            }
            
            _character.AddStatistics(newStatistics);
        }

        public InventorySlot FindFreeSlot()
        {
            if (_itemSlots == null)
            {
                return null;
            }

            foreach (var slot in _itemSlots)
            {
                if (!slot.IsFree || slot.IsBow || slot.IsSword)
                {
                    continue;
                }

                return slot;
            }

            return null;
        }
    }
}
