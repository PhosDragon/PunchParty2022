 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private bool grounded;
    private BoxCollider playerHitBox;
    private Rigidbody playerRb;
    private float verticleInput;
    private float horizontalInput;
    public float playerSpeed = 5f;
    private float rotationSpeed = 2f;
    public float dashForce = 6f; 
    public float playerHealth = 100f; 
    public float playerDamage = 10f;
    public float playerStamina = 100f;
    public int playerStaminaCap = 100;
    public int playerStaminaRegen = 1;
    public int playerStaminaReduction = 30;
    public float attackTime = 1f;

     void Start()
    {
        playerHitBox = gameObject.GetComponent<BoxCollider>();
        playerHitBox.enabled = false;
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerAttackCollider();
        PlayerMovement();
        if (playerStamina < playerStaminaCap)
        {
            playerStamina += playerStaminaRegen * Time.deltaTime;
        }
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void PlayerMovement()
    {
            verticleInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.forward * verticleInput* playerSpeed * Time.deltaTime);
            transform.Rotate(0f, horizontalInput, 0f * rotationSpeed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerStamina >= playerStaminaReduction)
            {
                playerRb.AddForce(transform.forward * dashForce,ForceMode.Impulse);
                playerStamina -= playerStaminaReduction;
                StartCoroutine(PlayerDash());
            }
    }
        IEnumerator PlayerDash()
    {
        yield return new WaitForSeconds(1);
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }

    private void playerAttackCollider()
    {
        if (Input.GetMouseButtonDown(0) && playerStamina >= playerStaminaReduction)
        {
            playerStamina -= playerStaminaReduction;
            StartCoroutine(AttackBox());
        }
    }
    IEnumerator AttackBox()
    {
        playerHitBox.enabled = true;
        yield return new WaitForSeconds (attackTime);
        playerHitBox.enabled = false;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().enemyHealth -= playerDamage;
        }
    }
}
