using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace Assets.Scripts.Blocks
{
    public class BlockSpawner : MonoBehaviour
    {
        public SpawnInfo Info;
        public SmoothFollow SmoothFollow;

        [SerializeField]
        private BlockTrail blockTrail;

        private int blockCounter = 0;
        private float currentTime = 0;
        private float nextBlockY = 0;
        private Score score;
        private Color currentColor;
        private Rigidbody nextBlock;

        public void SetCurrentColor(Color color)
        {
            currentColor = color;
            blockTrail.SetColor(color);
        }

        public void Spawn()
        {
            currentTime = (currentTime + Time.deltaTime) * Info.Color.ColorSpeed % 1;

            // Drop current
            if (nextBlock)
            {
                nextBlock.isKinematic = false;
                nextBlock.name = "Block " + blockCounter++;
                HighScore.CurrentScore += 1;

                // Add the block to the manager
                BuildingBlock buildingBlock = nextBlock.GetComponent<BuildingBlock>();
                BlockManager.Instance().AddBlock(buildingBlock);
                CameraShake.Instance().ScreenShake(.5f);
            }
            //Set last spawned block for camera follow script
            SmoothFollow.NewLastBlock(nextBlock);          

            // Score points
            if (score)
            {
                score.AddPoints(nextBlock.transform);
            }
        
            //Get new block
            nextBlock = getBlock();
            nextBlockY = transform.position.y + 3;
        }

        public int GetBlockCount()
        {
            return blockCounter;
        }


        private void Start()
        {
            score = FindObjectOfType<Score>();
            nextBlockY = transform.position.y;
            nextBlock = getBlock();
        }

        private void LateUpdate()
        {
            if (nextBlock)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = nextBlockY;
                nextBlock.transform.position = newPosition;
            }
        }

        private Rigidbody getBlock()
        {
            GameObject block = Instantiate(Info.prefabs[Info.selectedPrefab], Info.Folder, false) as GameObject;
            block.transform.position = transform.position + new Vector3(0, 1);
            block.GetComponent<Renderer>().material.color = currentColor;
            block.name = "Placeholder Block";

            Rigidbody rigidbody = block.GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            return rigidbody;
        }

        [System.Serializable]
        public class SpawnInfo
        {
            public Transform Folder;
            public List<GameObject> prefabs;
            public BlockColor Color;
            public int selectedPrefab = 0;

            [System.Serializable]
            public class BlockColor
            {
                public Gradient Gradient;
                public float ColorSpeed = 1;
            }
        }
    }
}