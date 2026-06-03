using UnityEngine;

public class StoneBuff : MonoBehaviour, ITileBuff
{
    public int defBonus = 3;

    public void ApplyBuff(PlayerPice player)
    {
        player.def += defBonus;
        Debug.Log($"?? Ô ?á — t?ng DEF +{defBonus}! DEF: {player.def}");
    }
}