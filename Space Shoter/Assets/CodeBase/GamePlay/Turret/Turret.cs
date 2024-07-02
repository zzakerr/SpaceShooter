using UnityEngine;
using Common;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode mode;
    public TurretMode Mode => mode;

    [SerializeField] private TurretProperty turretProperty;

    private float refire_Timer;

    public bool CanFire => refire_Timer <= 0;

    private SpaceShip ship;

    private void Start()
    {
        ship = transform.root.GetComponent<SpaceShip>();
    }

    private void Update()
    {
        if (refire_Timer > 0) 
        {
            
        }
        refire_Timer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (turretProperty == null) return;

        if (refire_Timer > 0) return;

        if (ship.DrawEnergy(turretProperty.EnergyUsage) == false) return;

        if (ship.DrawAmmo(turretProperty.AmmoUsage) == false) return;

        Projectile projectile = Instantiate(turretProperty.ProjectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.up;

        projectile.SetParrentShoter(ship);

        refire_Timer = turretProperty.RateOfFire;
        
    }

    public void AssignLoadout(TurretProperty props)
    {
        if (mode != props.Mode) return;

        refire_Timer = 0;
        turretProperty = props;
    }
}
