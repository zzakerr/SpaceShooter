using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : SingletonBase<LevelController>
{
    private const string MainMenuScene = "Main_Menu";

    public event UnityAction LevelPassed;
    public event UnityAction LevelLost;
    

    [SerializeField] private LevelCondicion[] condition;

    private bool islevelCompleted;
    private float levelTime;
    private LevelSequensController sequensController;
    private LevelProperties currentLevelProperties;

    public float LevelTime => levelTime;

    private void Start()
    {
        Time.timeScale = 1.0f;
        levelTime = 0;
        sequensController = LevelSequensController.Instance;
        currentLevelProperties = sequensController.GetCurrentLoadedLevel();
    }

    private void Update()
    {
        if (islevelCompleted == false)
        {
            levelTime += Time.deltaTime;
            CheckLvlConditions();

        }
      
        if (Player.Instance.NumLives == 0)
        {
            Lose();
        }
    }

    private void CheckLvlConditions()
    {
       
        int numComleted = 0;

        for (int i = 0; i < condition.Length; i++)
        {
            if (condition[i].IsCompleted == true)
            {
                numComleted++;
            }
        }

        if (numComleted == condition.Length)
        {
            islevelCompleted = true;

            Pass();
        }
    }

    private void Lose()
    {
        LevelLost.Invoke();
        Time.timeScale = 0;
    }

    private void Pass()
    {
        LevelPassed.Invoke();
        Time.timeScale = 0;
    }

    public void LoadNextLevel()
    {


        if(sequensController.CurrentLevelIsLast() == false)
        {
            string nextLevelSceneName = sequensController.GetNextLevelProperties(currentLevelProperties).SceneName;

            SceneManager.LoadScene(nextLevelSceneName);
        }
        else
        {
            SceneManager.LoadScene(MainMenuScene);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevelProperties.SceneName);
    }
}
