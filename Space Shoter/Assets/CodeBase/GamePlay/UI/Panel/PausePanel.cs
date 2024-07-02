using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        HidePause();
    }

    public void ShowPause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePause()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void LoadMainMenu()
    {
        panel.SetActive(true);
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
}
