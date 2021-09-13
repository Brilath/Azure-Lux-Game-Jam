using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private Transform _leaderboardContainer;
    [SerializeField] private Leader _leaderPrefab;

    [SerializeField] private GameObject _helpPanel;

    private void Awake()
    {
        PlayfabController.Instance.OnLeaderboardUpdated += HandleLeaderboardUpdated;
    }
    private void OnDestroy()
    {
        PlayfabController.Instance.OnLeaderboardUpdated -= HandleLeaderboardUpdated;
    }
    private void Start()
    {
        RefreshLeaderboard();
    }

    public void StartGame()
    {
        SceneController.LoadScene("Story");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Logout()
    {
        PlayfabController.Instance.Logout();
        SceneController.LoadScene("Login");
    }
    public void ToggleLeaderboard()
    {
        _leaderboardPanel.SetActive(!_leaderboardPanel.activeSelf);

        if (_leaderboardPanel.activeSelf)
            RefreshLeaderboard();
    }
    public void ToggleHelp()
    {
        _helpPanel.SetActive(!_helpPanel.activeSelf);
    }

    public void RefreshLeaderboard()
    {
        PlayfabController.Instance.GetLoaderboard();
    }

    private void HandleLeaderboardUpdated()
    {
        UpdateLeaderboard();
    }
    private void UpdateLeaderboard()
    {
        foreach(Transform leader in _leaderboardContainer)
        {
            Destroy(leader.gameObject);
        }
        foreach(var player in PlayfabController.Instance.CurrentLeaderboard.Leaderboard)
        {
            Leader newLeader = Instantiate(_leaderPrefab, _leaderboardContainer);
            newLeader.Initialize(player.DisplayName, player.StatValue, player.Position);
        }
    }
}
