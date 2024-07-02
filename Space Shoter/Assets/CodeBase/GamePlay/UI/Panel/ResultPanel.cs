using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    private const string PassedText = "Passed";
    private const string LoseText = "Lose";
    private const string RestartText = "Restart";
    private const string NextText = "Next";
    private const string MainMenuText = "Main menu";
    private const string KillsTextPrefix = "Kills : ";
    private const string ScoreTextPrefix = "Scores : ";
    private const string TimeTextPrefix = "Time : ";

    [SerializeField] private Text kills;
    [SerializeField] private Text score;
    [SerializeField] private Text time;
    [SerializeField] private Text result;
    [SerializeField] private Text buttonNextText;

    private bool levelPased = false;

    private void Start()
    {
        gameObject.SetActive(false);
        LevelController.Instance.LevelLost += OnlevelLost;
        LevelController.Instance.LevelPassed += OnlevelPassed;
    }

    private void OnDestroy()
    {
        LevelController.Instance.LevelLost -= OnlevelLost;
        LevelController.Instance.LevelLost -= OnlevelPassed;
    }

    private void OnlevelPassed()
    {
        gameObject.SetActive(true);

        levelPased = true;

        FillLevelStatistics();

        result.text = PassedText;

        if(LevelSequensController.Instance.CurrentLevelIsLast() == true)
        {
            buttonNextText.text = MainMenuText;
            
        }
        else
        {
            buttonNextText.text = NextText;
        }
    }

   
    private void OnlevelLost()
    {
        gameObject.SetActive(true);

        FillLevelStatistics();

        result.text = LoseText;

        buttonNextText.text = RestartText;
    }

    private void FillLevelStatistics()
    {
        kills.text = KillsTextPrefix + Player.Instance.NumKill.ToString();
        score.text = ScoreTextPrefix + Player.Instance.Score.ToString();
        time.text = TimeTextPrefix + (LevelController.Instance.LevelTime).ToString("F0");
    }

    public void OnButtonNextAction()
    {
        gameObject.SetActive (true);

        if(levelPased == true)
        {
            LevelController.Instance.LoadNextLevel();
        }
        else
        {
            LevelController.Instance.RestartLevel();
        }
    }
}
