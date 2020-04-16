using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 followPos;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX; //originall planned to include joystick movement but didn't.
    public float finalInputZ; //originally planned for joystic movement but didn't
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    public bool usingADControl = true;
    public GameObject CameraObj;
    public GameObject player;

    public bool invert = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //Locks and hides camera
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 rot = transform.localRotation.eulerAngles; //gets local rotation
        rotY = rot.y;
        rotX = rot.x;
    }

    private void Update()
    {
        //GetAxisHorizontal/Vertical Code here
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (usingADControl)
        {
            finalInputX = Input.GetAxis("Horizontal");
        }
        else
        {
            finalInputX = mouseX;
            finalInputZ = mouseY;
        }

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        if (!usingADControl)
        {
            if (invert)
            {
                rotX -= finalInputZ * inputSensitivity * Time.deltaTime;
            }
            else
            {
                rotX += finalInputZ * inputSensitivity * Time.deltaTime;
            }
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        }
        Quaternion localRot = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRot;
        player.transform.rotation = Quaternion.Euler(0, rotY, 0);
    }

    private void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        Transform target = CameraFollowObj.transform;

        float move = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, move);
    }
}
