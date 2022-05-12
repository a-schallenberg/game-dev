using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class Structure : MonoBehaviour {
	public bool      placed;
	public BoundsInt area;
	public string    structureName;

	#region Unity Methods

	private void Awake() {
		foreach (var collider in GetComponentsInChildren<Collider2D>()) {
			collider.enabled = placed;
		}
	}

	private void OnMouseOver() {
		print("pressed: " + gameObject.name);
	}

	#endregion

	#region Placing

	public bool CanBePlaced() {
		return StructureHandler.Instance.CanTakeArea(GetTempArea());
	}

	public void Place() {
		placed = true;
		StructureHandler.Instance.TakeArea(GetTempArea());

		BuildMenuScript.Instance.RemoveFoundationItem(this);
		
		foreach (var collider in GetComponentsInChildren<Collider2D>()) {
			collider.enabled = true;
		}
	}

	private BoundsInt GetTempArea() {
		return new BoundsInt(StructureHandler.Instance.gridLayout.LocalToCell(transform.position), area.size);
	}

	#endregion

	#region Menu

	private void EnableMenu() {
		if (!placed) {
			return;
		}

		// TODO
	}

	#endregion

	#region Operators

	public static bool operator ==(Structure s1, Structure s2) {
		if (ReferenceEquals(s1, null) && ReferenceEquals(s2, null)) {
			return true;
		}

		if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null)) {
			return false;
		}

		return s1.name == s2.name;
	}

	public static bool operator !=(Structure s1, Structure s2) {
		return !(s1 == s2);
	}

	public override bool Equals(object obj) {
		return obj != null && obj.GetType() == typeof(Structure) && structureName.Equals(((Structure) obj).structureName);
	}

	public override int GetHashCode() {
		return structureName.GetHashCode();
	}

	#endregion
}