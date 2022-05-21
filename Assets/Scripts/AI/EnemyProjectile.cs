using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    public int projectileDamage = 10;
    public float projectileSpeed = 10;
    private Rigidbody rb;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerPos = player.transform.position;
        transform.LookAt(playerPos);
    }

    void Update()
    {
        rb.AddForce(transform.forward * projectileSpeed * Time.deltaTime,ForceMode.Impulse);
        if (Vector3.Distance(transform.position, playerPos) > 30)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerControler>().playerHealth -= projectileDamage;
        }

        if (other.gameObject.CompareTag("RoomManager"))
        {
            Destroy(gameObject);
        }
    }
}
