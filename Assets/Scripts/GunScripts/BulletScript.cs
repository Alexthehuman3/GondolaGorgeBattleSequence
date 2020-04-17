using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Vector3 newPos;
    public bool isFired;

    private void Start()
    {
        StartCoroutine(waitTillDestroy());
    }

    private void Update()
    {
        transform.position += newPos;
    }

    IEnumerator waitTillDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

}
