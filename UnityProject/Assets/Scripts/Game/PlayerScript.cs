using System;
using Game.Structures;
using Game.UI.MenuHandling;
using Game.UI.MenuHandling.Menus;
using UnityEngine;

namespace Game {
	public class PlayerScript : MonoBehaviour {
		[SerializeField] private float movementSpeed = 5f;

		private Vector2    _move = Vector2.zero;
		private Collider2D _trigger; // null if the player isn't in a trigger
		private Animator _animator;
		/*
		 * 0 = Idle
		 * 1 = Move up
		 * 2 = Move down
		 * 3 = Move right
		 * 4 = Move left
		 */
		private int _animationState = 0;
		public PlayerScript() {
			Instance = this;
		}

		public static PlayerScript Instance { get; private set; }

		#region Unity Methods

		private void OnTriggerEnter2D(Collider2D col) {
			_trigger = col;
		}

		private void OnTriggerExit2D(Collider2D other) {
			_trigger = null;
		}

		private void Update() {
			if (_move != Vector2.zero) {
				var velocity = _move * (Time.deltaTime * movementSpeed);
				var pos      = transform.position;
				transform.position = new Vector3(pos.x + velocity.x, pos.y + velocity.y, pos.z);

				if (velocity.y > 0) _animationState = 1;
				if (velocity.y < 0) _animationState = 2;
				if (velocity.x > 0) _animationState = 3;
				if (velocity.x < 0) _animationState = 4;
			}
			else
			{
				_animationState = 0;
			}

			switch (_animationState)
			{
				case 0: _animator.Play("Player_Idle");
					break;
				case 1: _animator.Play("Player_MoveUp");
					break;
				case 2: _animator.Play("Player_MoveDown");
					break;
				case 3: _animator.Play("Player_MoveRight");
					break;
				case 4: _animator.Play("Player_MoveLeft");
					break;
				default:
					_animator.Play("Player_Idle");
					break;
			}
		}

		private void Awake()
		{
			_animator = GetComponentInChildren<Animator>();
			InputActions.Load();
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

			_trigger.gameObject.GetComponent<Interactable>().Interact(_trigger);
		}

		#endregion

		#region Foundation handling

		public bool AddFoundation(Building building) {
			return BuildMenu.Instance.AddFoundationItem(building);
		}

		public bool RemoveFoundation(Building building) {
			return BuildMenu.Instance.RemoveFoundationItem(building);
		}

		#endregion
	}
}