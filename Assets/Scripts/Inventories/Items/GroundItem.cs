using Characters;
using Inventories.Items;
using Inventories.Items.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundItem : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    private ItemType _itemType;

    private PlayerCharacter _character;

    private Canvas _canvas;

    public void Awake()
    {
        _character = FindObjectOfType<PlayerCharacter>();
        _canvas = FindObjectOfType<Canvas>();
    }

    public void Initialize(ItemType itemType, Sprite icon)
    {
        _itemType = itemType;
        _image.sprite = icon;

        transform.SetParent(_canvas.transform, false);
    }

    public void AddItemOnClick()
    {
        if (!_character.Inventory.AddItem(_itemType))
        {
            return;
        }

        Destroy(gameObject);
    }
}
