using Characters;
using Inventories;
using Inventories.Items;
using Inventories.Items.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : Slot
{
    [SerializeField]
    public bool isFirst;

    public void Awake()
    {
        if (isFirst)
        {
            Inventory.FirstItemSlot.ItemChanged.AddListener(SetSpriteOnItemChanged);
        }
        else
        {
            Inventory.SecondItemSlot.ItemChanged.AddListener(SetSpriteOnItemChanged);
        }
    }

    public override void ExecuteOnClick()
    {
        Ability ability;

        if (isFirst)
        {
            if (Inventory.FirstItemSlot.Item == null)
            {
                return;
            }

            ability = Inventory.FirstItemSlot.Item.Ability;
        }
        else
        {
            if (Inventory.SecondItemSlot.Item == null)
            {
                return;
            }

            ability = Inventory.SecondItemSlot.Item.Ability;
        }

        if (ability == null)
        {
            return; 
        }

        ability.ExecuteWithCooldown();
    }

    public void SetSpriteOnItemChanged(Statistic[] statistics, Statistic[] oldStatistics)
    {
        Item item;

        if (isFirst)
        {
            if (Inventory.FirstItemSlot.Item == null)
            {
                Image.enabled = false;
                return;
            }

            item = Inventory.FirstItemSlot.Item;
        }
        else
        {
            if (Inventory.SecondItemSlot.Item == null)
            {
                Image.enabled = false;
                return;
            }

            item = Inventory.SecondItemSlot.Item;
        }

        Image.enabled = true;
        Image.sprite = item.ItemData.Icon;
        Item = item;
    }
}
