using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlerFire1 : EnemyController 
{
    public GameObject projectile;
    private Vector3 projectileOffset = new Vector3(0,0,1);
    void Start()
    {
        enemyHealth = 30;
        combatDistance = 8;
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

    public override void EnemyMovement()
    {
        base.EnemyMovement();
        if (Vector3.Distance(transform.position, player.position) >= detectionRange)
        {
            transform.LookAt(player);
            transform.position += transform.forward  * enemySpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.position) <= detectionRange && enemyIsAttacking == false && enemyStamina >= enemyStaminaReduction)
            {
                transform.LookAt(player);
                StartCoroutine(EnemyAttack());
            }
        }
        else
        {
            transform.position -= transform.forward  * enemySpeed * Time.deltaTime;
        }
    }

    public override IEnumerator EnemyAttack()
    {
        enemyIsAttacking = true;
        yield return new WaitForSeconds(enemyAttackWait);
        enemyStamina -= enemyStaminaReduction;
        Instantiate(projectile, transform.position += projectileOffset, Quaternion.identity);
        enemyIsAttacking = false;
        yield return new WaitForSeconds(enemyAttackWait++);
    }
}
