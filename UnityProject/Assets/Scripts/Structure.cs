using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class Structure : MonoBehaviour {
    public bool Placed { get; private set; }
    public BoundsInt Area;
    public Canvas menu;

    #region Placing

    public bool CanBePlaced() {
        return StructureHandler.Instance.CanTakeArea(GetTempArea());
    }

    public void Place() {
        Placed = true;
        StructureHandler.Instance.TakeArea(GetTempArea());
    }

    private BoundsInt GetTempArea() {
        return new BoundsInt(StructureHandler.Instance.GridLayout.LocalToCell(transform.position), Area.size);
    }

    #endregion

    #region Menu
        
    #endregion
}