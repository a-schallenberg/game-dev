using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class GridComponent : MonoBehaviour {
    public bool Placed { get; private set; }
    public BoundsInt Area;

    #region Placing

    public bool CanBePlaced() {
        return GridComponentHandler.Instance.CanTakeArea(GetTempArea());
    }

    public void Place() {
        Placed = true;
        GridComponentHandler.Instance.TakeArea(GetTempArea());
    }

    private BoundsInt GetTempArea() {
        return new BoundsInt(GridComponentHandler.Instance.GridLayout.LocalToCell(transform.position), Area.size);
    }

    #endregion
}