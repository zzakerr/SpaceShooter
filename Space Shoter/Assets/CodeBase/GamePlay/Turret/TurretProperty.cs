using UnityEngine;
using Common;

public enum TurretMode
{
    Primary,
    Secondary
}

[CreateAssetMenu]
public sealed class TurretProperty : ScriptableObject
{
    [SerializeField] private TurretMode mode;
    public TurretMode Mode => mode;

    [SerializeField] private ProjectileBase projectilePrefab;
    public ProjectileBase ProjectilePrefab => projectilePrefab;

    [SerializeField] private float rateOfFire;
    public float RateOfFire => rateOfFire;

    [SerializeField] private int energyUsage;
    public int EnergyUsage => energyUsage;

    [SerializeField] private int ammoUsage;
    public int AmmoUsage => ammoUsage;

    [SerializeField] private AudioClip launchSFX;
    public AudioClip LaunchSFX => launchSFX;

}
