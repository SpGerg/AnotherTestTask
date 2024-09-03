using Inventories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories
{
    public abstract class Slot : MonoBehaviour
    {
        public Inventory Inventory => _inventory;

        public Image Image => _image;

        public virtual Item Item
        {
            get
            {
                return _item; 
            }
            set
            {
                _item = value;

                if (_item == null)
                {
                    _image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0);

                    return;
                }

                _image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 255);
                _image.sprite = _item.ItemData.Icon;
            }
        }

        [SerializeField]
        protected Item _item;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Inventory _inventory;

        public abstract void ExecuteOnClick();
    }
}
