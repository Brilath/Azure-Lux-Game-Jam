using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [Header("Item Slots")]
    [SerializeField] private Item _currentItem;
    public Item CurrentItem { get { return _currentItem; } private set { _currentItem = value; } }
    [SerializeField] private GameObject _mask;
    [SerializeField] private GameObject _coat;
    [SerializeField] private GameObject _hazmat;
    [SerializeField] private GameObject _rubberDucky;
    [SerializeField] private GameObject _boots;

    public static Action<Item> OnItemEquiped = delegate { };

    public void GiveItem(Item item)
    {
        Debug.Log($"Giving sheep an item {item.Name}!");
        EquipItem(item);
    }
    public void ConsumeItem()
    {
        CurrentItem = null;
        DisableItem();
    }

    private void EquipItem(Item item)
    {
        if (item == null) return;
        if (CurrentItem == item) return;

        CurrentItem = item;
        OnItemEquiped?.Invoke(CurrentItem);
        if (CurrentItem.ItemType == ItemType.Mask)
        {
            EnableItem(_mask);
        }
        else if (CurrentItem.ItemType == ItemType.Coat)
        {
            EnableItem(_coat);
        }
        else if (CurrentItem.ItemType == ItemType.Hazmat)
        {
            EnableItem(_hazmat);
        }
        else if (CurrentItem.ItemType == ItemType.RubberDucky)
        {
            EnableItem(_rubberDucky);
        }
        else if (CurrentItem.ItemType == ItemType.Boots)
        {
            EnableItem(_boots);
        }        
    }

    private void EnableItem(GameObject item)
    {
        _mask.SetActive(false);
        _coat.SetActive(false);
        _hazmat.SetActive(false);
        _rubberDucky.SetActive(false);
        _boots.SetActive(false);
        item.SetActive(true);
    }

    private void DisableItem()
    {
        _mask.SetActive(false);
        _coat.SetActive(false);
        _hazmat.SetActive(false);
        _rubberDucky.SetActive(false);
        _boots.SetActive(false);
    }
}
