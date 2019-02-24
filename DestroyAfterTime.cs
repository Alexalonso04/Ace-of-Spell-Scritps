using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float destroyAfterSeconds;

    private void Start() {
        StartCoroutine(destroyAfter());
    }

    IEnumerator destroyAfter() {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(gameObject);
    }
}
