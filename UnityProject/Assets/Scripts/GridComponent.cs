using UnityEngine;
using UnityEngine.UI;

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