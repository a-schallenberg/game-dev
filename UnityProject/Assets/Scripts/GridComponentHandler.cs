using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

/// <summary>
/// This class handles all structures for placing them on the grid.
/// </summary>
public class GridComponentHandler : MonoBehaviour {
    public static GridComponentHandler Instance { get; private set; }

    private static readonly Dictionary<TileType, TileBase> TileBases = new();

    //Unity attributes
    public GridLayout GridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;


    private GridComponent _tempComponent;
    private Vector3 _prevMousePos;
    private BoundsInt _prevArea;

    #region Unity Methods

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        const string tilePath = @"Tiles/";
        TileBases.Add(TileType.Emtpy, null);
        TileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "White"));
        TileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "Green"));
        TileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "Red"));
    }
    
    private void Update() {
        if (!_tempComponent) {
            return;
        }
        
        FollowCursor();


        if (Input.GetMouseButton(0)) {
            if (_tempComponent.CanBePlaced()) {
                _tempComponent.Place();
                _tempComponent = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
            StopPlacing();
        }
    }
    
    private void FollowCursor() {
        if (EventSystem.current.IsPointerOverGameObject(0)) {
            return;
        }

        if (!_tempComponent.Placed) {
            var cellPos = GridLayout.LocalToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (_prevMousePos != cellPos) {
                _tempComponent.transform.localPosition = GridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f));
                _prevMousePos = cellPos;
                FollowBuilding();
            }
        }
    }
    
    #endregion

    #region Tilemap Utils

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap) {
        var tilesBlock = new TileBase[area.size.x * area.size.y];
        var counter = 0;

        foreach (var vec in area.allPositionsWithin) {
            tilesBlock[counter++] = tilemap.GetTile(vec);
        }

        return tilesBlock;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap) {
        var tilesBlock = new TileBase[area.size.x * area.size.y * area.size.z];
        FillTilesBlock(tilesBlock, type);
        tilemap.SetTilesBlock(area, tilesBlock);
    }

    private static void FillTilesBlock(TileBase[] tilesBlock, TileType type) {
        for (var i = 0; i < tilesBlock.Length; i++) {
            tilesBlock[i] = TileBases[type];
        }
    }

    private static bool IsTilesBlockOfType(TileBase[] tilesBlock, TileType type) {
        return tilesBlock.All(tileBase => tileBase == TileBases[type]);
    }
    
    #endregion

    #region GridComponent Placement

    public void OnInstantiateButtonClicked(GameObject gridComponent) {
        if (!IsInPlacing()) {
            StartPlacing(gridComponent);
        }
    }
    private void StartPlacing(GameObject gridComponent) {
        _tempComponent = Instantiate(gridComponent, Vector3.zero, Quaternion.identity).GetComponent<GridComponent>();
        FollowBuilding();
    }

    private void StopPlacing() {
        ClearArea();
        if (!_tempComponent.Placed) {
            Destroy(_tempComponent.gameObject);
        }

        _tempComponent = null;
    }

    private void ClearArea() {
        var toClear = new TileBase[_prevArea.size.x * _prevArea.size.y * _prevArea.size.z];
        FillTilesBlock(toClear, TileType.Emtpy);
        TempTilemap.SetTilesBlock(_prevArea, toClear);
    }

    private void FollowBuilding() {
        ClearArea();

        _tempComponent.Area.position = GridLayout.WorldToCell(_tempComponent.gameObject.transform.position);
        var placingArea = _tempComponent.Area;

        var isTilesBlockWhite = IsTilesBlockOfType(GetTilesBlock(placingArea, MainTilemap), TileType.White);
        SetTilesBlock(placingArea, isTilesBlockWhite ? TileType.Green : TileType.Red, TempTilemap);
        _prevArea = placingArea;
    }

    public bool CanTakeArea(BoundsInt area) {
        if (IsTilesBlockOfType(GetTilesBlock(area, MainTilemap), TileType.White)) {
            return true;
        }
        
        Debug.Log("Cannot place here");
        return false;
    }

    public void TakeArea(BoundsInt area) {
        SetTilesBlock(area, TileType.Emtpy, TempTilemap);
        SetTilesBlock(area, TileType.Green, MainTilemap);
    }

    private bool IsInPlacing() {
        return _tempComponent != null;
    }
    
    #endregion
    
}

public enum TileType {
    Emtpy,
    White,
    Green,
    Red
}