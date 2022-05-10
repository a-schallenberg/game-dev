using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class Structure : MonoBehaviour {
	public bool      Placed { get; private set; }
	public BoundsInt area;
	public string    structureName;

	[SerializeField] private Canvas menu;

	#region Unity Methods

	private void Awake() {
		menu.enabled = false;
	}

	#endregion

	#region Placing

	public bool CanBePlaced() {
		return StructureHandler.Instance.CanTakeArea(GetTempArea());
	}

	public void Place() {
		Placed = true;
		StructureHandler.Instance.TakeArea(GetTempArea());

		BuildMenuScript.Instance.RemoveFoundationItem(this);
	}

	private BoundsInt GetTempArea() {
		return new BoundsInt(StructureHandler.Instance.gridLayout.LocalToCell(transform.position), area.size);
	}

	#endregion

	#region Menu

	private void EnableMenu() {
		if (!Placed) {
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