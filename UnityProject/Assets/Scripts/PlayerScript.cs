using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float MovementSpeed = 5f;
    
    private GameInputActions.PlayerActions _playerActions;
    private Vector2 _move = Vector2.zero;

    private void OnEnable() {
        _playerActions.Enable();
    }

    private void OnDisable() {
        _playerActions.Disable();
    }

    private void Awake() {
        _playerActions = Util.InputAction.Player;

        _playerActions.Movement.performed += context => _move = context.ReadValue<Vector2>();
        _playerActions.Movement.canceled += _ => _move = Vector2.zero;
    }

    private void Update() {
        if (_move != Vector2.zero) {
            var velocity = _move * (Time.deltaTime * MovementSpeed);
            var pos = transform.position;
            transform.position = new Vector3(pos.x + velocity.x, pos.y + velocity.y, pos.z);
        }
    }
}
