using Characters;
using Inventories.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Inventories.Items.Weapons.Bow;
using Inventories.Items.Weapons;
using UnityEngine.UIElements;
using Inventories.Items.Enums;

namespace Inventories
{
    public class InventorySlot : Slot
    {
        public override Item Item {
            get
            {
                return _item;
            }
            set
            {
                var oldValue = _item;

                _item = value;

                ItemChanged.Invoke(_item != null ? _item.ItemData.Statistics : null, oldValue != null ? oldValue.ItemData.Statistics : null);

                if (_item == null)
                {
                    Image.sprite = null;
                    Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0);
                }
                else
                {
                    Image.sprite = _item.ItemData.Icon;
                    Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 255);
                }
            }
        }

        public UnityEvent<Statistic[], Statistic[]> ItemChanged { get; } = new();

        public UnityEvent<InventorySlot> ItemSelected { get; } = new();

        public bool IsFree => Item == null;

        public bool IsSecondItem { get => _isSecondItem; set => _isSecondItem = value; }

        public bool IsFirstItem { get => _isFirstItem; set => _isFirstItem = value; }

        public bool IsBow { get => _isBow; set => _isBow = value; }

        public bool IsSword { get => _isSword; set => _isSword = value; }

        [SerializeField]
        private bool _isSword;

        [SerializeField]
        private bool _isBow;

        [SerializeField]
        private bool _isFirstItem;

        [SerializeField]
        private bool _isSecondItem;

        public override void ExecuteOnClick()
        {
            if (Item == null && Inventory.SelectedSlot != null)
            {
                SetItem(Inventory.SelectedSlot.Item);

                return;
            }

            if (Inventory.SelectedSlot == null)
            {
                Inventory.SelectCurrentSlot(this);

                return;
            }

            if (Inventory.SelectedSlot.Item == null)
            {
                Inventory.UnselectAndSwapSlots(this);

                return;
            }

            var item = Inventory.SelectedSlot.Item;

            if (!SetItem(item))
            {
                return; 
            }

            Inventory.UnselectAndSwapSlots(this);
        }

        public bool SetItem(Item item)
        {
            if (Inventory.SelectedSlot is SuitSlot && Item.ItemData.SuitType == SuitType.None)
            {
                return false;
            }

            if (_isBow)
            {
                if (item is not Bow bow)
                {
                    return false;
                }

                Inventory.UnselectAndSwapSlots(this);

                Inventory.BowInSlot = bow;

                return false;
            }
            else if (_isSword)
            {
                if (item is not Sword sword)
                {
                    return false;
                }

                Inventory.UnselectAndSwapSlots(this);

                Inventory.SwordInSlot = sword;

                return false;
            }
            else if (_isFirstItem)
            {
                Inventory.FirstItemInSlot = item;
            }
            else if (_isSecondItem)
            {
                Inventory.SecondItemInSlot = item;
            }

            return true;
        }
    }
}
