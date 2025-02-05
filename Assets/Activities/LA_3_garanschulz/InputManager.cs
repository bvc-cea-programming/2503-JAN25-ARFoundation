using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CopymonBall ball;
    private g_ObjectPlacementManager chair;
    InputSystem_Actions input;

    private void OnEnable()
    {
        ball = GetComponent<CopymonBall>();
        chair = GetComponent<g_ObjectPlacementManager>();
        if (input == null)
        {
            input = new InputSystem_Actions();

            input.Player.Attack.performed += i => ball?.MouseClick();
            input.Player.Attack.canceled += i => ball?.MouseRelease();

            input.Player.Attack.performed += i => chair?.MouseClick();
            input.Player.Attack.canceled += i => chair?.MouseRelease();
        }

        input.Enable();
    }

}
