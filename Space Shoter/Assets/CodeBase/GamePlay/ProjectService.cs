using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectService : MonoBehaviour
{
    [SerializeField] private LevelSequensController sequensController;

    private void Awake()
    {
        sequensController.Init();
    }
}
