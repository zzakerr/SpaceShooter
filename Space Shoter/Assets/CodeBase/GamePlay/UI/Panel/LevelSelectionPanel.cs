using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionPanel: MonoBehaviour
{
    [SerializeField] private LevelSelectionButton levelButtonPrefab;
    [SerializeField] private Transform parent;

    private void Start()
    {
        LevelProperties[] levelProperties = LevelSequensController.Instance.LevelSequences.LevelsProperties;

        for (int i = 0; i < levelProperties.Length; i++)
        {
            LevelSelectionButton levelSelectionButton = Instantiate(levelButtonPrefab);
            levelSelectionButton.SetLevelProperties(levelProperties[i]);
            levelSelectionButton.transform.SetParent(parent);
        }
    }
   
}
