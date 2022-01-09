using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Transform target;
    public float smoothing;
    Vector3 offset;
    float lowY = 30f;
    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        if (transform.position.y < -lowY) transform.position = new Vector3(transform.position.x, -lowY, transform.position.z);
        if (transform.position.y > lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
    }
}
