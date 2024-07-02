using UnityEngine;
using Common;
[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : Destructible
{
    [Header("Space ship")]

    [SerializeField] private Sprite m_Previewimage;
    /// <summary>
    /// Масса для установки у Rigid
    /// </summary>
    [SerializeField] private float m_Mass;

    /// <summary>
    /// Толкающая сила вперёд
    /// </summary>
    [SerializeField] private float m_Trust;
    private float startTrust;

    /// <summary>
    /// Вращающая сила
    /// </summary>
    [SerializeField] private float m_Mobility;

    /// <summary>
    /// Максимальная линейная скорость
    /// </summary>
    [SerializeField] private float m_MaxLinearVelocity;

    /// <summary>
    /// Максимальная вращательная скорость. В градуса/сек
    /// </summary>
    [SerializeField] private float m_MaxAgularVelocity;

    /// <summary>
    /// Сохранённая ссылка на Rigid
    /// </summary>
    private Rigidbody2D m_Rigid;

    public float MaxLinearVelocity => m_MaxLinearVelocity;
    public float MaxAngularVelocity => m_MaxAgularVelocity;
    public Sprite Previewlmage => m_Previewimage;

    #region Public Api

    /// <summary>
    /// Управление линейной тягой. от -1 до +1
    /// </summary>
    public float ThrustControl { get; set; }

    /// <summary>
    /// Управление вращательной тягой. от -1 до +1
    /// </summary>
    public float TorqueControl { get; set; }

    #endregion

    #region Unity Event

    protected override void Start()
    {
        base.Start();
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;       
        m_Rigid.inertia = 1;
        startTrust = m_Trust;
        InitOffensive();
    }

    private float timer;
    private float speedTimer;
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (speedTimer < timer)
        {
            m_Trust = startTrust;
        }

        UpdateRigidBody();
        UpdateEnergyRegen();
    }

    #endregion

    /// <summary>
    /// Метод добавления сил кораблю для движения
    /// </summary>
    private void UpdateRigidBody()
    {
        m_Rigid.AddForce(ThrustControl * m_Trust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddForce(-m_Rigid.velocity * (m_Trust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAgularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    [SerializeField] private Turret[] turrets;


    public void Fire(TurretMode mode)
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i].Mode == mode)
            {
                turrets[i].Fire();
            }
        }
    }

    [SerializeField] private int maxEnergy;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int energyRegenPerSecond;

    private float primaryEnergy;
    private float secondaryAmmo;

    public void AddEnergy(int energy)
    {
        primaryEnergy = Mathf.Clamp(primaryEnergy + energy, 0, maxEnergy);
    }

    public void AddAmmo(int ammo)
    {
        secondaryAmmo = Mathf.Clamp(secondaryAmmo + ammo, 0, maxAmmo);
    }

    public void AddHealth(int health)
    {
        ApplyHeal(health);
    }

    public void SpeedUp(int value ,float timer)
    {
        this.timer = 0;
        speedTimer = timer;
        m_Trust += value;
    }

    public void Imortal(float timer)
    {
        ImortalityOn(timer);
    }

    private void InitOffensive()
    {
        primaryEnergy = maxEnergy;
        secondaryAmmo = maxAmmo;
    }

    private void UpdateEnergyRegen()
    {
        primaryEnergy += (float)energyRegenPerSecond * Time.deltaTime;
        primaryEnergy = Mathf.Clamp(primaryEnergy, 0, maxEnergy);
    }

    public bool DrawAmmo(int count)
    {
        if (count == 0) return true;

        if (secondaryAmmo >= count)
        {
            secondaryAmmo -= count;
            return true;
        }

        return false;
    }

    public bool DrawEnergy(int count)
    {
        if (count == 0) return true;

        if (primaryEnergy >= count)
        {
            primaryEnergy -= count;
            return true;
        }

        return false;
    }

    public void AssingWeapon(TurretProperty props)
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].AssignLoadout(props);
        }
    }
}
