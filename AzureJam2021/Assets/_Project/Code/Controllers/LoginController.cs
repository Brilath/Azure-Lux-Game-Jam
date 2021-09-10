using System.Collections;
using UnityEngine;
using TMPro;
using PlayFab;

public class LoginController : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private GameObject _loginContainer;
    [SerializeField] private TMP_InputField _loginUsername;
    [SerializeField] private TMP_InputField _loginPassword;

    [Header("Register")]
    [SerializeField] private GameObject _registerContainer;
    [SerializeField] private TMP_InputField _registerUsername;
    [SerializeField] private TMP_InputField _registerPassword;
    [SerializeField] private TMP_InputField _registerConfirmPassword;

    [Header("Misc")]
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private float _errorDispalyTime;

    private Coroutine _errorCoroutine;

    #region Unity Methods
    private void Awake()
    {
        PlayfabController.Instance.OnError += HandlePlayfabError;
    }

    private void OnDestroy()
    {
        PlayfabController.Instance.OnError -= HandlePlayfabError;
    }
    #endregion

    #region Private Methods
    private bool CheckPasswordMatch()
    {
        return string.Compare(_registerPassword.text, _registerConfirmPassword.text) == 0;
    }
    private bool CheckUsername()
    {
        bool isValid = false;
        string username = _registerUsername.text;

        if (username.Length >= 3 && username.Length <= 24)
            isValid = true;

        return isValid;
    }
    private void HandlePlayfabError(PlayFabError error)
    {
        StatusText(error.GenerateErrorReport());
    }
    private void StatusText(string message)
    {
        _statusText.SetText(message);
        if (_errorCoroutine != null)
            StopCoroutine(_errorCoroutine);

        _errorCoroutine = StartCoroutine(ClearErrorText());
    }
    private IEnumerator ClearErrorText()
    {
        yield return new WaitForSeconds(_errorDispalyTime);
        _statusText.SetText("");
    }
    #endregion

    #region  Public Methods
    public void Login()
    {
        PlayfabController.Instance.LoginPlayer(_loginUsername.text, _loginPassword.text);
    }
    public void RegisterAccount()
    {
        bool passwordMatch = CheckPasswordMatch();
        bool validUsername = CheckUsername();
        if (!validUsername)
        {
            StatusText("Invalid Username!");
        }
        else if(!passwordMatch)
        {
            StatusText("Passwords do not match!");
        }
        else
        {
            PlayfabController.Instance.RegisterNewPlayer(_registerUsername.text, _registerPassword.text);
            StatusText("Registering account!");
        }
    }
    public void ShowLoginPanel()
    {
        _registerContainer.SetActive(false);
        _loginContainer.SetActive(true);
    }
    public void ShowRegisterPanel()
    {
        _registerContainer.SetActive(true);
        _loginContainer.SetActive(false);
    }
    #endregion
}