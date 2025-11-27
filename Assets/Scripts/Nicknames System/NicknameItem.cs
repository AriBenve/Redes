using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class NicknameItem : MonoBehaviour
{
    [SerializeField] float _yOffset = 2;

    TMP_Text _myText;

    Transform _target;

    private void Awake()
    {
        _myText = GetComponent<TMP_Text>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void UpdateNickname(string newNick)
    {
        _myText.text = newNick;
    }

    public void UpdatePosition()
    {
        transform.position = _target.position + Vector3.up * _yOffset;
    }
}
