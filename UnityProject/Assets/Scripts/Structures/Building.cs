using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class Building : Structure {
	
	[SerializeField] protected Transform menuPanel;
	[SerializeField] protected string    buildingName;

	#region Building Tools
	

	public void Move() {
		Placed = false;
		StructureHandler.Instance.Move(this);
	}
	public new void Remove() {
		base.Remove();
		BuildMenu.Instance.AddFoundationItem(this); // TODO do we want this?
	}

	#endregion

	public new void OnPlayerInteract(Collider2D trigger) {
		StructureInteractionMenu.Instance.Enable(this);
	}
	
	#region Menu

	public virtual void OnMenuEnable() { }

	public virtual void OnMenuDisable() { }

	#endregion

	#region Operators

	public override bool Equals(object obj) {
		return obj != null && obj.GetType() == typeof(Building) && ID == ((Building) obj).ID;
	}

	public override int GetHashCode() {
		return ID.GetHashCode();
	}

	#endregion

	#region Getter

	public string BuildingName {
		get { return buildingName; }
		set { buildingName = value; }
	}
	
	public Transform MenuPanel {
		get { return menuPanel; }
	}

	#endregion
}