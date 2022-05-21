using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawnManager : MonoBehaviour
{
    public GameObject newRoom;
    public Vector3 newRoomOffset;
    private BoxCollider boxCollider;
    private Quaternion roomRotation;
    private int distace = 70;
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
        newRoomOffset = new Vector3(0,0,0);
    }
    void CreateRoom()
    { // change new room to array of rooms
        Instantiate(newRoom,transform.position + Vector3.forward * distace, Quaternion.identity);
    }
    void OnTriggerEnter (Collider other)
    {        //pass in a 10 as the max for normal rooms and go to a boss room if the limits met
        if (other.gameObject.CompareTag("Player"))
        {
            CreateRoom();
            other.gameObject.transform.position = transform.position + Vector3.forward * distace;
        }
    }
}
