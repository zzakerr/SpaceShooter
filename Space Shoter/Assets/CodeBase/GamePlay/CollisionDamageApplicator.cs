using System;
using UnityEngine;
using Common;

public class CollisionDamageApplicator : MonoBehaviour
{
    public static string IgnoreTag = "WorldBoundary";
    [SerializeField] private int DamageConstant;
    [SerializeField] private int DamageModifier;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == IgnoreTag) return;

        var destructible = transform.root.GetComponent<Destructible>();
        

        if (destructible != null)
        {
            destructible.ApplyDamage(Convert.ToInt32(collision.relativeVelocity.magnitude * DamageModifier + DamageConstant));
        }
    }
}
