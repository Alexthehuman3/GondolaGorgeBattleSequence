using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGear : MonoBehaviour
{
    public GameObject player;
    public GameObject gear;
    public GearPickUpScript pickUpScript;
    public Animator worldAnim;
    public Animator enemyAnim;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                worldAnim.SetBool("gearPickedUp", true);
                pickUpScript.inCollider = true;
                enemyAnim.SetBool("moveRocketDown", true);
            }
        }
    }
}
