using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject shipSelectionPanel;
    [SerializeField] private GameObject mainPanel;

    private void Start()
    {
        UI_ShowMainPanel();
    }
    public void UI_ShowMainPanel()
    {
        mainPanel.SetActive(true);
        shipSelectionPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
    }

    public void UI_ShowShipSelectionPanel()
    {
        shipSelectionPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void UI_ShowLevelSelection()
    {
        levelSelectionPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
