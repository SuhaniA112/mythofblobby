using System;
using UnityEngine;

public class GameInput : MonoBehaviour { 

    
    public static GameInput Instance { get; private set; }


    private PlayerInputActions playerInputActions;


    public event EventHandler OnPlayerSwordAttack;


    private void Awake() {
        Instance = Instance == null? this : throw new Exception($"There is more than one Instance of {Instance}!");

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.SwordAttack.performed += SwordAttack_performed;
    }

    private void SwordAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerSwordAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMoveVectorNormalized() {
        Vector2 moveVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return moveVector.normalized;
    }
}
