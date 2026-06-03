using UnityEngine;

// Ph?i n?m NGOÀI class, ? c?p namespace
public enum TileType
{
    Empty,
    Grass,
    Water,
    Stone,
    Heal,
}

public class TileData : MonoBehaviour
{
    public TileType tileType = TileType.Empty;

    public void ApplyBuff(PlayerPice player)
    {
        ITileBuff buff = tileType switch
        {
            TileType.Grass => GetComponent<GrassBuff>(),
            TileType.Water => GetComponent<WaterBuff>(),
            TileType.Stone => GetComponent<StoneBuff>(),
            TileType.Heal => GetComponent<HealBuff>(),
            _ => null
        };

        if (buff != null)
            buff.ApplyBuff(player);
        else
            Debug.Log("Ô tr?ng.");

        Debug.Log($"[Stats] HP:{player.playerHealth}/{player.MaxHealth} ATK:{player.atk} DEF:{player.def} SPD:{player.speed}");
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = tileType switch
        {
            TileType.Grass => new Color(0.2f, 0.8f, 0.2f),
            TileType.Water => new Color(0.2f, 0.5f, 1.0f),
            TileType.Stone => new Color(0.5f, 0.5f, 0.5f),
            TileType.Heal  => new Color(1.0f, 0.3f, 0.5f),
            _              => new Color(1f, 1f, 1f, 0.3f)
        };
        Gizmos.DrawSphere(transform.position + Vector3.up * 0.6f, 0.15f);
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.9f, tileType.ToString());
    }
#endif
}