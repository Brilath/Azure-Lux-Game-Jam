using UnityEngine;

public class HazardObject : MonoBehaviour
{
    [SerializeField] private Hazard _currentHazard;
    [SerializeField] private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        if (_currentHazard == null) return;

        Initialize(_currentHazard);
    }    

    public void Initialize(Hazard hazard)
    {
        _currentHazard = hazard;
        _spriteRenderer.sprite = _currentHazard.SpriteImage;
        var main = _particleSystem.main;
        main.startColor = _currentHazard.ParticleColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sheep sheep = collision.GetComponent<Sheep>();
        if (sheep == null) return;

        ItemHolder itemHolder = sheep.GetComponent<ItemHolder>();
        Health health = sheep.GetComponent<Health>();
        if(itemHolder.CurrentItem == null ||
            itemHolder.CurrentItem.ItemType != _currentHazard.ItemCounter)
        {
            Debug.Log($"Applying {_currentHazard.Damage} damage to {sheep.name}");
            if (_currentHazard.IsDamageOverTime)
            {
                health.ModifyHealthOverTime(_currentHazard.Damage, _currentHazard.DamageTime);
            }
            else
            {
                health.ModifyHealth(_currentHazard.Damage);
            }
        }
        itemHolder.ConsumeItem();
        gameObject.SetActive(false);
    }
}