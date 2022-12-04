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

        private void Update()
        {
            if(homing && target != null)
            {
                Vector3 movingDirection = (target.transform.position - transform.position).normalized;
                transform.position += speed * Time.deltaTime * movingDirection;
                transform.LookAt(transform.position);
            }
        }

        public void Fire(Transform newTarget)
        {
            target = newTarget;
            homing = true;
            Destroy(gameObject, aliveTimer);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(target != null)
            {
                if (collision.gameObject.CompareTag(target.tag))
                {
                    Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                    Vector3 away = -collision.GetContact(0).normal;
                    targetRb.AddForce(away * misileStrength, ForceMode.Impulse);
                    Destroy(gameObject);
                }
            }
        }
    }
}