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
    [SerializeField] private float _duration;
    public float Duration { get { return _duration; } private set { _duration = value; } }
    [SerializeField] private HazardType _type;
    public HazardType Type { get { return _type; } private set { _type = value; } }
    [SerializeField] private ItemType _itemCounter;
    public ItemType ItemCounter { get { return _itemCounter; } private set { _itemCounter = value; } }
    [SerializeField] private Sprite _spriteImage;
    public Sprite SpriteImage { get { return _spriteImage; } private set { _spriteImage = value; } }
    [SerializeField] private Sprite _uIImage;
    public Sprite UIImage { get { return _uIImage; } private set { _uIImage = value; } }
    [SerializeField] private Color _particleColor;
    public Color ParticleColor { get { return _particleColor; } private set { _particleColor = value; } }
}

public enum HazardType
{
    Chemical,
    Gas,
    Fire,
    Water,
    Electrical
}