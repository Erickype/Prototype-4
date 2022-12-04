using Assets.Scripts.PowerUps;
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

    //Variables for smash
    public float hangTime = 2;
    public float smashSpeed = 5;
    public float explosionForce = 5;
    public float explosionRadius = 5;
    bool smashing = false;
    float floorY;

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

        if (currentPowerUp == PowerUpType.HomingRocket && Input.GetKeyDown(KeyCode.F))
        {
            FireMisiles();
        }

        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(SmashFloor());
        }

    }

    IEnumerator SmashFloor()
    {
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;

        float jumpTime = Time.time + hangTime;

        while(Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);

            yield return null;
        }

        while(transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);

            yield return null;
        }

        foreach (var enemy in enemies)
        {
            if(enemy != null)
            {
                enemy.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }

        smashing = false;
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
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Normal)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Collision with: " + collision.gameObject.name + "\nWith powerup state: " + currentPowerUp.ToString());
        }
    }

    void FireMisiles()
    {
        var enemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            tmpMisile = Instantiate(misilePrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpMisile.GetComponent<HominMisile>().Fire(enemy.transform);
        }
    }
}
