using UnityEngine;
using Inventories.Items.Enums;

namespace Inventories
{
    public class SuitSlot : Slot
    {
        public override Items.Item Item { get => base.Item; set => base.Item = value; }

        [SerializeField]
        private SuitType _type;

        [SerializeField]
        private bool _isLeftSleeve;

        public override void ExecuteOnClick()
        {
            if (Inventory.SelectedSlot == null)
            {
                Inventory.SelectCurrentSlot(this);

                return;
            }

            if (Inventory.SelectedSlot.Item == null || Inventory.SelectedSlot.Item.ItemData.SuitType != _type)
            {
                return;
            }

            var item = Inventory.SelectedSlot.Item;

            SetItem(item);
            Inventory.UnselectAndSwapSlots(this);

            Item = item;

            if (_type == SuitType.Sleeve && !_isLeftSleeve)
            {
                Image.transform.rotation = new Quaternion(0, -180, Image.transform.rotation.y, 0);
            }
            else
            {
                Image.transform.rotation = new Quaternion(0, 0, Image.transform.rotation.z, 0);
            }
        }

        private void SetItem(Items.Item item)
        {
            switch (_type)
            {
                case SuitType.Helmet:
                    Inventory.Helmet = item;
                    break;
                case SuitType.Breastplate:
                    Inventory.Breastplate = item;
                    break;
                case SuitType.Sleeve:
                    if (_isLeftSleeve)
                    {
                        Inventory.LeftSleeve = item;
                    }
                    else
                    {
                        Inventory.RightSleeve = item;
                    }
                    
                    break;
                case SuitType.Leggings:
                    Inventory.Leggings = item;
                    break;
                case SuitType.Boots:
                    Inventory.Boots = item;
                    break;
            }
        }
    }
}
