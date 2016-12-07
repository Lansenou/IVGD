﻿using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Blocks
{
    public class BuildingBlock : MonoBehaviour
    {
        [SerializeField]
        private float maxFallDistance = 5f;

        [SerializeField]
        private float scaleAmount = 0.15f;

        [SerializeField]
        private float smoothScaleTime = 0.2f;

        private static TowerPhysics TowerPhysics;

        private int scaleIteration = 0;
        private int maxScaleIterations = 2;
        private float startY;
        private float scaleThreshold = 0.05f;
        private bool animate = false;
        private bool animationFinishing = false;

        private Vector3 scale = Vector3.zero;
        private Vector3 scaleTarget = Vector3.zero;
        private Vector3 scaleVelocity = Vector3.zero;

        private void Start()
        {
            if (TowerPhysics == null)
            {
                TowerPhysics = GameObject.FindObjectOfType<TowerPhysics>();
            }
            startY = transform.position.y;
            scale = transform.GetChild(0).localScale;
            scaleTarget = scale - new Vector3(scaleAmount, 0, scaleAmount);
        }

        private void Update()
        {
            if (!FallManager.DidFall && startY - transform.position.y > maxFallDistance)
            {
                FallManager.DidFall = true;
            }

            if (animate)
            {
                Animate();
            }

        }

        public void PlayAnimation()
        {
            animate = true;
        }

        protected bool Equals(BuildingBlock other)
        {
            return base.Equals(other) && name.Equals(other.name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BuildingBlock) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ gameObject.name.GetHashCode();
            }
        }

        private void Animate()
        {
            transform.GetChild(0).localScale = Vector3.SmoothDamp(transform.GetChild(0).localScale, scaleTarget, ref scaleVelocity,
                                                      smoothScaleTime);

            if (!animationFinishing && TargetReached(transform.GetChild(0).localScale.x, scaleTarget.x, scaleThreshold))
            {
                scaleIteration++;

                if (scaleIteration == maxScaleIterations)
                    animationFinishing = true;

                if (animationFinishing)
                    scaleTarget = scale;
                else
                    scaleTarget = scaleTarget.x < scale.x
                                      ? (scale + new Vector3(scaleAmount, 0, scaleAmount))
                                      : (scale - new Vector3(scaleAmount, 0, scaleAmount));
            }

            else if (animationFinishing && TargetReached(transform.GetChild(0).localScale.x, scaleTarget.x, scaleThreshold))
            {
                animate = false;
                animationFinishing = false;
                scaleIteration = 0;
                scaleTarget = scale - new Vector3(scaleAmount, 0, scaleAmount);
            }

        }

        private bool TargetReached(float current, float target, float threshold)
        {
            return Mathf.Abs(target - current) < threshold;
        }


        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag("towerBlock"))
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
                TowerPhysics.Direction();
                transform.SetParent(TowerPhysics.gameObject.transform);
                TowerPhysics.lastBlock = this.transform;
                OnTap.blockLanded = true;
            }
            if (coll.gameObject.CompareTag("Platform"))
            {
                OnTap.blockLanded = true;
                FallManager.DidFall = true;
            }
        }
    }
}