using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    private CameraController _cameraController;
    private Transform _gyroscope;
    private Transform _lastCheckpointPosition;
    public GameObject playerPrefab;

    [Header("Player Respawn")]
    public float respawnAfterSeconds;
    //public ParticleSystem respawnEffect;

    public static GameController Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
        _cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }

    public IEnumerator Respawn(){
        yield return new WaitForSeconds(respawnAfterSeconds);
        //Instantiate(respawnEffect, lastCheckpointPosition.position, Quaternion.identity);
        GameObject player = Instantiate(playerPrefab, _lastCheckpointPosition.position, Quaternion.identity);
        _cameraController.setGyroscope(GameObject.Find("Player/Gyroscope").transform);
    }

    public void updateCheckpoint(Transform checkpoint){
        _lastCheckpointPosition = checkpoint;
    }
}
