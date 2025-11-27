using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknamesHandler : MonoBehaviour
{
    public static NicknamesHandler Instance { get; private set; }

    [SerializeField] NicknameItem _nicknamePrefab;

    List<NicknameItem> _nicknamesInUse;

    private void Awake()
    {
        Instance = this;
        _nicknamesInUse = new List<NicknameItem>();
    }

    public NicknameItem CreateNicknameItem(NicknameComponent owner)
    {
        var newNickname = Instantiate(_nicknamePrefab, transform);

        _nicknamesInUse.Add(newNickname);

        newNickname.SetTarget(owner.transform);

        owner.OnLeft += () =>
        {
            _nicknamesInUse.Remove(newNickname);

            Destroy(newNickname.gameObject);
        };

        return newNickname;
    }

    private void LateUpdate()
    {
        foreach (var nickname in _nicknamesInUse)
        {
            nickname.UpdatePosition();
        }
    }
}
