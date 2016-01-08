using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour 
{
    public GameObject star;
    public GameObject snail;
    
    [Range(0.001f, 0.10f)]
    public float precentChanceOfStar;

    public void RemovePowerUps()
    {
        var stars = GameObject.FindObjectsOfType<Star>();
        foreach (var star in stars)
        {
            Destroy(star);
        }
        var snails = GameObject.FindObjectsOfType<Snail>();
        foreach (var snail in snails)
        {
            Destroy(snail);
        }

    }

    void GetNewSnail(Vector3 floorPosition, float aspect)
    {
        var newSnail = GameObject.Instantiate(snail);
        newSnail.transform.localScale *= aspect;
        newSnail.transform.position = new Vector3(floorPosition.x, 0.7f, floorPosition.z);
    }

    void GetNewStar(Vector3 floorPosition, float newScaleX, bool turned, float aspect)
    {
        var newX = floorPosition.x;
        var newZ = floorPosition.z;

        if (!turned)
        {
            newX += (Random.Range(-(newScaleX / 2), (newScaleX / 2)));
        }
        else if (turned)
        {
            newZ += (Random.Range(-(newScaleX / 2), (newScaleX / 2)));
        }

        var newStar = GameObject.Instantiate(star);
        newStar.transform.localScale *= aspect;
        newStar.transform.position = new Vector3(newX, 0.7f, newZ);
    }
}