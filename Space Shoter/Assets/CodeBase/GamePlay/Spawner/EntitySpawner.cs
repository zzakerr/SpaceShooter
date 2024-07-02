using UnityEditor;
using UnityEngine;
using Common;

[RequireComponent(typeof(CircleArea))]
public class EntitySpawner: MonoBehaviour
{
    public enum SpawnMode
    {
        Start,
        Loop
    }

    [CustomEditor(typeof(EntitySpawner))]
    class MyClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EntitySpawner self = (EntitySpawner)target;
            serializedObject.Update();
            if (self.typeAI == AIBehavior.Null)
            {
                DrawPropertiesExcluding(serializedObject,"patrolPoint",
                    "respawnTime", "aIPointPatrol", "AISetPointsPatrol");
            }
            if (self.typeAI == AIBehavior.RandomPatrol)
            {
                DrawPropertiesExcluding(serializedObject, "AISetPointsPatrol");
            }
            if (self.typeAI == AIBehavior.SetPatrol)
            {
                DrawPropertiesExcluding(serializedObject, "aIPointPatrol");
            }
            serializedObject.ApplyModifiedProperties();
        }
    }

    [SerializeField] private Entity[] entitiesPrefabs;

    [SerializeField] SpawnMode spawnMode;

    [SerializeField] AIBehavior typeAI;

    [SerializeField] private int numSpawns;

    [SerializeField] private float respawnTime;

    [SerializeField] private int teamID;

    [SerializeField] private AIPointPatrol aIPointPatrol;

    [SerializeField] private AISetPointsPatrol AISetPointsPatrol;

    private CircleArea area;

    private float timer;

    private void Start()
    {
        area = GetComponent<CircleArea>();
        if (spawnMode == SpawnMode.Start)
        {
            SpawnEntities();
        }     
        timer = respawnTime;
    }


    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;

        if (spawnMode == SpawnMode.Loop && timer < 0)
        {
            SpawnEntities();

            timer = respawnTime;
        }
    }

    private void SpawnEntities()
    {
        for (int i = 0; i < numSpawns; i++)
        {
            int index = Random.Range(0,entitiesPrefabs.Length);

            GameObject e = Instantiate(entitiesPrefabs[index].gameObject);

            e.GetComponent<Destructible>().SetTeamID(teamID);

            if (typeAI == AIBehavior.SetPatrol)
            {
                if (AISetPointsPatrol != null)
                {
                    e.GetComponent<AIController>().SetAiPatrolSet(AISetPointsPatrol);
                }
            }
            if (typeAI == AIBehavior.RandomPatrol)
            {
                if (aIPointPatrol != null)
                {
                    e.GetComponent<AIController>().SetAiPatrol(aIPointPatrol);
                }
            }

            if (typeAI == AIBehavior.Null)
            {
                e.GetComponent<AIController>().SetAiNull();
            }
            
            e.transform.position = area.GetRandomInsideZone();
        }
    }
}
