using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Structure : MonoBehaviour {
    public                   BoundsInt area;

	[SerializeField] protected bool   placed;
	[SerializeField] protected string id;

	[SerializeField] private UnityEvent<Collider2D> onPlayerInteract;

	#region Unity Methods

	private void Awake() {
		foreach (var col in GetComponentsInChildren<Collider2D>()) {
			col.enabled = Placed;
		}
	}

	#endregion

	#region Building Tools

	public bool CanBePlaced() {
		return StructureHandler.Instance.CanTakeArea(GetTempArea());
	}

	public void Place() {
		Placed = true;
		StructureHandler.Instance.TakeArea(GetTempArea());
	}

	public void Remove() {
		StructureHandler.Instance.Remove(area);
		Destroy(gameObject);
	}

	protected BoundsInt GetTempArea() {
		return new BoundsInt(StructureHandler.Instance.gridLayout.LocalToCell(transform.position), area.size);
	}

	#endregion

	public void OnPlayerInteract(Collider2D trigger) {
		onPlayerInteract.Invoke(trigger);
	}

	#region Operators

	public override bool Equals(object obj) {
		return obj != null && obj.GetType() == typeof(Building) && ID == ((Building) obj).ID;
	}

	public override int GetHashCode() {
		return ID.GetHashCode();
	}

	#endregion

	#region Getter

	public bool Placed {
		get { return placed; }
		protected set {
			placed = value; 
			foreach (var col in GetComponentsInChildren<Collider2D>()) {
				col.enabled = value;
			}
		}
	}

	public string ID {
		get { return id; }
		protected set { id = value; }
	}

	#endregion
}
