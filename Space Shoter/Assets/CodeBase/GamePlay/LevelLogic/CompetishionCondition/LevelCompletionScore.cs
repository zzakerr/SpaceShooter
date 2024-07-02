using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelCompletionScore : LevelCondicion
{
    [SerializeField] private int score;

    public override bool IsCompleted
    {
        get
        {
            if (Player.Instance.ActiveShip == null) return false;
            if (Player.Instance.Score >= score) return true;
            return false;
        }
        
    }

}
