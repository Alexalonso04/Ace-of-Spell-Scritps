using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Player Gyroscope")]
    public Transform indicator;
    public Vector3 offset;

    public float pitch = 2f;

    //private float currentZoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = indicator.position - offset;
        transform.LookAt(indicator.position + Vector3.up * pitch);
    }

    void FixedUpdate() {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo)) {
            if (hitInfo.collider != null) {
                Vector3 direction = hitInfo.point - indicator.position;                           //Calculate the direction vector by substracting the position vector of the target minus the position vector of the player
                direction.y = 0;                                                               //We need to set y = 0 so that we have a planar vector, which will cause only one axis to rotate
                Quaternion rotated = Quaternion.LookRotation(direction);                       //calculates the rotation vector from the result of the previously calculated direction vector
                indicator.rotation = rotated;
            }
        }
    }

}
