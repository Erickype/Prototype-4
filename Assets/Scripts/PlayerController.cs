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
    private float powerUpStrength = 15;
    public GameObject powerupIndicator;

    //Variables for homing misile
    public PowerUpType currentPowerUp = PowerUpType.None;

    public GameObject misilePrefab;
    private GameObject tmpMisile;
    private Coroutine powerUpCoutdown;


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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUpEnum>().powerUpType;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);

            if(powerUpCoutdown != null)
            {
                StopCoroutine(powerUpCoutdown);
            }

            powerUpCoutdown =  StartCoroutine(PowerupCountDownRoutine());
        }
    }

    // Coroutine for countdown of powerup
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
        currentPowerUp = PowerUpType.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Collision with " + collision.gameObject.name + " with powerup state " + hasPowerup);
        }
    }
}
