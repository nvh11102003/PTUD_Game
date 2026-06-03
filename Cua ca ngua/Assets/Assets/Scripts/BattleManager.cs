using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("=== Cài ??t tr?n ??u ===")]
    public float battleDelay = 1f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void CheckCollision(PlayerPice mover)
    {
        PlayerPice[] allPlayers = FindObjectsByType<PlayerPice>(FindObjectsSortMode.None);

        foreach (PlayerPice other in allPlayers)
        {
            if (other == mover) continue;
            if (!other.gameObject.activeSelf) continue;

            if (other.location == mover.location)
            {
                StartCoroutine(ResolveBattle(mover, other));
                return;
            }
        }
    }

    IEnumerator ResolveBattle(PlayerPice attacker, PlayerPice defender)
    {
        Debug.Log($"?? ??ng ??! {attacker.name} vs {defender.name}");

        yield return new WaitForSeconds(battleDelay);

        int rollA = RollDice();
        int rollD = RollDice();

        Debug.Log($"?? {attacker.name} gieo: {rollA} | {defender.name} gieo: {rollD}");

        if (rollA == rollD)
        {
            Debug.Log("?? Hòa! Gieo l?i...");
            StartCoroutine(ResolveBattle(attacker, defender));
            yield break;
        }

        PlayerPice loser = rollA < rollD ? attacker : defender;
        PlayerPice winner = rollA < rollD ? defender : attacker;

        Debug.Log($"?? {winner.name} th?ng! {loser.name} v? save point!");

        SendToSavePoint(loser, winner);
    }

    private void SendToSavePoint(PlayerPice loser, PlayerPice winner)
    {
        // Sát th??ng = ATK winner - DEF loser, t?i thi?u 1
        int damage = Mathf.Max(winner.atk - loser.def, 1);

        loser.playerHealth -= damage;
        loser.playerHealth = Mathf.Max(loser.playerHealth, 0);

        Debug.Log($"?? {winner.name} ATK:{winner.atk} vs {loser.name} DEF:{loser.def} ? sát th??ng: {damage}");
        Debug.Log($"?? {loser.name} HP còn: {loser.playerHealth}/{loser.MaxHealth}");

        // V? save point, gi? nguyên ATK/DEF/SPD
        loser.location = loser.savePoint;
        loser.transform.position =
            loser.pathPointsObject.CommanPath[loser.savePoint].transform.position;

        Debug.Log($"?? {loser.name} v? ô {loser.savePoint}!");

        if (loser.playerHealth <= 0)
        {
            Debug.Log($"?? {loser.name} ?ã ch?t!");
            loser.gameObject.SetActive(false);
        }
    }

    private int RollDice() => Random.Range(1, 7);
}