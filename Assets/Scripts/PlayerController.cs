using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float moveSpeed = 5;
    public bool hasPowerup = false;
    private PowerUpEnum powerUpType;
    private float powerUpStrength = 15;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(moveSpeed * verticalInput * focalPoint.transform.forward);
        powerupIndicator.transform.position = transform.position - new Vector3(0, 0.5f, 0);

        if (hasPowerup)
        {
            switch (powerUpType.powerUpSelector)
            {
                case PowerUpEnum.PowerUp.Normal:
                    break;
                case PowerUpEnum.PowerUp.HomingRocket:
                    ShootHomingMisiles();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            powerUpType = other.GetComponent<PowerUpEnum>();
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    private void ShootHomingMisiles()
    {
        Debug.Log("Shooting");
    }

    // Coroutine for countdown of powerup
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup && powerUpType.powerUpSelector == PowerUpEnum.PowerUp.Normal)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Collision with " + collision.gameObject.name + " with powerup state " + hasPowerup);
        }
    }
}
