using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    
    [SerializeField] private Text levelTitle;
    [SerializeField] private Image previewImage;

    private LevelProperties levelProperties;

    public void SetLevelProperties(LevelProperties levelProperties)
    {
        this.levelProperties = levelProperties;
        previewImage.sprite = levelProperties.Previewlmage;
        levelTitle.text = levelProperties.Title;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelProperties.SceneName);
    }
}
