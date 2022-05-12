using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WCF
{
    public class Cell : MonoBehaviour
    {
        public int Entropy;
        
        public bool       IsCollapsed;
        public Vector2Int Coord;

        public ImageGrid Grid;
        
        public Module SelectedModule;
        
        public List<Module> Modules       = new List<Module>();
        public List<Cell>   AdjacentCells = new List<Cell>();

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
        }

        public void InitCell(ImageGrid _grid)
        {
            foreach (var _module in Modules)
                _module.InitModuleIDs();

            Grid = _grid;
        }

        public void UpdateCell()
        {
            if (Modules.Count == 1)
            {
                SelectedModule = Modules.First(_module => _module != null);

                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = SelectedModule.Sprite;
            
                Modules.Clear();

                IsCollapsed = true;
            }
            
            UpdateEntropy();
        }

        public void CollapseModulesByID(int _id)
        {
            var _targetModule = Modules.FirstOrDefault(_module => _module.ID == _id);
            
            Modules.Remove(_targetModule);

            if (Modules.Count == 1)
                IsCollapsed = true;
        }

        public int GetEntropy()
        {
            if (IsCollapsed) return 99999;

            return Modules.Count;
        }

        private void UpdateEntropy()
        {
            Entropy = Modules.Count;

            if (IsCollapsed)
                Entropy = -1;
        }

        public List<int> GetAllPossibleEastIDs()
        {
            var _allEastIDs = new List<int>();
            
            foreach (var _module in Modules)
            {
                if (_module == null) continue;
                _allEastIDs.AddRange(_module.GetEastIDs());
            }

            return _allEastIDs;
        }
        
        public List<int> GetAllPossibleNorthIDs()
        {
            var _allEastIDs = new List<int>();
            
            foreach (var _module in Modules)
            {
                if (_module == null) continue;
                _allEastIDs.AddRange(_module.GetNorthIDs());
            }

            return _allEastIDs;
        }
        
        public List<int> GetAllPossibleWestIDs()
        {
            var _allEastIDs = new List<int>();
            
            foreach (var _module in Modules)
            {
                if (_module == null) continue;
                _allEastIDs.AddRange(_module.GetWestIDs());
            }

            return _allEastIDs;
        }
        
        public List<int> GetAllPossibleSouthIDs()
        {
            var _allEastIDs = new List<int>();
            
            foreach (var _module in Modules)
            {
                if (_module == null) continue;
                _allEastIDs.AddRange(_module.GetSouthIDs());
            }

            return _allEastIDs;
        }

        public void ForceCollapse()
        {
            if(IsCollapsed || Modules.Count == 0) return;
            
            var _randomIndex = Random.Range(0, Modules.Count - 1);
            
            SelectedModule = Modules[_randomIndex];

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = SelectedModule.Sprite;
            
            Modules.Clear();

            IsCollapsed = true;
            
            Propagate();
        }

        public void Propagate()
        {
            if(!IsCollapsed) return;
            
            int _moduleID = SelectedModule.ID;

            var _eastCell = Grid.GetEastCell(this);
            if (_eastCell != null)
            {
                if (!_eastCell.IsCollapsed)
                {
                    var _toBeRemoved = new List<Module>();
                    
                    foreach (var _module in _eastCell.Modules)
                    {
                        if (!_module.GetWestIDs().Contains(_moduleID))
                            _toBeRemoved.Add(_module);   
                    }

                    foreach (var _module in _toBeRemoved)
                    {
                        _eastCell.Modules.Remove(_module);
                        
                        _eastCell.UpdateCell();

                        if (_eastCell.IsCollapsed)
                        {
                            _eastCell.Propagate();
                            break;
                        }
                    }
                }
            }
            
            var _northCell = Grid.GetNorthCell(this);
            if (_northCell != null)
            {
                if (!_northCell.IsCollapsed)
                {
                    var _toBeRemoved = new List<Module>();
                    
                    foreach (var _module in _northCell.Modules)
                    {
                        if (!_module.GetSouthIDs().Contains(_moduleID))
                            _toBeRemoved.Add(_module);   
                    }

                    foreach (var _module in _toBeRemoved)
                    {
                        _northCell.Modules.Remove(_module);
                        
                        _northCell.UpdateCell();

                        if (_northCell.IsCollapsed)
                        {
                            _northCell.Propagate();
                            break;
                        }
                    }
                }
            }

            var _westCell = Grid.GetWestCell(this);
            if (_westCell != null)
            {
                if (!_westCell.IsCollapsed && _westCell != null)
                {
                    var _toBeRemoved = new List<Module>();

                    foreach (var _module in _westCell.Modules)
                    {
                        if(!_module.GetEastIDs().Contains(_moduleID))
                            _toBeRemoved.Add(_module);
                    }

                    foreach (var _module in _toBeRemoved)
                    {
                        _westCell.Modules.Remove(_module);
                        
                        _westCell.UpdateCell();

                        if (_westCell.IsCollapsed)
                        {
                            _westCell.Propagate();
                            break;
                        }
                    }
                }
            }

            var _southCell = Grid.GetSouth(this);

            if (_southCell != null)
            {
                if (!_southCell.IsCollapsed)
                {
                    var _toBeRemoved = new List<Module>();

                    foreach (var _module in _southCell.Modules)
                    {
                        if(!_module.GetNorthIDs().Contains(_moduleID))
                            _toBeRemoved.Add(_module);
                    }

                    foreach (var _module in _toBeRemoved)
                    {
                        _southCell.Modules.Remove(_module);
                        
                        _southCell.UpdateCell();

                        if (_southCell.IsCollapsed)
                        {
                            _southCell.Propagate();
                            break;
                        }
                    }
                }
            }

        }
    }
}
