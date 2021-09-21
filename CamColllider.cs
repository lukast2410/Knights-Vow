using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamColllider : MonoBehaviour
{
    private float minDistance = 0.8f;
    private float maxDistance = 3.6f;
    private float smooth = 7.0f;
    Vector3 dollyDir;
    public Vector3 adjustDollyDir;
    public float distance;


    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        var ignore = ~(1 << 8);
        if(Physics.Linecast(transform.parent.position, cameraPos, out hit, ignore))
        {
            distance = Mathf.Clamp(hit.distance * 0.9f, minDistance, maxDistance);
            Debug.Log(hit.collider);
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
