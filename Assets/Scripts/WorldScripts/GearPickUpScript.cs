using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPickUpScript : MonoBehaviour
{

    public bool inCollider;
    public GameObject player;
    public GameObject originalParent;
    private Vector3 positionOffset;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                this.transform.parent = player.transform;
                positionOffset = new Vector3(0f, 1f, -0.1f);
                transform.position = player.transform.position + player.transform.forward + positionOffset;
                if (!this.GetComponent<BoxCollider>().isTrigger)
                {
                    this.GetComponent<BoxCollider>().enabled = false;
                }
                this.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && this.GetComponent<MeshRenderer>().enabled)
        {
            if (!inCollider)
            {
                this.transform.parent = originalParent.transform;
                if (!this.GetComponent<BoxCollider>().isTrigger)
                {
                    this.GetComponent<BoxCollider>().enabled = true;
                }
                this.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
