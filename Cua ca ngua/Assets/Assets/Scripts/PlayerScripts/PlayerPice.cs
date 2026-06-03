using System.Collections;
using UnityEngine;

public class PlayerPice : MonoBehaviour
{
    [Header("=== Player Piece ===")]
    public bool isPlayer;
    public int numberOfStepsToMove;
    public int location = 0;
    public int savePoint = 0;

    [Header("=== Player Stats ===")]
    public int MaxHealth = 20;
    public int playerHealth = 20;
    public int atk = 0;
    public int def = 0;
    public int speed = 0;

    [Header("=== Buff Mỗi Ô Đất ===")]
    public int grassAtkBonus = 2;
    public int waterSpdBonus = 2;
    public int stoneDefBonus = 3;
    public int healHpBonus = 10;

    [Header("=== Di Chuyển ===")]
    public float moveSpeed = 5f;

    public PathPointsObject pathPointsObject;
    private bool isMoving = false;

    private void Awake()
    {
        // Dùng FindFirstObjectByType thay FindObjectOfType (không còn deprecated)
        pathPointsObject = FindFirstObjectByType<PathPointsObject>();
    }

    void Update()
    {
        if (!isMoving || pathPointsObject == null) return;

        Vector3 target = pathPointsObject.CommanPath[location].transform.position;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            transform.position = target;
            isMoving = false;
        }
    }

    public void Move()
    {
        if (!isMoving)
            StartCoroutine(MoveSteps());
    }

    IEnumerator MoveSteps()
    {
        numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;
        for (int i = 0; i < numberOfStepsToMove; i++)
        {
            int nextIndex = location + 1;
            int pathLength = pathPointsObject.CommanPath.Length;
            if (nextIndex >= pathLength)
            {
                Debug.Log("Đã đến cuối bàn cờ!");
                GameManager.gm.NextPlayer();
                FindFirstObjectByType<RolingDice>().UnlockRoll(); // ✅
                yield break;
            }
            location = nextIndex;
            isMoving = true;
            yield return new WaitUntil(() => !isMoving);
            ApplyTileBuff(pathPointsObject.CommanPath[location].transform);
            yield return new WaitForSeconds(0.1f);
        }

        BattleManager.instance.CheckCollision(this);
        GameManager.gm.NextPlayer();
        FindFirstObjectByType<RolingDice>().UnlockRoll(); // ✅ mở khóa cho lượt tiếp
    }

    private void ApplyTileBuff(Transform waypoint)
    {
        TileData tile = waypoint.GetComponent<TileData>();
        TileType type = (tile != null) ? tile.tileType : TileType.Empty;

        switch (type)
        {
            case TileType.Grass:
                atk += grassAtkBonus;
                Debug.Log($"🌿 Ô cỏ — tăng ATK +{grassAtkBonus}! ATK: {atk}");
                break;

            case TileType.Water:
                speed += waterSpdBonus;
                Debug.Log($"💧 Ô nước — tăng SPD +{waterSpdBonus}! SPD: {speed}");
                break;

            case TileType.Stone:
                def += stoneDefBonus;
                Debug.Log($"🪨 Ô đá — tăng DEF +{stoneDefBonus}! DEF: {def}");
                break;

            case TileType.Heal:
                playerHealth = Mathf.Min(playerHealth + healHpBonus, MaxHealth);
                Debug.Log($"💚 Ô hồi máu +{healHpBonus}! HP: {playerHealth}/{MaxHealth}");
                break;

            default:
                Debug.Log("Ô trống.");
                break;
        }

        Debug.Log($"[Stats] HP:{playerHealth}/{MaxHealth} ATK:{atk} DEF:{def} SPD:{speed}");
    }
}