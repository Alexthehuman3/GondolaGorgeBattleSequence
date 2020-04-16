using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCam;
    public Camera gunCam;
    public Camera cartCam;
    public Animator gunReloadAnim;
    public SkinnedMeshRenderer playerBody;
    public SkinnedMeshRenderer playerGlasses;
    private GameObject player;
    private PlayerMovement playerMovement;

    public bool playerInGunCollider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        switchCamActive(playerCam, gunCam, cartCam);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && playerInGunCollider)
        {
            if (!gunCam.isActiveAndEnabled)
            {
                switchCamActive(gunCam, playerCam, cartCam);
                playerBody.enabled = false;
                playerGlasses.enabled = false;
                playerMovement.playerMovable = false;
                gunReloadAnim.SetBool("playerInGun", true);
            }
            else
            {
                switchCamActive(playerCam, gunCam, cartCam);
                playerBody.enabled = true;
                playerGlasses.enabled = true;
                playerMovement.playerMovable = true;
                gunReloadAnim.SetBool("playerInGun", false);
            }
        }
    }

    public void switchCamActive(Camera activeCam, Camera camToInactive1, Camera camToInactive2)
    {
        activeCam.enabled = true;
        camToInactive1.enabled = false;
        camToInactive2.enabled = false;
    }
}
