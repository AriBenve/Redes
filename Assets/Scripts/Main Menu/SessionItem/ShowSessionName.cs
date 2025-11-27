using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ShowSessionName : MonoBehaviour
{
    TMP_Text _textName;

    private void Awake()
    {
        _textName = GetComponent<TMP_Text>();

        var provider = GetComponentInParent<SessionItemDefinition.IProvider>();
        provider.OnSessionUpdate += Refresh;
    }

    void Refresh(SessionItemDefinition sessionItem)
    {
        _textName.text = sessionItem.info.Name;
    }
}
