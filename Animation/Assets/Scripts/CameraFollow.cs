using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float Interpolation = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Target.position + Offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, Interpolation) + Offset; // 0.01f pour 1% de vitesse de follow 
    }
}
