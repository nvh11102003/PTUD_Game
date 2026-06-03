using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("=== Game Manager ===")]
    public int numberOfStepsToMove;
    public static GameManager gm;

    [Header("=== Player rotation ===")]
    public PlayerPice[] listPlayer;
    public int currentPlayerIndex = 0;

    private void Awake()
    {
        gm = this;
    }

    // G?i hàm này khi ng??i ch?i b?m nút tung xúc x?c xong
    public void StartCurrentPlayerMove()
    {
        PlayerPice current = listPlayer[currentPlayerIndex];

        // B? qua n?u quân ?ã b? lo?i
        if (!current.gameObject.activeSelf)
        {
            Debug.Log($"{current.name} ?ã b? lo?i, b? qua l??t!");
            NextPlayer();
            return;
        }

        current.Move();
    }

    // G?i hàm này sau khi quân ?i xong (g?i t? PlayerPice)
    public void NextPlayer()
    {
        int count = listPlayer.Length;

        // Tìm quân ti?p theo còn s?ng
        for (int i = 1; i <= count; i++)
        {
            int nextIndex = (currentPlayerIndex + i) % count;

            if (listPlayer[nextIndex].gameObject.activeSelf)
            {
                currentPlayerIndex = nextIndex;
                Debug.Log($"L??t c?a: {listPlayer[currentPlayerIndex].name}");
                return;
            }
        }

        Debug.Log("T?t c? quân ?ã b? lo?i!");
    }
}