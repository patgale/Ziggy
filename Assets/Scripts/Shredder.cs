using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour 
{
    void OnTriggerEnter(Collider col)
    {
        var gameManager = GameObject.FindObjectOfType<GameManager>();

        Destroy(col.gameObject);
        gameManager.GameOver();
    }
}