using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour 
{
    void OnTriggerEnter(Collider col)
    {
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }
}
