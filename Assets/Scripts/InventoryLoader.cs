using Databases;
using Databases.Serializables;
using Inventories;
using Inventories.Items.Enums;
using Inventories.Items.Weapons;
using Inventories.Items.Weapons.Bow;
using Scripts.Factories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryLoader : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    private InventorySerializable _inventorySerializable;

    [SerializeField]
    private ItemFactory _itemFactory;

    private ItemSerializable[] _items;

    public void Awake()
    {
        _items = JsonDatabase.Instance.Inventory.items;
    }

    public void Start()
    {
        foreach (var item in _items)
        {
            if (item.isBow)
            {
                var createdItem = _itemFactory.Create(item.itemType, _inventory);

                _inventory.BowSlot.Item = createdItem;
                _inventory.BowInSlot = createdItem as Bow;

                continue;
            }
            else if (item.isSword)
            {
                var createdItem = _itemFactory.Create(item.itemType, _inventory);

                _inventory.SwordSlot.Item = createdItem;
                _inventory.SwordInSlot = createdItem as Sword;

                continue;
            }

            _inventory.AddItem(item.itemType);
        }
    }

    public void OnApplicationQuit()
    {
        var items = _inventory.Items.Select(item => new ItemSerializable()
        {
            itemType = item.ItemData.Type
        }).ToList();

        if (_inventory.BowInSlot != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.BowInSlot.ItemData.Type,
                isBow = true
            });
        }

        if (_inventory.SwordInSlot != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.SwordInSlot.ItemData.Type,
                isSword = true
            });
        }

        if (_inventory.FirstItemInSlot != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.FirstItemInSlot.ItemData.Type
            });
        }

        if (_inventory.SecondItemInSlot != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.SecondItemInSlot.ItemData.Type
            });
        }

        if (_inventory.Helmet != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.Helmet.ItemData.Type
            });
        }

        if (_inventory.Breastplate != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.Breastplate.ItemData.Type
            });
        }

        if (_inventory.Leggings != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.Leggings.ItemData.Type
            });
        }

        if (_inventory.Boots != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.Boots.ItemData.Type
            });
        }

        if (_inventory.LeftSleeve != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.LeftSleeve.ItemData.Type
            });
        }

        if (_inventory.RightSleeve != null)
        {
            items.Add(new ItemSerializable()
            {
                itemType = _inventory.RightSleeve.ItemData.Type
            });
        }

        JsonDatabase.Instance.SaveInventory(new InventorySerializable()
        {
            items = items.ToArray()
        });
    }
}
