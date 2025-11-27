using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameComponent : NetworkBehaviour
{
    NicknameItem _myNickname;

    [Networked, OnChangedRender(nameof(NicknameChanged))] 
    NetworkString<_16> CurrentNickname { get; set; }

    public event Action OnLeft;

    public override void Spawned()
    {
        _myNickname = NicknamesHandler.Instance.CreateNicknameItem(this);

        if (HasInputAuthority)
        {
            NetworkString<_16> loadedNickname = "Unknown Player";

            if (PlayerPrefs.HasKey("Nickname"))
            {
                loadedNickname = PlayerPrefs.GetString("Nickname");
            }

            RPC_LoadNickname(loadedNickname);
        }
        else if (IsProxy)
        {
            NicknameChanged();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    void RPC_LoadNickname(NetworkString<_16> loadedNickname)
    {
        CurrentNickname = loadedNickname;
    }

    void NicknameChanged()
    {
        _myNickname.UpdateNickname(CurrentNickname.Value);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft?.Invoke();
    }
}
