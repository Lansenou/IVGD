using System.Collections;
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
        public GameObject BuildParticles;

        private bool spawnBlock = false;

        private int blockCounter = 0;
        private float currentTime = 0;
        private float nextBlockY = 0;
        private Scoring score;
        private Color currentColor;
        private Rigidbody nextBlock;

        public void SetCurrentColor(Color color)
        {
            currentColor = color;
        }

        public void Spawn()
        {
            currentTime = (currentTime + Time.deltaTime) * Info.Color.ColorSpeed % 1;
            //Set last spawned block for camera follow script
            SmoothFollow.NewLastBlock(nextBlock.transform);
            Transform particles = Instantiate(BuildParticles).transform;
            particles.position = Vector3.zero;
            particles.SetParent(nextBlock.transform, false);

            // Score points
            if (score)
            {
                score.AddPoints(nextBlock.transform);
            }
        
            //Get new block
            StartCoroutine(SpawnNewBlock());
            nextBlockY = transform.position.y + 3;
        }

        public void DropBlock()
        {
            nextBlock.isKinematic = false;
            nextBlock.name = "Block " + blockCounter++;
            HighScore.instance.CurrentScore += 1;
            nextBlock.GetComponent<BoxCollider>().enabled = true;
            // Add the block to the manager
            BuildingBlock buildingBlock = nextBlock.GetComponentInChildren<BuildingBlock>();
            BlockManager.Instance.AddBlock(buildingBlock);
            CameraShake.Instance().ScreenShake(.5f);
        }

        public int GetBlockCount()
        {
            return blockCounter;
        }

         IEnumerator SpawnNewBlock()
         {
            DropBlock();
            nextBlock = null;
            yield return new WaitForSeconds(1f);
            nextBlock = getBlock();
            OnTap.blockAvailable = true;
         }


        private void Start()
        {
            score = FindObjectOfType<Scoring>();
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
            block.GetComponentInChildren<Renderer>().material.color = currentColor;
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