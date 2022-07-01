using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTargetScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerCamera;
    Ray ray;
    RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = PlayerCamera.transform.position;
        ray.direction = PlayerCamera.transform.forward;
        Physics.Raycast(ray, out hitInfo);
        transform.position = hitInfo.point;
    }
}
