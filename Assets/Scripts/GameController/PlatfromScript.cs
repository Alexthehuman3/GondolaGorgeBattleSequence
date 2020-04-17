using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromScript : MonoBehaviour
{
    public Animator fadeOut;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fadeOut.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("readyToFinish", true);
            other.transform.parent = this.transform;
            StartCoroutine(freezePlayerMovement(other.gameObject));
            fadeOut.gameObject.SetActive(true);
            fadeOut.Play("fadeToBlack");
        }
    }

    IEnumerator freezePlayerMovement(GameObject player)
    {
        yield return new WaitForSeconds(3);
        PlayerMovement pMovement = player.GetComponent<PlayerMovement>();
        pMovement.anim.SetFloat(pMovement.hash.speedFloat, 0);
        pMovement.playerMovable = false;
    }
}
