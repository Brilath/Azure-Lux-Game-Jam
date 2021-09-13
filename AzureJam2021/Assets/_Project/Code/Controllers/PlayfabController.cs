using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabController : MonoBehaviour
{
    [Header("Playfab Info")]
    [SerializeField] private string _username;
    public string Username { get { return _username; } private set { _username = value; } }
    [SerializeField] private string _playfabId;
    public string PlayfabId { get { return _playfabId; } private set { _playfabId = value; } }
    [SerializeField] private EntityTokenResponse _entityToken;
    public EntityTokenResponse EntityToken { get { return _entityToken; } private set { _entityToken = value; } }
    [SerializeField] private string _sessionTicket;
    public string SessionTicket { get { return _sessionTicket; } private set { _sessionTicket = value; } }

    private static PlayfabController _instance;
    public static PlayfabController Instance { get { return _instance; } }

    public Action<PlayFabError> OnError = delegate { };

    #region Unity Methods
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "5B5AD";
        }
    }
    #endregion

    #region Private Methods
    private void Login(string username, string passwod)
    {
        var request = new LoginWithPlayFabRequest
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = passwod
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnFailure);
    }
    private void Register(string username, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            DisplayName = username,
            Password = password,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnFailure);
    }
    #endregion

    #region  Public Methods
    public void LoginPlayer(string username, string password)
    {
        Login(username, password);
        Username = username;
    }
    public void RegisterNewPlayer(string username, string password)
    {
        Register(username, password);
        Username = username;
    }
    public void Logout()
    {
        Username = "";
        PlayfabId = "";
        EntityToken = null;
        SessionTicket = "";
    }
    public void RecordScore(int score)
    {
        Debug.Log($"Requesting PlayFab to update {Username} score");
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "FlockScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    #endregion

    #region Playfab Callbacks
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log($"You have logged into your playfab account!");
        PlayfabId = result.PlayFabId;
        EntityToken = result.EntityToken;
        SessionTicket = result.SessionTicket;
        SceneController.LoadScene("Main Menu");
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log($"You have registered into your playfab account!");
        PlayfabId = result.PlayFabId;
        EntityToken = result.EntityToken;
        SessionTicket = result.SessionTicket;
        SceneController.LoadScene("Main Menu");
    }
    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"Updated Leaderboard");
    }
    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"There was an issue with your request: {error.GenerateErrorReport()}");
        OnError?.Invoke(error);
    }
    #endregion
}