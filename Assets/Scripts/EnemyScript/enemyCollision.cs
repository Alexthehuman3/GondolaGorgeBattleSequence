using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollision : MonoBehaviour
{
    public GameObject explosion;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }
    }
}
