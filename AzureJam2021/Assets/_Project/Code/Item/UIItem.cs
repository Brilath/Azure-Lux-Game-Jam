using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image _uiItemImage;
    [SerializeField] private Image _uiItemBackgroundImage;
    private ItemDraggable _itemDraggable;

    private void Awake()
    {
        _itemDraggable = GetComponent<ItemDraggable>();
    }

    private void Start()
    {
        _uiItemImage.sprite = _itemDraggable.CurrentItem.UIImage;
        _uiItemBackgroundImage.color = _itemDraggable.CurrentItem.ItemColor;
    }
}
