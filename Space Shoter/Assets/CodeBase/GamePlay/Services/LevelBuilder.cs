using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject PlayerHUDPrefab;
    [SerializeField] private GameObject LevelGUIPrefab;
    [SerializeField] private GameObject BackgroundPrefab;

    [Header("Dependencies")]
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private LevelBoundary levelBoundary;
    [SerializeField] private LevelController levelController;

    private void Awake()
    {
        levelBoundary.Init();
        levelController.Init();

        Player player = playerSpawner.Spawn();

        player.Init();

        Instantiate(PlayerHUDPrefab);
        Instantiate(LevelGUIPrefab);

        GameObject background = Instantiate(BackgroundPrefab);
        background.AddComponent<SyncTransform>().SetTarget(player.FollowCamera.transform);
    }


}
