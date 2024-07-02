using UnityEngine;

[System.Serializable]
public class LevelProperties
{
    [SerializeField] private string titel;
    [SerializeField] private string sceneName;
    [SerializeField] private Sprite previewImage;
    [SerializeField] private LevelProperties nextLevl;

    public string Title => titel;
    public string SceneName => sceneName;
    public Sprite Previewlmage => previewImage;

}
