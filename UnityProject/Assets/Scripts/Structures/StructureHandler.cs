using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This class handles all structures for placing them on the grid.
/// </summary>
public class StructureHandler : MonoBehaviour {
	public static StructureHandler Instance { get; private set; }

	private static readonly Dictionary<TileType, TileBase> TileBases = new();

	//Unity attributes
	public                   GridLayout gridLayout;
	[SerializeField] private Tilemap    mainTilemap;
	[SerializeField] private Tilemap    tempTilemap;
	[SerializeField] private Transform    structureParent;
	[SerializeField] private TileBase   whiteTile;
	[SerializeField] private TileBase   greenTile;
	[SerializeField] private TileBase   redTile;


	private Structure _tempComponent;
	private Vector3   _prevMousePos;
	private BoundsInt _prevArea;

	#region Unity Methods

	private void Awake() {
		Instance = this;
	}

	private void Start() {
		TileBases.Add(TileType.Emtpy, null);
		TileBases.Add(TileType.White, whiteTile);
		TileBases.Add(TileType.Green, greenTile);
		TileBases.Add(TileType.Red, redTile);
	}

	#endregion

	#region Input Actions

	public void Submit() {
		if (_tempComponent == null) {
			return;
		}

		if (_tempComponent.CanBePlaced()) {
			_tempComponent.Place();
			_tempComponent = null;
		}
	}

	public void Cancel() {
		if (_tempComponent == null) {
			return;
		}

		StopPlacing();
	}

	public void MousePosition(Vector2 pos) {
		if (_tempComponent == null) {
			return;
		}

		if (!_tempComponent.Placed) {
			var cellPos = gridLayout.LocalToCell(Camera.main.ScreenToWorldPoint(pos));

			if (_prevMousePos != cellPos) {
				_tempComponent.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f));
				_prevMousePos                          = cellPos;
				FollowBuilding();
			}
		}
	}

	#endregion

	#region Tilemap Utils

	private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap) {
		var tilesBlock = new TileBase[area.size.x * area.size.y];
		var counter    = 0;

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

	#region Structure Placement

	public void OnInstantiateButtonClicked(GameObject gridComponent) {
		if (!IsInPlacing()) {
			StartPlacing(gridComponent);
		}
	}

	public void StartPlacing(GameObject gridComponent) {
		_tempComponent = Instantiate(gridComponent, Vector3.zero, Quaternion.identity, structureParent).GetComponent<Structure>();
		BuildMenuScript.Instance.RemoveFoundationItem(_tempComponent);
		FollowBuilding();
	}

	public void StopPlacing() {
		ClearArea();
		if (!_tempComponent.Placed) {
			Destroy(_tempComponent.gameObject);
			BuildMenuScript.Instance.AddFoundationItem(_tempComponent);
		}
		
		_tempComponent = null;
	}

	private void ClearArea() {
		var toClear = new TileBase[_prevArea.size.x * _prevArea.size.y * _prevArea.size.z];
		FillTilesBlock(toClear, TileType.Emtpy);
		tempTilemap.SetTilesBlock(_prevArea, toClear);
	}

	private void FollowBuilding() {
		ClearArea();

		_tempComponent.area.position = gridLayout.WorldToCell(_tempComponent.gameObject.transform.position);
		var placingArea = _tempComponent.area;

		var isTilesBlockWhite = IsTilesBlockOfType(GetTilesBlock(placingArea, mainTilemap), TileType.White);
		SetTilesBlock(placingArea, isTilesBlockWhite ? TileType.Green : TileType.Red, tempTilemap);
		_prevArea = placingArea;
	}

	public bool CanTakeArea(BoundsInt area) {
		if (IsTilesBlockOfType(GetTilesBlock(area, mainTilemap), TileType.White)) {
			return true;
		}

		Debug.Log("Cannot place here");
		return false;
	}

	public void TakeArea(BoundsInt area) {
		SetTilesBlock(area, TileType.Emtpy, tempTilemap);
		SetTilesBlock(area, TileType.Green, mainTilemap);
	}

	public bool IsInPlacing() {
		return _tempComponent != null;
	}

	#endregion

	#region Structure Tools

	public void Move(Structure structure) {
		if (!BuildMenuScript.Instance.gameObject.activeSelf) {
			BuildMenuScript.Instance.gameObject.GetComponent<ActivityToggle>().ToggleActivity();
		}

		SetTilesBlock(structure.area, TileType.White, mainTilemap);
		_tempComponent = structure;
	}

	public void Remove(BoundsInt area) {
		SetTilesBlock(area, TileType.White, mainTilemap);
	} 

	#endregion
}

public enum TileType {
	Emtpy,
	White,
	Green,
	Red
}