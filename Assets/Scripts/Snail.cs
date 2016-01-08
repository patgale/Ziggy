using UnityEngine;
using System.Collections;

public class Snail : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }
}
