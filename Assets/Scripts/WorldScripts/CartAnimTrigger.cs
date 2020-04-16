using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnimTrigger : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    public GameObject playerCartParent;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                anim.SetBool("pPressed", true);
                
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
}
