using UnityEngine;

public class HealBuff : MonoBehaviour, ITileBuff
{
    public int hpBonus = 10;

    public void ApplyBuff(PlayerPice player)
    {
        player.playerHealth = Mathf.Min(player.playerHealth + hpBonus, player.MaxHealth);
        Debug.Log($"?? Ô h?i máu +{hpBonus}! HP: {player.playerHealth}/{player.MaxHealth}");
    }
}