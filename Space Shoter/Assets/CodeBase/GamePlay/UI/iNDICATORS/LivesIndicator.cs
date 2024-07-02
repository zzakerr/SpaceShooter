using UnityEngine;
using UnityEngine.UI;

public class LivesIndicator : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image icon;

    private float lastSLives;

    private void Start()
    {
        icon.sprite = Player.Instance.ActiveShip.Previewlmage;
    }
    private void Update()
    {
        int lives = Player.Instance.NumLives;
        if (lastSLives != lives)
        {
            text.text = lives.ToString();
            lastSLives = lives;
        }  
    }
}
