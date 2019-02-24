using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    // Start is called before the first frame update
    private GameController _gameController;
    void Awake() {
        _gameController = GameController.Instance;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _gameController.updateCheckpoint(transform);
            Destroy(gameObject);
        }
    }
}
