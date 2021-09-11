using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _baseText;
    [SerializeField] private float _delayTime;    
    [SerializeField] private float _resetTime;

    [Header("Values")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _currentTotalTime;
    private TMP_Text _loadingText;

    private void Awake()
    {
        _loadingText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _loadingText.SetText(_baseText);
    }

    private void Update()
    {
        if (_currentTime >= _delayTime)
        {
            _loadingText.SetText($"{_loadingText.text}.");
            _currentTime = 0;
        }
        else if(_currentTotalTime >= _resetTime)
        {
            _loadingText.SetText(_baseText);
            _currentTime = 0;
            _currentTotalTime = 0;
        }
        _currentTime += Time.deltaTime;
        _currentTotalTime += Time.deltaTime;
    }
}
