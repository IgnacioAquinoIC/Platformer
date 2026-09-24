using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text collectibleText;
    public TMP_Text dashText;
    public PlayerMovement player;

    void Update()
    {
        UpdateCollectibles();
        UpdateDash();
    }

    void UpdateCollectibles()
    {
        collectibleText.text =
            "Collectibles: " + Collectible.Collected + " / " + Collectible.Total;
    }

    void UpdateDash()
    {
        if (player.dashReady)
        {
            dashText.text = "Dash: Ready";
        }
        else
        {
            dashText.text =
                "Dash: " + player.dashCooldownRemaining.ToString("0.0");
        }
    }
}