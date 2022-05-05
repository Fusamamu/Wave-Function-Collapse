using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WCF
{
    public class WaveFunctionCollapse : MonoBehaviour
    {
        [SerializeField] private ImageGrid imageGrid;

        private void Start()
        {
            if (imageGrid == null)
                imageGrid = FindObjectOfType<ImageGrid>();
            
            imageGrid.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                imageGrid.PickRandomCell();
                
                
                
                
                
            }
        }
    }
}
