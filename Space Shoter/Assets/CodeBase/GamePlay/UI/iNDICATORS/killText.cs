using UnityEngine;
using UnityEngine.UI;

public class killText : MonoBehaviour
{
    [SerializeField] private Text text;

    private float lastNumKills;

    private void Update()
    {
        int numkils = Player.Instance.NumKill;
        if (lastNumKills != numkils)
        {
            text.text = "Kills:" + numkils.ToString();
            lastNumKills = numkils;
        }  
    }
}
