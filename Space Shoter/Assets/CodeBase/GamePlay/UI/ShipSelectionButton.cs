using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectionButton : MonoBehaviour
{
    [SerializeField] private MainMenu menu;
    [SerializeField] private SpaceShip prefab;

    [SerializeField] private Text shipName;
    [SerializeField] private Text hitpoints;
    [SerializeField] private Text speed;
    [SerializeField] private Text agility;
    [SerializeField] private Image preview;

    private void Start()
    {
        if (prefab == null) return;

        shipName.text = prefab.Nickname;
        hitpoints.text = "HP :" + prefab.MaxHitPoints.ToString();
        speed.text = "Speed :" + prefab.MaxLinearVelocity.ToString();
        agility.text = "Agility :" + prefab.MaxAngularVelocity.ToString();
        preview.sprite = prefab.Previewlmage;
    }

    public void SelectShip()
    {
        Player.SelectedSpaceShip = prefab;
        menu.UI_ShowMainPanel();
    }
}
