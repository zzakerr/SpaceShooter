using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStats : PowerUp
{
    public enum EffectType
    {
        AddAmmo,
        AddEnergy,
        AddHealth,
        SpeedUp,
        Imortal
    }

    [Header("Тип Баффа")]
    [SerializeField] private EffectType type;

    [Header("Количество")]
    [SerializeField] private float value;

    [Header("Время действия")]
    [SerializeField] private float time;


    protected override void OnPickedUp(SpaceShip ship)
    {
        if (type == EffectType.AddEnergy)
        {
            ship.AddEnergy((int)value);
        }

        if(type == EffectType.AddAmmo)
        {
            ship.AddAmmo((int)value);
        }

        if (type == EffectType.AddHealth)
        {
            ship.AddHealth((int)value);
        }

        if (type == EffectType.SpeedUp)
        {
            ship.SpeedUp((int)value, time);
        }

        if (type == EffectType.Imortal)
        {
            ship.Imortal(time);
        }
    }
}
