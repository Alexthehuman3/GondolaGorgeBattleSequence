using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minimumDist = 0.2f;
    public float maximumDist = 3f;
    public float smoothing = 10.0f;
    Vector3 normalizedDir;
    public Vector3 adjustedNormDir;
    public float dist2Player;

    private void Awake()
    {
        normalizedDir = transform.localPosition.normalized;
        dist2Player = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(normalizedDir * maximumDist);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position,desiredCameraPos,out hit))
        {
            dist2Player = Mathf.Clamp((hit.distance * 0.9f), minimumDist, maximumDist);
        }
        else
        {
            dist2Player = maximumDist;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, normalizedDir * dist2Player, Time.deltaTime * smoothing);
    }
}
