using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript Instance { get; private set; }

	[SerializeField] public List<Structure> foundations;

	[SerializeField] private float          movementSpeed = 5f;
	[SerializeField] private ActivityToggle buildMenuActivityToggle;

	private GameInputActions.PlayerActions _playerActions;
	private Vector2                        _move = Vector2.zero;
	private Collider2D                           _trigger; // null if the player isn't in a trigger


	private void OnEnable() {
		_playerActions.Enable();
	}

	private void OnDisable() {
		_playerActions.Disable();
	}

	private void OnTriggerEnter2D(Collider2D col) {
		_trigger = col;
	}

	private void OnTriggerExit2D(Collider2D other) {
		_trigger = null;

		if (StructureInteractionMenu.Instance.gameObject.activeSelf) {
			StructureInteractionMenu.Instance.Disable();
		}
	}

	private void Awake() {
		Instance = this;

		_playerActions = Util.InputAction.Player;

		//Movement
		_playerActions.Movement.performed += context => _move = context.ReadValue<Vector2>();
		_playerActions.Movement.canceled  += _ => _move       = Vector2.zero;

		//Hotkeys
		_playerActions.BuildMenu.performed   += _ => BuildMenu();
		_playerActions.Interaction.performed += _ => Interact();
	}

	public void LoadStartFoundations()
	{
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
		if (_trigger == null) {
			return;
		}

		var structure = _trigger.gameObject.GetComponent<Structure>();
		StructureInteractionMenu.Instance.Enable(structure);
	}

	public bool AddFoundation(Structure structure) {
		return BuildMenuScript.Instance.AddFoundationItem(structure);
	}

	public bool RemoveFoundation(Structure structure) {
		return BuildMenuScript.Instance.RemoveFoundationItem(structure);
	}
}