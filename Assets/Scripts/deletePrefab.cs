using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletePrefab : MonoBehaviour
{
    public float secondsToDestroy;
    private void Start()
    {
        if (secondsToDestroy!=0)
        {
            StartCoroutine(deleteObj());
        }
    }
    IEnumerator deleteObj()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(this.gameObject);
    }
}
