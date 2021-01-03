using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed=35;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 normalizedDeltaPos = speed * Time.deltaTime * Vector3.up;
        transform.Translate(normalizedDeltaPos);
            
    }
}
