using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnimTrigger : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private PlayerMovement pMovement;
    private CameraController camControl;
    public GameObject playerCartParent;
    public GameObject activationText;

    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        activationText.SetActive(false);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        pMovement = player.GetComponent<PlayerMovement>();
        camControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (!pressed)
            {
                activationText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                anim.SetBool("pPressed", true);
                activationText.SetActive(false);
                pMovement.playerMovable = false;
                pMovement.anim.SetFloat(pMovement.hash.speedFloat, 0);
                pressed = true;
                camControl.inCartAndActivated = true;
            }
        }
    }

    public void setButtonPressed()
    {
        anim.SetBool("ButtonPressed", true);
        anim.SetBool("playerEntered", true);
        player.transform.parent = playerCartParent.transform;
    }

    public void releasePlayerFromCart()
    {
        player.transform.parent = null;
    }

    public void freePlayerFromCutscene()
    {
        pMovement.playerMovable = true;
        camControl.gearFellOff = true;
    }

    public void freePlayerFromFinal()
    {
        pMovement.playerMovable = true;
        camControl.animEnd = true;
    }
}
