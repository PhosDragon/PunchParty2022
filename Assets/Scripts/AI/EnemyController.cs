 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 public class EnemyController : MonoBehaviour
 {
 
     private Rigidbody enemyRB;
     private BoxCollider enemyAttackBox; 
     public Transform player;
     public int enemySpeed = 4;
     public float combatDistance = 4f;
     public float detectionRange = 1f;
     public float enemyHealth = 60f;
     public float enemyDamage = 5f;
     public float enemyDashSpeed = 5f;
     public float enemyStamina = 100f;
     public float enemyStaminaCap = 100f;
     public float enemyStaminaReduction = 30f;
     public float enemyAttackWait = 1f;
     public bool enemyIsAttacking = false;

 
     void Start()
    {
        enemyRB = gameObject.GetComponent<Rigidbody>();
        enemyAttackBox = gameObject.GetComponent<BoxCollider>();
        enemyAttackBox.enabled = false;
    }
 
     void Update()
    {
        if (enemyStamina< enemyStaminaCap)
        {
            StartCoroutine(EnemyStaminaRegen());
        }
        if (enemyHealth == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        EnemyMovement();
    }
    public IEnumerator EnemyStaminaRegen()
    {
        yield return new WaitForSeconds(3);
        enemyStamina += 5f;
    }
    
    public virtual void EnemyMovement()
    {
        if (Vector3.Distance(transform.position, player.position) >= detectionRange)
        {

         transform.LookAt(player);
         transform.position += transform.forward * enemySpeed * Time.deltaTime;
             
            if (Vector3.Distance(transform.position, player.position) <= combatDistance && enemyIsAttacking == false && enemyStamina > enemyStaminaReduction)
            {
                StartCoroutine(EnemyAttack());
            }
        }
    }
        public virtual IEnumerator EnemyAttack()
    {
        enemyIsAttacking = true;
        yield return new WaitForSeconds(enemyAttackWait);
        enemyStamina -= enemyStaminaReduction;
        enemyAttackBox.enabled = true;
        yield return new WaitForSeconds(enemyAttackWait++);
        enemyAttackBox.enabled = false;
        enemyRB.velocity = Vector3.zero;
        enemyRB.angularVelocity = Vector3.zero;
        enemyIsAttacking = false;
        Debug.Log("melee enemy attacking " + player.gameObject.GetComponent<PlayerControler>().playerHealth);
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerControler>().playerHealth -= enemyDamage;
        }
    }
}
