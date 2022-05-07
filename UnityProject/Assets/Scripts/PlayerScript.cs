using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public static PlayerScript Instance { get; private set; }
    
    [SerializeField]
    private float movementSpeed = 5f;
    
    public readonly List<Sprite> Foundations = new();
    
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
