using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSequensController : SingletonBase<LevelSequensController>
{
    public LevelSequences LevelSequences;
    
    public bool CurrentLevelIsLast()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string lastLevelSceneName = LevelSequences.LevelsProperties[LevelSequences.LevelsProperties.Length - 1].SceneName;
        return lastLevelSceneName == sceneName;
    }

    public LevelProperties GetCurrentLoadedLevel()
    {
        string sceneNmae = SceneManager.GetActiveScene().name;

        for (int i = 0;i < LevelSequences.LevelsProperties.Length; i++)
        {
            if (LevelSequences.LevelsProperties[i].SceneName == sceneNmae)
                return LevelSequences.LevelsProperties[i];
        }

        Debug.LogError("LEVEL PROPERTIES NOT FOUND");
        return null;
    }

    public LevelProperties GetNextLevelProperties(LevelProperties levelProperties)
    {
        for (int i = 0; i < LevelSequences.LevelsProperties.Length; i++)
        {
            if (LevelSequences.LevelsProperties[i].SceneName == levelProperties.SceneName)
            {
                if (i < LevelSequences.LevelsProperties.Length - 1)
                    return LevelSequences.LevelsProperties[i + 1];
            }
        }
        Debug.LogError("LEVEL PROPERTIES is Last");
        return null;
    }
}
