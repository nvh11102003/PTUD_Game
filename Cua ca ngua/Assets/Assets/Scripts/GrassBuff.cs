using UnityEngine;

public class GrassBuff : MonoBehaviour, ITileBuff
{
    public int atkBonus = 2;

    public void ApplyBuff(PlayerPice player)
    {
        player.atk += atkBonus;
        Debug.Log($"?? Ô c? — t?ng ATK +{atkBonus}! ATK: {player.atk}");
    }
}