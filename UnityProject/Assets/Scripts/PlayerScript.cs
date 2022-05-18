using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript Instance { get; private set; }

	[SerializeField] public List<Building> foundations;

	[SerializeField] private float          movementSpeed = 5f;

	private                Vector2    _move = Vector2.zero;
	private Collider2D _trigger; // null if the player isn't in a trigger

	public PlayerScript() {
		Instance = this;
	}
	
	#region Unity Methods

	private void OnTriggerEnter2D(Collider2D col) {
		_trigger = col;
	}

	private void OnTriggerExit2D(Collider2D other) {
		_trigger = null;
	}

	private void Awake() {
		LoadStartFoundations();
	}

	private void Update() {
		if (_move != Vector2.zero) {
			var velocity = _move * (Time.deltaTime * movementSpeed);
			var pos      = transform.position;
			transform.position = new Vector3(pos.x + velocity.x, pos.y + velocity.y, pos.z);
		}
	}

	#endregion

	#region Input Actions
	public void Move(Vector2 vec) {
		_move = vec;
	}

	public void Interact() {
		if (_trigger == null) {
			return;
		}
		
		_trigger.gameObject.GetComponent<Structure>().OnPlayerInteract(_trigger);
	}

	#endregion

	#region Foundation handling

	public void LoadStartFoundations()
	{
		foreach (var foundation in foundations) {
			BuildMenu.Instance.AddFoundationItem(foundation);
		}
	}
	
	public bool AddFoundation(Building building) {
		return BuildMenu.Instance.AddFoundationItem(building);
	}

	public bool RemoveFoundation(Building building) {
		return BuildMenu.Instance.RemoveFoundationItem(building);
	}

	#endregion
	
}