using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //[Tooltip("Player Gyroscope")]
    private Transform _gyroscope;
    private Transform _player;
    public Vector3 offset;

    public float pitch = 2f;

    //private float currentZoom;
    // Start is called before the first frame update
    void Awake(){
        _gyroscope = GameObject.Find("Player/Gyroscope").transform;
        _player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = Vector3.Slerp(transform.position, _player.position - offset, Time.time * 0.05f);
        transform.LookAt(_player.position + Vector3.up * pitch);
    }


    void FixedUpdate() {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int layerMask = 1 << 2;
        // layerMask = ~layerMask;
        if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, layerMask)) {
            Debug.Log("HIT PLAPYER");
            if (hitInfo.collider != null) {
                Vector3 direction = hitInfo.point - _gyroscope.position;                           //Calculate the direction vector by substracting the position vector of the target minus the position vector of the player
                direction.y = 0;                                                               //We need to set y = 0 so that we have a planar vector, which will cause only one axis to rotate
                Quaternion rotated = Quaternion.LookRotation(direction);                       //calculates the rotation vector from the result of the previously calculated direction vector
                _gyroscope.rotation = rotated;
            }
        }
    }

    public void setGyroscope(Transform gyroscope){
        _gyroscope = gyroscope;
    }
}
