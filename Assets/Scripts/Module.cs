using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WCF
{
    public class Module : MonoBehaviour
    {
        public int ID;
        
        public Sprite Sprite;

        public List<Module> EastModules  = new List<Module>();
        public List<Module> WestModules  = new List<Module>();
        public List<Module> SouthModules = new List<Module>();
        public List<Module> NorthModules = new List<Module>();

        public List<int> EastIDs  = new List<int>();
        public List<int> WestIDs  = new List<int>();
        public List<int> SouthIDs = new List<int>();
        public List<int> NorthIDs = new List<int>();

        public void InitModuleIDs()
        {
            EastIDs  = EastModules.Select(_module => _module.ID).ToList();
            WestIDs  = WestModules.Select(_module => _module.ID).ToList();
            SouthIDs = SouthModules.Select(_module => _module.ID).ToList();
            NorthIDs = NorthModules.Select(_module => _module.ID).ToList();
        }

        public List<int> GetEastIDs()
        {
            return EastModules.Select(_module => _module.ID).ToList();
        }

        public List<int> GetWestIDs()
        {
            return WestModules.Select(_module => _module.ID).ToList();
        }

        public List<int> GetSouthIDs()
        {
            return SouthModules.Select(_module => _module.ID).ToList();
        }

        public List<int> GetNorthIDs()
        {
            return NorthModules.Select(_module => _module.ID).ToList();
        }
    }
}
