using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public float rotationSpeed = 10;
    private float rotX;
    private float rotY;
    public float clampYMin;
    public float clampYMax;
    public float charge;
    public float chargeRate = 10f;
    public float maxChargeTime = 200f;
    public bool mouseControlMode = false;
    public bool fired = false;
    public GameObject rotatingUpDownObj;
    private CameraController camControl;
    public Camera gunCam;


    private float fireRate = 10.0f;
    private float coolDown;
    private GameObject player;

    private void Start()
    {
        camControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 rot = transform.localRotation.eulerAngles; //gets local rotation
        rotX = rot.x;
        rotY = rot.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mouseControlMode && gunCam.isActiveAndEnabled)
        {
            handleGunMovement();
            fireHandler();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            camControl.playerInGunCollider = true;
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
            charge += chargeRate*Time.deltaTime;
            fired = false;
        }

        if (Input.GetKeyUp(KeyCode.F) && charge >= maxChargeTime)
        {
            fired = true;
            //instantiate bullet
            //if bullet exists
            coolDown = Time.time + fireRate;
            charge = 0;
        }
        else if (Input.GetKeyUp(KeyCode.F) && charge < maxChargeTime)
        {
            charge = 0;
            fired = false;
        }
    }
}