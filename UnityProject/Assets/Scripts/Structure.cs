using UnityEngine;

/// <summary>
/// Any structure that can be built within the gameplay needs this script.
/// An area is required for this. In this game, the z-coordinate must always be set to 1.
/// X and Y are for the length and width of the structure in tiles.
/// </summary>
public class Structure : MonoBehaviour {
    public bool Placed { get; private set; }
    public BoundsInt area;
    [SerializeField]
    private Canvas menu;

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
    }

    private BoundsInt GetTempArea() {
        return new BoundsInt(StructureHandler.Instance.gridLayout.LocalToCell(transform.position), area.size);
    }

    #endregion

    #region Menu
        private void EnableMenu() {
            if(!Placed) {return;}

            // TODO
        }
    #endregion
}