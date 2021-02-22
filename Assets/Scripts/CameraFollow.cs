using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    public float[] posLimit = new float [4]; //x1, y1, x2, y2
    public float sSpeed = 0.125f;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector2 smoothedPos = Vector2.Lerp(transform.position, desiredPos, sSpeed);
        transform.position = new Vector3 (Mathf.Clamp(smoothedPos.x, posLimit[0], posLimit[2]), Mathf.Clamp(smoothedPos.y, posLimit[1], posLimit[3]), -10);
    }
}
