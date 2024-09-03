using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventory; 

    public void ToggleActiveOnClick()
    {
        _inventory.SetActive(!_inventory.activeSelf);
    }
}
