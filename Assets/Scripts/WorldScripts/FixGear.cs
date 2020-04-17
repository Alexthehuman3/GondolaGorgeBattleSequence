using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGear : MonoBehaviour
{
    public GameObject player;
    public GameObject gear;
    public GameObject activationText;
    public GameObject smoke;
    public GearPickUpScript pickUpScript;
    public Animator worldAnim;
    public Animator enemyAnim;
    public Animator enemy2Anim;

    private CameraController camControl;
    private bool activated;
    private void Start()
    {
        activationText.SetActive(false);
        camControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (!activated)
            {
                activationText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.P) && gear.transform.parent == player.transform)
            {
                PlayerMovement pMovement = player.GetComponent<PlayerMovement>();
                worldAnim.SetBool("gearPickedUp", true);
                pickUpScript.inCollider = true;
                enemyAnim.SetBool("moveRocketDown", true);
                enemy2Anim.SetBool("drop", true);
                activationText.SetActive(false);
                pMovement.playerMovable = false;
                pMovement.anim.SetFloat(pMovement.hash.speedFloat, 0);
                camControl.gearReplaced = true;
                activated = true;
                smoke.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            activationText.SetActive(false);
        }
    }
}
