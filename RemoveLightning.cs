using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLightning : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
        Destroy(gameObject, 5); //destroys this GameObject after everything is done
    }
}
