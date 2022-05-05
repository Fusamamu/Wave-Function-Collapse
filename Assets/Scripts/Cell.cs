using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WCF
{
    public class Cell : MonoBehaviour
    {
        public Module SelectedModule;
        
        public List<Module> Modules = new List<Module>();

        private SpriteRenderer spriteRenderer;

        public bool IsCollapsed;

        private void Start()
        {
        }
        
        public void PickRandomModule()
        {
            var _randomIndex = Random.Range(0, Modules.Count - 1);
            
            SelectedModule = Modules[_randomIndex];

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = SelectedModule.Sprite;

            IsCollapsed = true;
        }
    }
}
