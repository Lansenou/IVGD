using System;
using System.Collections.Generic;
using Assets.Scripts.Blocks;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class BlockManager : Singleton<BlockManager>
    {
        private string lastBlock;
        private List<BuildingBlock> blocks = new List<BuildingBlock>();

        void Start()
        {
            blocks = new List<BuildingBlock>();
        }

        public void AddBlock(BuildingBlock block)
        {
            if (lastBlock == block.name || blocks.Contains(block))
            {
              //  Debug.Log(name + " block was already added");
                return;
            }

           // Debug.Log(name + " adding block: " + block.name);

            lastBlock = block.name;
            blocks.Add(block);
            AnimateBlocks();
        }

        private void AnimateBlocks()
        {
            foreach (BuildingBlock b in blocks)
            {
                b.PlayAnimation();
            }
        }
    }
}