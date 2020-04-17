using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveLeftRight : MonoBehaviour
{
    public float rotationSpeed = 10;
    private float rotX;
    private float rotY;
    public float clampYMin;
    public float clampYMax;
    public float charge;
    public float bulletSpeed = 10f;
    public float chargeRate = 10f;
    public float maxChargeTime = 200f;
    public bool mouseControlMode = false;
    public bool fired = false;
    public GameObject rotatingUpDownObj;
    public GameObject bullet;
    public GameObject entireShip;
    public GameObject ship2;
    public GameObject platform;
    public GameObject activationText;
    public GameObject chargeEffect;
    public GameObject coolDownUI;
    public Transform bulletAnchor;
    public Transform pivot;
    private CameraController camControl;
    public Camera gunCam;
    public Camera thirdPersonCam;
    public Animator reloadAnim;
    public Animator shootBackAnim;
    public Slider chargeBar;
    
    private float fireRate = 10.0f;
    private float coolDown;
    private Vector3 newPos;
    private GameObject player;

    private void Start()
    {
        coolDownUI.SetActive(false);
        gunCam.gameObject.SetActive(false);
        activationText.SetActive(false);
        chargeEffect.SetActive(false);
        camControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 rot = transform.localRotation.eulerAngles; //gets local rotation
        rotX = rot.x;
        rotY = rot.y;
        newPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        chargeBar.value = charge;
        if (!mouseControlMode && (gunCam.isActiveAndEnabled || thirdPersonCam.isActiveAndEnabled))
        {
            handleGunMovement();
            fireHandler();
            if (fired)
            {
                bullet.transform.position += newPos;
            }
        }

        if (!entireShip.activeInHierarchy && !ship2.activeInHierarchy)
        {
            platform.GetComponent<Animator>().SetBool("alienShotDown", true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            camControl.playerInGunCollider = true;
            activationText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            camControl.playerInGunCollider = false;
            activationText.SetActive(false);
        }
    }

    private void handleGunMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotY -= rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotY += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rotX -= rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rotX += rotationSpeed * Time.deltaTime;
        }
        rotX = Mathf.Clamp(rotX, clampYMin, clampYMax);
        Quaternion localRotY = Quaternion.Euler(0, rotY, 0.0f);
        Quaternion localRotX = Quaternion.Euler(rotX, 0.0f, 0.0f);
        transform.rotation = localRotY;
        rotatingUpDownObj.transform.localRotation = localRotX;
    }

    private void fireHandler()
    {
        if (Input.GetKey(KeyCode.F) && Time.time > coolDown)
        {
            thirdPersonCam.gameObject.SetActive(false);
            gunCam.gameObject.SetActive(true);
            charge += chargeRate*Time.deltaTime;
            charge = Mathf.Clamp(charge, 0, 200);
            chargeEffect.SetActive(true);
            fired = false;
        }

        if (Time.time > coolDown)
        {
            coolDownUI.SetActive(false);
        }
    

        if (Input.GetKeyUp(KeyCode.F) && charge >= maxChargeTime)
        {
            gunCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);
            fireBullet();
            coolDown = Time.time + fireRate;
            chargeEffect.SetActive(false);
            charge = 0;
            coolDownUI.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F) && charge < maxChargeTime)
        {
            gunCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);
            chargeEffect.SetActive(false);
            charge = 0;
        }
    }

    public void fireBullet()
    {
        GameObject instBullet = Instantiate(bullet, bulletAnchor.position, pivot.rotation);
        instBullet.GetComponent<BulletScript>().newPos = bulletSpeed * instBullet.transform.forward * Time.deltaTime;
        instBullet.tag = "bullet";
        reloadAnim.SetBool("fired", true);
        shootBackAnim.SetBool("fired", true);
        StartCoroutine(waitTillAnimPlays());
    }

    IEnumerator waitTillAnimPlays()
    {
        yield return new WaitForSeconds(2.5f);
        reloadAnim.SetBool("fired", false);
        shootBackAnim.SetBool("fired", false);
    }
}