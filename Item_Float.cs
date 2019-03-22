using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Float : MonoBehaviour
{

    public float amp;
    public float speed = 0.0f;
    float y0;

    public float rotationSpeed;
    float rot;
    
    void Start(){
        y0 = transform.position.y;
    }
    void Update()
    {   
        transform.position= new Vector3(transform.position.x, y0 + amp*Mathf.Sin(speed*Time.time), transform.position.z);
        // transform.position.y = ;

        // Rotation
        rot = rotationSpeed * Time.deltaTime;
        transform.Rotate(rot, rot, rot);
    }
}
