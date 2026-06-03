using UnityEngine;

public class WaterBuff : MonoBehaviour, ITileBuff
{
    public int spdBonus = 2;

    public void ApplyBuff(PlayerPice player)
    {
        player.speed += spdBonus;
        Debug.Log($"?? Ô n??c — t?ng SPD +{spdBonus}! SPD: {player.speed}");
    }
}