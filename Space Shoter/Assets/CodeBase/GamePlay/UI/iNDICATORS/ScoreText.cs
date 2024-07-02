using UnityEngine;
using UnityEngine.UI;

public class ScoreText: MonoBehaviour
{
    [SerializeField] private Text text;

    private float lastScore;

    private void Update()
    {
        int score = Player.Instance.Score;
        if (lastScore != score)
        {
            text.text = "Score:" + score.ToString();
            lastScore = score;
        }  
    }
}
