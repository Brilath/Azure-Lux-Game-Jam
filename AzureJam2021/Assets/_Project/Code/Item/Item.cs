using UnityEngine;

[CreateAssetMenu(menuName = "Game Stuff/Item", fileName = "newItem")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } private set { _name = value; } }
    [SerializeField] private ItemType _itemType;
    public ItemType ItemType { get { return _itemType; } private set { _itemType = value; } }
    [SerializeField] private Sprite _uIImage;
    public Sprite UIImage { get { return _uIImage; } private set { _uIImage = value; } }
    [SerializeField] private Color _itemColor;
    public Color ItemColor { get { return _itemColor; } private set { _itemColor = value; } }
}

public enum ItemType
{
    Mask,
    Boots,
    Coat,
    Hazmat,
    RubberDucky
}