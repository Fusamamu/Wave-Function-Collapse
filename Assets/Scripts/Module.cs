using System.Collections;
using System.Collections.Generic;
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
        
        
    }
}
