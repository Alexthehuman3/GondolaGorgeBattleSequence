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
    public GameObject activationText;
    public GameObject sliderBar;
    public GameObject coolDown;
    public GameObject terminalSmoke;
    private GameObject player;
    private PlayerMovement playerMovement;

    public bool playerInGunCollider;
    public bool inCartAndActivated;
    public bool gearFellOff;
    public bool gearReplaced;
    public bool animEnd;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        switchCamActive(playerCam, gunCam, cartCam);
        sliderBar.SetActive(false);
        terminalSmoke.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && playerInGunCollider)
        {
            if (!gunCam.isActiveAndEnabled)
            {
                activationText.SetActive(false);
                switchCamActive(gunCam, playerCam, cartCam);
                playerBody.enabled = false;
                playerGlasses.enabled = false;
                playerMovement.anim.SetFloat(playerMovement.hash.speedFloat, 0);
                playerMovement.playerMovable = false;
                gunReloadAnim.SetBool("playerInGun", true);
                sliderBar.SetActive(true);
            }
            else
            {
                Debug.Log("Player Exited");
                switchCamActive(playerCam, gunCam, cartCam);
                playerBody.enabled = true;
                playerGlasses.enabled = true;
                playerMovement.playerMovable = true;
                gunReloadAnim.SetBool("playerInGun", false);
                sliderBar.SetActive(false);
                coolDown.SetActive(false);
            }
        }

        if (inCartAndActivated)
        {
            switchCamActive(cartCam, playerCam, gunCam);
            inCartAndActivated = false;
        }

        if (gearFellOff)
        {
            switchCamActive(playerCam, cartCam, gunCam);
            terminalSmoke.SetActive(true);
            gearFellOff = false;
        }

        if (gearReplaced)
        {
            switchCamActive(cartCam, playerCam, gunCam);
            gearReplaced = false;
        }

        if (animEnd)
        {
            switchCamActive(playerCam, cartCam, gunCam);
            animEnd = false;
        }
    }

    public void switchCamActive(Camera activeCam, Camera camToInactive1, Camera camToInactive2)
    {
        activeCam.enabled = true;
        camToInactive1.enabled = false;
        camToInactive2.enabled = false;
    }
}
