using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    public class HominMisile : MonoBehaviour
    {
        private Transform target;
        private float speed = 100;
        private bool homing;

        private float misileStrength = 15.0f;
        private float aliveTimer = 5.0f;

        public void Fire(Transform newTarget)
        {
            target = newTarget;
            homing = true;
            Destroy(gameObject, aliveTimer);
        }
    }
}