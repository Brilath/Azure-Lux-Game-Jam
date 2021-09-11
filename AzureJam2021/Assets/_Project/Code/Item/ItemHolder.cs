using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [Header("Item Slots")]
    [SerializeField] private Item _currentItem;
    [SerializeField] private GameObject _mask;
    [SerializeField] private GameObject _coat;
    [SerializeField] private GameObject _hazmat;
    [SerializeField] private GameObject _rubberDucky;
    [SerializeField] private GameObject _boots;

    public void GiveItem(Item item)
    {
        Debug.Log($"Giving sheep an item {item.Name}!");
        EquipItem(item);
    }

    private void EquipItem(Item item)
    {
        if (item == null) return;
        if (_currentItem == item) return;

        _currentItem = item;
        if (_currentItem.ItemType == ItemType.Mask)
        {
            EnableItem(_mask);
        }
        else if (_currentItem.ItemType == ItemType.Coat)
        {
            EnableItem(_coat);
        }
        else if (_currentItem.ItemType == ItemType.Hazmat)
        {
            EnableItem(_hazmat);
        }
        else if (_currentItem.ItemType == ItemType.RubberDucky)
        {
            EnableItem(_rubberDucky);
        }
        else if (_currentItem.ItemType == ItemType.Boots)
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
}
