using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WCF
{
	public class ImageGrid : MonoBehaviour
	{
		public Vector2Int GridSize;

		public float CellWidth;
		public float CellHeight;

		public Cell CellPrefab;

		public Cell[] Cells;

		public void ApplyAllCells(Action<Cell> _action)
		{
			foreach (var _cell in Cells)
				_action?.Invoke(_cell);
		}

		private void Start()
		{
			
		}

		public void Init()
		{
			var _column = GridSize.x;
			var _row    = GridSize.y;

			Cells = new Cell[_row * _column];

			var _group = new GameObject("CellGrid");

			for (var _i = 0; _i < _column; _i++)
			{
				for (var _j = 0; _j < _row; _j++)
				{
					var _targetPos = new Vector2(_i, _j);
					var _newCell = Instantiate(CellPrefab, _targetPos, Quaternion.identity, _group.transform);

					_newCell.transform.position -= new Vector3(_column / 2, _row / 2);

					_newCell.name = $"[Column:{_i}, Row:{_j}]";
					_newCell.Coord = new Vector2Int(_i, _j);
					_newCell.InitCell(this);
					
					Cells[_i + _j * _column] = _newCell;
				}
			}

			foreach (var _cell in Cells)
			{
				_cell.AdjacentCells.AddRange(new List<Cell>
				{
					GetEastCell (_cell),
					GetNorthCell(_cell),
					GetWestCell (_cell),
					GetSouth    (_cell)
				});
			}
			
			DebugTextOverLay();
		}

		public Cell GetLeastEntropyCell()
		{
			var _remainingCells = Cells.Where(_cell => !_cell.IsCollapsed);
			
			return _remainingCells.OrderBy(_cell => _cell.GetEntropy()).FirstOrDefault();
		}

		public Cell PickRandomCell()
		{
			bool _allCollapsed = false;
			
			foreach (var _cell in Cells)
				_allCollapsed = _cell.IsCollapsed;

			if (_allCollapsed) return null;
			
			var _randomCell = GetRandomCell();

			if (_randomCell.IsCollapsed)
				return PickRandomCell();

			return _randomCell;
		}

		public Cell GetRandomCell()
		{
			var _randomRow    = Random.Range(0, GridSize.y);
			var _randomColumn = Random.Range(0, GridSize.x);

			return Cells[_randomRow + _randomColumn * GridSize.x];
		}

		public Cell GetEastCell(Cell _cell)
		{
			var _x = _cell.Coord.x + 1;
			var _y = _cell.Coord.y;

			var _coord = new Vector2Int(_x, _y);
			
			return GetCellAtCoord(_coord);
		}

		public Cell GetNorthCell(Cell _cell)
		{
			var _x = _cell.Coord.x;
			var _y = _cell.Coord.y + 1;

			var _coord = new Vector2Int(_x, _y);

			return GetCellAtCoord(_coord);
		}

		public Cell GetWestCell(Cell _cell)
		{
			var _x = _cell.Coord.x - 1;
			var _y = _cell.Coord.y;

			var _coord = new Vector2Int(_x, _y);

			return GetCellAtCoord(_coord);
		}
		
		public Cell GetSouth(Cell _cell)
		{
			var _x = _cell.Coord.x;
			var _y = _cell.Coord.y - 1;

			var _coord = new Vector2Int(_x, _y);

			return GetCellAtCoord(_coord);
		}

		public Cell GetCellAtCoord(Vector2Int _coord)
		{
			if (_coord.x > GridSize.x - 1 || _coord.x < 0)
				return null;

			if (_coord.y > GridSize.y - 1 || _coord.y < 0)
				return null;
			
			return Cells[_coord.x + _coord.y * GridSize.x];
		}

		public bool IsAllCollapsed()
		{
			return Cells.Any(_cell => !_cell.IsCollapsed);
		}

		public void DebugTextOverLay()
		{
			foreach (var _cell in Cells)
			{
				var _textObject = new GameObject("Debug Text");
				var _text = _textObject.AddComponent<TextMesh>();

				_text.text = "alsd";
				_text.fontSize = 5;

				_textObject.transform.SetParent(_cell.transform);
			}
		}
	}
}
