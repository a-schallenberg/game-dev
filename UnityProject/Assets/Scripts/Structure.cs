using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class Structure : MonoBehaviour {
	
	public BoundsInt area;

	[SerializeField] private  bool      placed;
	[SerializeField] private string    id;
	[SerializeField] private string    structureName;
	[SerializeField] private Transform menuPanel;

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

	public void Move() {
		Placed = false;
		StructureHandler.Instance.Move(this);
	}
	public void Remove() {
		StructureHandler.Instance.Remove(area);
		Destroy(gameObject);
		BuildMenuScript.Instance.AddFoundationItem(this); // TODO do we want this?
	}

	private BoundsInt GetTempArea() {
		return new BoundsInt(StructureHandler.Instance.gridLayout.LocalToCell(transform.position), area.size);
	}

	#endregion

	#region Menu

	public void OnMenuEnabled() { }

	public void OnMenuDisabled() { }

	#endregion

	#region Operators

	public override bool Equals(object obj) {
		return obj != null && obj.GetType() == typeof(Structure) && ID == ((Structure) obj).ID;
	}

	public override int GetHashCode() {
		return ID.GetHashCode();
	}

	#endregion

	#region Getter

	public bool Placed {
		get { return placed; }
		private set {
			placed = value; 
			foreach (var col in GetComponentsInChildren<Collider2D>()) {
				col.enabled = value;
			}
		}
	}

	public string ID {
		get { return id; }
		private set { id = value; }
	}

	public string StructureName {
		get { return structureName; }
	}

	public Transform MenuPanel {
		get { return menuPanel; }
	}

	#endregion
}