using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CopymonBall ball;
    InputSystem_Actions input;

    private void OnEnable()
    {
        ball = GetComponent<CopymonBall>();
        if (input == null)
        {
            input = new InputSystem_Actions();

            input.Player.Attack.performed += i => ball?.MouseClick();
            input.Player.Attack.canceled += i => ball?.MouseRelease();
            //characterInput.CharacterMovement.Movement.performed += i => playerController?.HandleMovementInput(i.ReadValue<Vector2>());
            //characterInput.CharacterActions.AButton.performed += i => playerSword?.StartAttack();
        }

        input.Enable();
    }

}
