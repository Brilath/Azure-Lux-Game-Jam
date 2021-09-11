using UnityEngine;

[CreateAssetMenu(menuName = "Game Stuff/Hazard", fileName = "newHazard")]
public class Hazard : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } private set { _name = value; } }
    [SerializeField] private bool _isDamageOverTime;
    public bool IsDamageOverTime { get { return _isDamageOverTime; } private set { _isDamageOverTime = value; } }
    [SerializeField] private float _damage;
    public float Damage { get { return _damage; } private set { _damage = value; } }
    [SerializeField] private float _damageTime;
    public float DamageTime { get { return _damageTime; } private set { _damageTime = value; } }
    [SerializeField] private ItemType _itemCounter;
    public ItemType ItemCounter { get { return _itemCounter; } private set { _itemCounter = value; } }
    [SerializeField] private Sprite _spriteImage;
    public Sprite SpriteImage { get { return _spriteImage; } private set { _spriteImage = value; } }
    [SerializeField] private Sprite _uIImage;
    public Sprite UIImage { get { return _uIImage; } private set { _uIImage = value; } }
}
