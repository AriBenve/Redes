using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] RunnerHandler _runnerHandler;

    [Header("Panels")]
    [SerializeField] private GameObject _initialPanel;
    [SerializeField] private GameObject _joiningPanel;
    [SerializeField] private GameObject _sessionBrowserPanel;
    [SerializeField] private GameObject _hostPanel;

    [Header("Buttons")]
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _goToHostPanelButton;
    [SerializeField] private Button _hostButton;

    [Header("InputFields")]
    [SerializeField] TMP_InputField _sessionNameField;
    [SerializeField] TMP_InputField _nicknameField;

    private void Awake()
    {
        _joinButton.onClick.AddListener(AskToJoinLobby);

        _goToHostPanelButton.onClick.AddListener(() =>
        {
            _sessionBrowserPanel.SetActive(false);
            _hostPanel.SetActive(true);
        });

        _hostButton.onClick.AddListener(StartGameAsHost);

        _runnerHandler.OnJoinedLobbySuccesfully += () =>
        {
            _joiningPanel.SetActive(false);
            _sessionBrowserPanel.SetActive(true);
        };
    }

    void AskToJoinLobby()
    {
        PlayerPrefs.SetString("Nickname", _nicknameField.text);

        _joinButton.interactable = false;
        _runnerHandler.JoinLobby();

        _initialPanel.SetActive(false);
        _joiningPanel.SetActive(true);
    }

    void StartGameAsHost()
    {
        _hostButton.interactable = false;
        _runnerHandler.HostGame(_sessionNameField.text, "SampleScene");
    }
}
