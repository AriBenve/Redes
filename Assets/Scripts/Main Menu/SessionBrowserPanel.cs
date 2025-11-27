using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionBrowserPanel : MonoBehaviour
{
    [SerializeField] RunnerHandler _runnerHandler;
    [SerializeField] SessionItemPresenter _sessionItemPrefab;
    [SerializeField] VerticalLayoutGroup _content;

    [SerializeField] TMP_Text _statusText;

    private void OnEnable()
    {
        _runnerHandler.OnSessionsUpdate += UpdateList;
    }

    private void OnDisable()
    {
        _runnerHandler.OnSessionsUpdate -= UpdateList;
    }


    void UpdateList(List<SessionInfo> sessions)
    {
        ClearContent();

        if (sessions.Count == 0)
        {
            _statusText.gameObject.SetActive(true);
            return;
        }

        _statusText.gameObject.SetActive(false);

        foreach (var sessionInfo in sessions)
        {
            var sessionItem = Instantiate(_sessionItemPrefab, _content.transform);
            sessionItem.Initialize(sessionInfo, _runnerHandler);
        }
    }

    void ClearContent()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
