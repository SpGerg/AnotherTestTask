using Inventories;
using Inventories.Items;
using Inventories.Items.Abilities.Items;
using Inventories.Items.Enums;
using Inventories.Items.Weapons;
using Inventories.Items.Weapons.Bow;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Factories
{
    public class ItemFactory : MonoBehaviour
    {
        public ItemData[] Items => _items;

        [SerializeField]
        private ItemData[] _items;

        [SerializeField]
        private ItemData _empty;

        [SerializeField]
        private GroundItem _groundItem;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Canvas _canvas;

        private readonly Dictionary<ItemType, Type> _itemsRealizations = new()
        {
            { ItemType.Bow, typeof(Bow) },
            { ItemType.Sword, typeof(Sword) },
            { ItemType.DamageSword, typeof(Sword) },
            { ItemType.DamageBow, typeof(Bow) },
            { ItemType.Head, typeof(Head) },
            { ItemType.HealPoison, typeof(Inventories.Items.HealPoison) },
        };

        public Item Create(ItemType itemType, Inventory inventory)
        {
            var empty = new GameObject();
            empty.transform.SetParent(inventory.transform);

            var itemData = GetItemData(itemType);

            if (itemData == null)
            {
                Debug.LogWarning($"{itemType} doesnt have scriptable object.");

                itemData = _empty;
            }

            Item component;

            if (!_itemsRealizations.TryGetValue(itemType, out var realization))
            {
                Debug.Log($"{itemType} doesnt have realization, adding default Item class...");

                component = empty.AddComponent<Item>();
            }
            else
            {
                component = empty.AddComponent(realization) as Item;
            }

            component.Initialize(inventory, itemData);

            return component;
        }

        public GroundItem CreateGround(ItemType itemType, Vector2 position)
        {
            var groundItem = Instantiate(_groundItem);
            groundItem.transform.position = position;
            var rectTransform = groundItem.GetComponent<RectTransform>();
            rectTransform.position = position;
            groundItem.Initialize(itemType, GetItemData(itemType).Icon);

            return groundItem;
        }

        public ItemData GetItemData(ItemType itemType) => _items.FirstOrDefault(item => item.Type == itemType);
    }
}
