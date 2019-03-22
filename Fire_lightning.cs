using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_lightning : MonoBehaviour
{
    public GameObject lightning;
    public Transform position;
    private GameObject target;
    public int radius;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (LookforTarget() != null)
            {
                Debug.Log("Instantiating lightning");
                Instantiate(lightning, position.position, Quaternion.identity);
             
            }
            else { Debug.Log("No Available Target!"); }
        }
     
    }
    private GameObject LookforTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject mainTarget in enemies)
        {
            float distance = Vector3.Distance(mainTarget.transform.position, transform.position);
            if (distance < radius)
            {
                target = mainTarget;
                //target.GetComponent<Rigidbody>().AddForce(target.transform.position * 10);
                return mainTarget;
            }
        }

        return null;
    }

    public GameObject ReturnTarget()
    {
        return target;
    }
    public void SetTarget()
    {
        target = null;
    }
}
