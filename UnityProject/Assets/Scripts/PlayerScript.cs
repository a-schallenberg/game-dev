using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class PlayerScript : MonoBehaviour {
    public static PlayerScript Instance { get; private set; }

    public List<Button> foundations;
    [SerializeField]
    private float movementSpeed = 5f;

    private GameInputActions.PlayerActions _playerActions;
    private Vector2 _move = Vector2.zero;
    

    private void OnEnable() {
        _playerActions.Enable();
    }

    private void OnDisable() {
        _playerActions.Disable();
    }

    private void Awake() {
        Instance = this;

        _playerActions = Util.InputAction.Player;

        //Movement
        _playerActions.Movement.performed += context => _move = context.ReadValue<Vector2>();
        _playerActions.Movement.canceled += _ => _move = Vector2.zero;
        
        //Hotkeys
        _playerActions.BuildMenu.performed += _ => BuildMenu();
    }

    private void Update() {
        if (_move != Vector2.zero) {
            var velocity = _move * (Time.deltaTime * movementSpeed);
            var pos = transform.position;
            transform.position = new Vector3(pos.x + velocity.x, pos.y + velocity.y, pos.z);
        }
    }

    private void BuildMenu() {
        
    }
}
