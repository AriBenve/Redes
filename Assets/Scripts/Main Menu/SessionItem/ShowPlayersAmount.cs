using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ShowPlayersAmount : MonoBehaviour
{
    TMP_Text _textPlayers;

    private void Awake()
    {
        _textPlayers = GetComponent<TMP_Text>();

        var provider = GetComponentInParent<SessionItemDefinition.IProvider>();
        provider.OnSessionUpdate += Refresh;
    }

    void Refresh(SessionItemDefinition sessionItem)
    {
        _textPlayers.text = $"{sessionItem.info.PlayerCount} / {sessionItem.info.MaxPlayers}";
    }
}
