using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WCF
{
	public class ImageGrid : MonoBehaviour
	{
		public Vector2Int GridSize;

		public float CellWidth;
		public float CellHeight;

		public Cell CellPrefab;

		public Cell[] Cells;

		private void Start()
		{
			
		}

		public void Init()
		{
			var _row    = GridSize.y;
			var _column = GridSize.x;

			Cells = new Cell[_row * _column];

			var _group = new GameObject("CellGrid");

			for (var _i = 0; _i < _row; _i++)
			{
				for (var _j = 0; _j < _column; _j++)
				{
					var _targetPos = new Vector2(_i, _j);
					var _newCell = Instantiate(CellPrefab, _targetPos, Quaternion.identity, _group.transform);

					_newCell.name = $"[{_j}, {_i}]";
					
					Cells[_i + _j * _column] = _newCell;
				}
			}
			
		}

	}
}
