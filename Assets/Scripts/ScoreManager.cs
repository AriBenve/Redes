using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    int bluePoints;
    int redPoints;

    public void OnDeath(bool team)
    {
        if (team) bluePoints += 1;
        else redPoints += 1;
    }
}
