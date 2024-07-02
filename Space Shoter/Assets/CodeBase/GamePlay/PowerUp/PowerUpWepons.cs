using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWepons : PowerUp
{
    [SerializeField] private TurretProperty property;

    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.AssingWeapon(property);
    }
}
