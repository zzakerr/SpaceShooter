using UnityEngine;
using Common;

[RequireComponent(typeof(CircleArea))]
public class EntitySpawnDerbies: MonoBehaviour
{
   
    [SerializeField] private Asteroids[] DebrisPrefabs;

    [SerializeField] private int numDebris;

    [SerializeField] private float randomSpeed;

    private CircleArea area;

    private void Start()
    {
        area = GetComponent<CircleArea>();
        for (int i = 0; i < numDebris; i++)
        {
            SpawnDebris();
        }
    }

    private void SpawnDebris()
    {
        int index = Random.Range(0, DebrisPrefabs.Length);

        Asteroids asteroids = Instantiate(DebrisPrefabs[index]);

        asteroids.transform.position = area.GetRandomInsideZone();

        asteroids.SetSize((Size)Random.Range(1, 3));

        asteroids.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

        Rigidbody2D rb = asteroids.GetComponent<Rigidbody2D>();
        if (rb != null && randomSpeed > 0)
        {
            rb.velocity = (Vector2) Random.insideUnitSphere * randomSpeed;
        }
    }

    private void OnDebrisDead()
    {
        SpawnDebris();
    }
}
