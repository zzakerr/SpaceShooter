using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

        if (ship != null)
        {
            OnPickedUp(ship);
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickedUp(SpaceShip ship);
}
