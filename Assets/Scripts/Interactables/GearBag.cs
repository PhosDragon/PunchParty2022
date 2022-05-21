using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBag : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Player;
    private Vector3 resetPoint = new Vector3(-4.14f,1f,2.19f);
    IEnumerator PlayerSwap()
    {
        Camera.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Player.transform.position = resetPoint;
        Camera.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerSwap());
        }
    }
}
