using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript Instance { get; private set; }

	[SerializeField] public List<Structure> foundations;


	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private ActivityToggle buildMenuActivityToggle;

	private GameInputActions.PlayerActions _playerActions;
	private Vector2                        _move = Vector2.zero;


	private void OnEnable() {
		_playerActions.Enable();
	}

	private void OnDisable() {
		_playerActions.Disable();
	}

	private void OnCollisionEnter2D(Collision2D col) {
		print("Enter Collision");
	}

	private void OnCollisionStay2D(Collision2D collision) {
		print("Stay Collision");
	}

	private void OnCollisionExit2D(Collision2D other) {
		print("Leave Collision");
	}

	private void OnTriggerEnter2D(Collider2D col) {
		print("Enter Trigger");
	}

	private void OnTriggerStay2D(Collider2D other) {
		print("Stay Trigger");
	}

	private void OnTriggerExit2D(Collider2D other) {
		print("Leave Trigger");
	}

	private void Awake() {
		Instance = this;
		
		_playerActions  = Util.InputAction.Player;

		//Movement
		_playerActions.Movement.performed += context => _move = context.ReadValue<Vector2>();
		_playerActions.Movement.canceled  += _ => _move       = Vector2.zero;

		//Hotkeys
		_playerActions.BuildMenu.performed += _ => BuildMenu();
		_playerActions.Interaction.performed += _ => Interact();
	}

	public void LoadStartFoundations() { 
		foreach (var foundation in foundations) { 
			BuildMenuScript.Instance.AddFoundationItem(foundation);
		}
	}

	private void Update() {
		if (_move != Vector2.zero) {
			var velocity = _move * (Time.deltaTime * movementSpeed);
			var pos      = transform.position;
			transform.position = new Vector3(pos.x + velocity.x, pos.y + velocity.y, pos.z);
		}
	}

	private void BuildMenu() {
		buildMenuActivityToggle.ToggleActivity();
	}

	private void Interact() {
		
	}

	public bool AddFoundation(Structure structure) {
		return BuildMenuScript.Instance.AddFoundationItem(structure);
	}
	public bool RemoveFoundation(Structure structure) {
		return BuildMenuScript.Instance.RemoveFoundationItem(structure);
	}
}