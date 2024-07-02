using UnityEngine;
using Common;

public class Player : SingletonBase<Player>
{
    public static SpaceShip SelectedSpaceShip;

    [SerializeField] private int m_NumLives;
    
    [SerializeField] private SpaceShip m_PlayerShipPref;

    private SpaceShip m_SpaceShip;
    private FollowCamera m_FollowCamera;
    private ShipInputController m_ShipInputController;
    private Transform m_SpawnPoint;

    private int score;
    private int numKill;

    public int Score => score;
    public int NumKill => numKill;
    public int NumLives => m_NumLives;
    public SpaceShip ActiveShip => m_SpaceShip;
    public FollowCamera FollowCamera => m_FollowCamera;

    public void Construct(FollowCamera followCamera,ShipInputController shipInputController,Transform spawnPoint)
    {
        m_FollowCamera = followCamera;
        m_ShipInputController = shipInputController;
        m_SpawnPoint = spawnPoint;
    }


    public SpaceShip ShipPrefab 
    { 
        get
        {
            if(SelectedSpaceShip == null)
            {
                return m_PlayerShipPref;
            }
            else
            {
                return SelectedSpaceShip;
            }
        }
       
    }

    private void Start()
    {   
        Respawn();  
    }

    private void OnShipDeath()
    {
        m_SpaceShip.EventOnDeath.RemoveAllListeners();
        m_NumLives--;
        if (m_NumLives > 0) Respawn();
    }

    private void Respawn()
    {
        var newPlayerShip = Instantiate(ShipPrefab , m_SpawnPoint.position, m_SpawnPoint.rotation);

        m_SpaceShip = newPlayerShip.GetComponent<SpaceShip>();
        m_FollowCamera.SetTarget(m_SpaceShip.transform);
        m_ShipInputController.SetTargetShip(m_SpaceShip);
        m_SpaceShip.EventOnDeath.AddListener(OnShipDeath);
    }

    public void AddKill()
    {
        numKill++;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }
}
