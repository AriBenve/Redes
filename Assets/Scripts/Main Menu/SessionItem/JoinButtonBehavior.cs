using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JoinButtonBehavior : MonoBehaviour
{
    Button _joinButton;

    SessionInfo _session;

    private void Awake()
    {
        _joinButton = GetComponent<Button>();

        var sessionProvider = GetComponentInParent<SessionItemDefinition.IProvider>();
        sessionProvider.OnSessionUpdate += RefreshSession;

        var runnerProvider = GetComponentInParent<RunnerHandler.IProvider>();
        runnerProvider.OnRunnerUpdate += RefreshRunner;
    }

    void RefreshSession(SessionItemDefinition sessionItem)
    {
        _joinButton.interactable = sessionItem.info.PlayerCount < sessionItem.info.MaxPlayers;
        _session = sessionItem.info;
    }

    void RefreshRunner(RunnerHandler runnerHandler)
    {
        _joinButton.onClick.AddListener(() => runnerHandler.JoinGame(_session));
    }
}
