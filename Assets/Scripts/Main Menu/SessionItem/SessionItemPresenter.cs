using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionItemPresenter : MonoBehaviour, SessionItemDefinition.IProvider, RunnerHandler.IProvider
{
    public event Action<SessionItemDefinition> OnSessionUpdate;
    public event Action<RunnerHandler> OnRunnerUpdate;

    public void Initialize(SessionInfo session, RunnerHandler runnerHandler)
    {
        OnSessionUpdate?.Invoke(new SessionItemDefinition(session));
        OnRunnerUpdate?.Invoke(runnerHandler);
    }

    //public void Initialize(SessionInfo session, RunnerHandler runnerHandler)
    //{
    //    _joinButton.interactable = session.PlayerCount < session.MaxPlayers;

    //    _joinButton.onClick.AddListener(() => runnerHandler.JoinGame(session));
    //}
}
