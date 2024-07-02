using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Projectile : ProjectileBase
{
    [Header("Импак при столкновении")]
    [SerializeField] private ImpactEffect impactEffectPrefab;

    protected override void Onhit(Destructible destructible)
    {
        if (parrent == Player.Instance.ActiveShip)
        {
            if (destructible.HitPoints <= 0)
            {
                Player.Instance.AddScore(destructible.ScoreValue);
            }

            if (destructible is SpaceShip)
            {
                if (destructible.HitPoints <= 0)
                {    
                    Player.Instance.AddKill();
                }
            }
        }
    }

    protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        if (impactEffectPrefab != null)
        {
            Instantiate(impactEffectPrefab,pos,Quaternion.identity);
        }
        Destroy(gameObject, 0);
    }

}
