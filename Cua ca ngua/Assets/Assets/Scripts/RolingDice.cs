using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class RolingDice : MonoBehaviour
{
    [SerializeField] Sprite[] numberSprite;
    [SerializeField] SpriteRenderer numberSpriteholder;
    [SerializeField] int numberGot = 0;

    private bool hasRolled = false; // tránh b?m nhi?u l?n 1 l??t
    InputAction inputActions;

    private void Start()
    {
        inputActions = new InputAction("RollDice", binding: "<Mouse>/leftButton");
        inputActions.Enable();
    }

    private void Update()
    {
        if (inputActions.triggered && !hasRolled)
        {
            RollTheDice();
            DiceToMove(numberGot);
        }
    }

    public void DiceToMove(int number)
    {
        Debug.Log($"?? ?i {number} b??c — l??t c?a: {GameManager.gm.listPlayer[GameManager.gm.currentPlayerIndex].name}");
        GameManager.gm.numberOfStepsToMove = number;

        hasRolled = true; // khóa không cho b?m ti?p
        GameManager.gm.StartCurrentPlayerMove(); // ? dùng hàm m?i
    }

    public void RollTheDice()
    {
        numberGot = Random.Range(1, 6);
        numberSpriteholder.sprite = numberSprite[numberGot];
        numberGot++;
    }

    // G?i hàm này sau khi quân ?i xong ?? m? khóa l??t ti?p
    public void UnlockRoll()
    {
        hasRolled = false;
    }
}