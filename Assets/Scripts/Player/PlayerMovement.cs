using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public bool playerMovable = true;
    private Animator anim;
    private HashIDs hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool sneak = Input.GetButton("Sneak");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");     
        MovementManager(v, sneak);
    }

    private void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        bool sneak = Input.GetButton("Sneak");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout, sneak);
    }

    void MovementManager(float vertical, bool sneaking)
    {
        if (playerMovable)
        {
            anim.SetBool(hash.sneakingBool, sneaking);
            if (vertical > 0 || vertical < 0)
            {
                anim.SetFloat(hash.speedFloat, vertical * 1.5f, speedDampTime, Time.deltaTime);
            }
            else
            {
                anim.SetFloat(hash.speedFloat, 0);
            }
        }
    }

    void AudioManagement(bool shout, bool sneaking)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("WalkBack"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                if (sneaking)
                {
                    GetComponent<AudioSource>().pitch = 0.225f;
                    GetComponent<AudioSource>().Play();
                }
                else
                {
                    GetComponent<AudioSource>().pitch = 0.45f;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}