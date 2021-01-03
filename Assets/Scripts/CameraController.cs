using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followedObject;

    public void Update()
    {
        transform.position =new Vector3(followedObject.position.x, followedObject.position.y, transform.position.z);
    }
}
