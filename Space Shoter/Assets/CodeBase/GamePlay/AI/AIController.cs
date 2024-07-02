using UnityEditor;
using UnityEngine;
using Common;

public enum AIBehavior
{
    Null,
    RandomPatrol,
    SetPatrol
}

[RequireComponent(typeof(SpaceShip))]
public class AIController : MonoBehaviour
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AIController))]
    class MyClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AIController self = (AIController)target;
            serializedObject.Update();
            if (self.behavior == AIBehavior.Null)
            {
                DrawPropertiesExcluding(serializedObject, "setPatrolPooints", "patrolPoint", "navigationLinear", "navigationAngular",
                    "randomSelectMovePointTimer", "findNewTargetTime", "shotDelay", "evadeRayLenght");
            }
            if (self.behavior == AIBehavior.RandomPatrol)
            {
                DrawPropertiesExcluding(serializedObject, "setPatrolPooints");
            }
            if (self.behavior == AIBehavior.SetPatrol)
            {
                DrawPropertiesExcluding(serializedObject, "patrolPoint", "randomSelectMovePointTimer" );
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    [Header("Тип поведения Ai")]
    [SerializeField] private AIBehavior behavior;
    [Space]

    [Header("Точки патрулирования")]
    [SerializeField] private AISetPointsPatrol setPatrolPooints;
    [Space]

    [Header("Зона патрулирования")]
    [SerializeField] private AIPointPatrol patrolPoint;
    [Space]

    [Header("Таймер Поиска новой точки")]
    [SerializeField] private float randomSelectMovePointTimer;

    [Header("Таймер поиска новой Цели")]
    [SerializeField] private float findNewTargetTime;

    [Header("Скорость стрельбы")]
    [SerializeField] private float shotDelay;

    [Header("Длинна луча до препятствия")]
    [SerializeField] private float evadeRayLenght;
    [Space]

    [Header("Скорость перемещения")]
    [Range(0f, 1f)]
    [SerializeField] private float navigationLinear;

    [Header("Скорость Вращения")]
    [Range(0f, 1f)]
    [SerializeField] private float navigationAngular;

    private SpaceShip spaceShip;

    private Vector3 movePos;

    private Destructible selectedTarget;

    private Timer RandomizeDirectionTimer;
    
    private Timer FireTimer;

    private Timer FindNewTargetTimer;

    private Rigidbody2D rbMoveTarget;

    private int pointsValue;

    private int currentPoints;

    private float radiusMax;

    private void Start()
    {
        radiusMax = GetComponent<CircleArea>().Radius;
        spaceShip = GetComponent<SpaceShip>();
        InitTimers();
        if (setPatrolPooints != null )
        {
            pointsValue = setPatrolPooints.Points.Count;
            movePos = setPatrolPooints.Points[currentPoints];
        }      
    }

    private void Update()
    {
        UpdateTimers();
        UpdateAI();
    }

    private void UpdateAI()
    {
        if (behavior == AIBehavior.Null)
        {
            Debug.Log("Мне нечего делать");
        }

        if (behavior == AIBehavior.RandomPatrol)
        {
            UpdateBehaviorPatrol();
        }
        if (behavior == AIBehavior.SetPatrol)
        {
            UpdateBehaviorPatrol();
        }
    }

    private void UpdateBehaviorPatrol()
    {
        ActionFindMovePosition();
        ActionControlShip();
        ActionFindNewAttacTarget();
        ActionFire();
    }

    private void ActionFindMovePosition()
    {
        if(behavior == AIBehavior.RandomPatrol)
        {
            if(selectedTarget != null)
            {
                if (rbMoveTarget == null)
                {
                    rbMoveTarget = selectedTarget.GetComponent<Rigidbody2D>();
                }

                movePos =(Vector2)selectedTarget.transform.position + rbMoveTarget.velocity;
            }
            else
            {
                if (patrolPoint != null)
                {
                    if (rbMoveTarget != null)
                    {
                        rbMoveTarget = null;
                    }

                    bool isInsidePatrolZone = (patrolPoint.transform.position - transform.position).sqrMagnitude < patrolPoint.Radius * patrolPoint.Radius;
                    if (isInsidePatrolZone)
                    {
                        if(RandomizeDirectionTimer.IsFinished == true)
                        {
                            Vector2 newPoint = Random.onUnitSphere * patrolPoint.Radius + patrolPoint.transform.position;

                            movePos = newPoint;

                            RandomizeDirectionTimer.Start(randomSelectMovePointTimer);
                        }
                       
                    }
                    else
                    {
                        movePos = patrolPoint.transform.position;
                    }
                }
            }
        }
        if (behavior == AIBehavior.SetPatrol)
        {
            if(selectedTarget != null)
            {
                if (rbMoveTarget == null)
                {
                    rbMoveTarget = selectedTarget.GetComponent<Rigidbody2D>();
                }

                movePos = (Vector2)selectedTarget.transform.position + rbMoveTarget.velocity;
            }
            else
            {
                if (rbMoveTarget != null)
                {
                    rbMoveTarget = null;
                }

                if (setPatrolPooints != null)
                {                   
                    if (Mathf.Round(transform.position.x) == Mathf.Round(movePos.x) && Mathf.Round(transform.position.y) == Mathf.Round(movePos.y))
                    {
                        if(currentPoints >= pointsValue-1)
                        {
                            currentPoints = 0;    
                        }
                        else
                        {
                            currentPoints++;
                        }
                    }
                    movePos = setPatrolPooints.Points[currentPoints];
                }
                else
                {
                    Debug.Log("нет точки патруля");
                }
            }
        }
    }

    private void ActionEvadeCollision()
    {
        if (Physics2D.Raycast(transform.position, transform.up, evadeRayLenght) == true)
        {
            movePos = transform.position + transform.right * 100.0f;
        }
    }


    private void ActionControlShip()
    {
        spaceShip.ThrustControl = navigationLinear;

        spaceShip.TorqueControl = ComputeAliginTorqueNormalized(movePos,spaceShip.transform) * navigationAngular;
    }

    private const float MAX_ANGLE = 45.0f;

    private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
    {
        Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

        float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

        angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

        return -angle;  
    }

    private void ActionFindNewAttacTarget()
    {
        if (FindNewTargetTimer.IsFinished == true)
        {
            selectedTarget = FindNearestDestructibleTarget();

            FindNewTargetTimer.Start(findNewTargetTime);
        }
    }

    private void ActionFire()
    {
        if(selectedTarget != null)
        {
            if (FireTimer.IsFinished)
            {
                spaceShip.Fire(TurretMode.Primary);

                FireTimer.Start(shotDelay);
            }
        }
    }

    private Destructible FindNearestDestructibleTarget()
    {
        float maxDist = radiusMax;

        Destructible potentialTarget = null;

        foreach (var v in Destructible.AllDestructibles)
        {
            if (v.GetComponent<SpaceShip>() == spaceShip) continue;

            if (v.TeamId == Destructible.TeamIDNeutral) continue;

            if (v.TeamId == spaceShip.TeamId) continue;

            float dist = Vector2.Distance(spaceShip.transform.position, v.transform.position);

            if (dist < maxDist)
            {
                maxDist = dist;
                potentialTarget = v;
            }
        }

        return potentialTarget;
    }


    private void InitTimers()
    {
        RandomizeDirectionTimer = new Timer(randomSelectMovePointTimer);
        FindNewTargetTimer = new Timer(findNewTargetTime);
        FireTimer = new Timer(shotDelay);
    }

    private void UpdateTimers()
    {
        RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
        FindNewTargetTimer.RemoveTime(Time.deltaTime);
        FireTimer.RemoveTime(Time.deltaTime);
    }

    public void SetAiPatrol(AIPointPatrol point)
    {
        behavior = AIBehavior.RandomPatrol;
        patrolPoint = point;
    }

    public void SetAiPatrolSet(AISetPointsPatrol point)
    {
        behavior = AIBehavior.SetPatrol;
        setPatrolPooints = point;
        pointsValue = setPatrolPooints.Points.Count;
        movePos = setPatrolPooints.Points[currentPoints];
    }

    public void SetAiNull()
    {
        behavior = AIBehavior.Null;
    }
}
