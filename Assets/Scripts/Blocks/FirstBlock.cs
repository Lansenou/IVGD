using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Blocks
{
    public class FirstBlock : BuildingBlock
    {
        private void Start()
        {
            BlockManager.Instance().AddBlock(this);
        }

        private void Update ()
        {
            // Do not call base update
            if (FallManager.DidFall)
            {
                Destroy(gameObject);
            }
        }
    }
}
