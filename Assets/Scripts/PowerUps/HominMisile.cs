using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    public class HominMisile : MonoBehaviour
    {
        private Rigidbody enemyRb;

        private Transform playerTrans;
        public float speed = 100;

        // Use this for initialization
        void Start()
        {
             playerTrans = GameObject.Find("Player").transform;
             enemyRb = GetComponent<Rigidbody>(); 
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 lookDirection = (transform.position - playerTrans.position).normalized;

            enemyRb.AddForce(speed * lookDirection);

            StartCoroutine(DestroyCountdown());
        }

        IEnumerator DestroyCountdown()
        {
            yield return new WaitForSeconds(7);
            Destroy(gameObject);
        }
    }
}