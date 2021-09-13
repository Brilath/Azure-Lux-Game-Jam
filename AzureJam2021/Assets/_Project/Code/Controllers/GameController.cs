using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Sheep> _currentFlock;
    [SerializeField] private TMP_Text _flockCountText;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private GameObject _menuPanel;

    public static Action OnGameStart = delegate { };
    public static Action OnGameOver = delegate { };

    private void Awake()
    {
        SheepSpawner.OnSheepSpawned += HandleSheepSpawned;
        Health.OnDeath += HandleDeath;
    }
    private void OnDestroy()
    {
        SheepSpawner.OnSheepSpawned -= HandleSheepSpawned;
        Health.OnDeath -= HandleDeath;
    }
    private void Start()
    {
        StartCoroutine(StartCountdown());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }

    private void UpdateFlockUI()
    {
        _flockCountText.SetText($"Woolf Pack: {_currentFlock.Count}");
    }

    private void HandleSheepSpawned()
    {
        Sheep[] foundSheeps = FindObjectsOfType<Sheep>();
        _currentFlock = foundSheeps.ToList();

        UpdateFlockUI();
    }

    private void HandleDeath(GameObject deadObject)
    {
        Sheep deadSheep = deadObject.GetComponent<Sheep>();
        if (deadSheep == null) return;
        if (_currentFlock.Contains(deadSheep))
            _currentFlock.Remove(deadSheep);

        UpdateFlockUI();
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        if (_currentFlock.Count >= 0) return;
        OnGameOver?.Invoke();        
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        if (!_menuPanel.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;

        _menuPanel.SetActive(!_menuPanel.activeSelf);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneController.LoadScene("Game");
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneController.LoadScene("Main Menu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnGameOver?.Invoke();
        PlayfabController.Instance.RecordScore(_currentFlock.Count);
        ToggleMenu();
    }

    private IEnumerator StartCountdown()
    {
        _countdownText.gameObject.SetActive(true);
        int time = 3;
        while(time > 0)
        {
            _countdownText.SetText(time.ToString());
            yield return new WaitForSeconds(1);
            time--;
        }
        _countdownText.gameObject.SetActive(false);
        OnGameStart?.Invoke();
    }
}
