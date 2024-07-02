using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelCompletionPosition : LevelCondicion
{
    [SerializeField] private float radius;

    public override bool IsCompleted
    {
        get
        {
            if (Player.Instance.ActiveShip == null) return false;
            if (Vector3.Distance(Player.Instance.ActiveShip.transform.position, this.transform.position) <= radius)
            {
                return true;
            }
            return false;
        }
        
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, radius);

    }
#endif
}
