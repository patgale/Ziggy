using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour 
{
    public GameObject floor;
    public GameObject star;
    public GameObject snail;
    public float minLength, maxLength, minWidth, maxWidth;

    [Range(0.001f, 0.10f)]
    public float precentChanceOfStar;

    Vector3 lastPosition;
    Vector3 lastScale;
    Vector3 newScale;
    bool turned = false;
    Ball ball;
    float aspect;

	void Start () 
    {
        aspect = GameManager.GetScreenAspect();

        BuildInitialFloors();
	}
	
	void Update () 
    {
        if (ball)
        {
            Debug.Log("Floor Check!");
            CheckIfNewFloorIsNeeded();
        }
	}

    public void BuildInitialFloors()
    {
        ball = GameObject.FindObjectOfType<Ball>();

        RemovePowerUps(); // remove any previous power ups

        var floors = GameObject.FindGameObjectsWithTag("floor");
        for (var i = 0; i < floors.Length; i++)
        {
            Destroy(floors[i]);
        }

        var firstFloor = GameObject.Instantiate(floor);
        var size = Random.Range(3.5f, 5.0f);
        firstFloor.transform.localScale = new Vector3(size, 1.0f, size) * aspect;
        firstFloor.transform.position = new Vector3(0, 0, 0);
        firstFloor.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        lastScale = firstFloor.transform.localScale;
        lastPosition = firstFloor.transform.position;

        for (var i = 0; i < 30; i++)
        { GetNewFloor(); }

        ball = GameObject.FindObjectOfType<Ball>();
    }

    private void CheckIfNewFloorIsNeeded()
    {
        var floors = GameObject.FindGameObjectsWithTag("floor");

        for (var i = 0; i < floors.Length; i++)
        {
            // 0 < 20.71822 - 26 (-6.71822) FALSE
            // 0 < (20.71822 * 0.75) - 20 (-5) TRUE
            if ((floors[i].transform.position.z / aspect < ball.transform.position.z - 15) || 
                (floors[i].transform.position.x / aspect < ball.transform.position.x - 15))
            {
                GetNewFloor();
                Destroy(floors[i]);
            }
        }
    }

    void GetNewFloor()
    {
        var newFloor = GameObject.Instantiate(floor);
        var length = Random.Range(minLength, maxLength);
        var width = Random.Range(minWidth, maxWidth);
        var rotationY = 0.0f;

        if (!turned)
        {          
            rotationY = 90.0f;
        }

        newScale = new Vector3(length, 1, width) * aspect;
        var newPosition = GetNewPosition();

        newFloor.transform.rotation = Quaternion.Euler(new Vector3(0, rotationY, 0));
        newFloor.transform.localScale = newScale;
        newFloor.transform.position = newPosition;
        lastScale = newScale;

        GetPowerUps(newPosition);
    }

    void RemovePowerUps()
    {
        var stars = GameObject.FindObjectsOfType<Star>();
        foreach (var star in stars)
        {
            Destroy(star);
        }
        var snails = GameObject.FindObjectsOfType<Snail>();
        foreach(var snail in snails)
        {
            Destroy(snail);
        }

    }

    void GetPowerUps(Vector3 newPosition)
    {
        // STARS
        bool newStar = false;
        var randomNum = Random.value * 0.1;
        if (randomNum <= precentChanceOfStar)
        {
            SetNewPowerup(newPosition, star);
        }

        // SNAILS
        if (ball & !newStar && (ball.speed - ball.startingSpeed > 2.0))
        {
            if (Random.value < 0.03f) // 3% chance of snail showing up when speed is doubled from start
            {
                SetNewPowerup(newPosition, snail);
            }
        }
    }

    void SetNewPowerup(Vector3 floorPosition, GameObject newPowerup)
    {
        var newObject = GameObject.Instantiate(newPowerup);
        newObject.transform.localScale *= aspect;
        newObject.transform.position = new Vector3(floorPosition.x, 0.7f, floorPosition.z);    
    }

    void GetNewStar(Vector3 floorPosition)
    {
        var newX = floorPosition.x;
        var newZ = floorPosition.z;

        if (!turned)
        {
            newX += (Random.Range(-(newScale.x / 2), (newScale.x / 2)));
        }
        else if (turned)
        {
            newZ += (Random.Range(-(newScale.x / 2), (newScale.x / 2))); 
        }

        var newStar = GameObject.Instantiate(star);
        newStar.transform.localScale *= aspect; 
        newStar.transform.position = new Vector3(newX, 0.66f, newZ);        
    }

    Vector3 GetNewPosition()
    {
        //not turned
        //lastPosition.X + (lastScale.Y / 2) + (newScale.X /2)
        //lastPosition.y + (lastScale.x / 2) - (newScale.y / 2)

        //turned
        //lastPosition.X + (lastScale.X / 2) - (newScale.Y / 2)
        //lastPosition.y + (newScale.x / 2) + (lastScale.y /2)

        float newX = lastPosition.x;
        float newZ = lastPosition.z;

        if (turned)
        {
            newX += (lastScale.z / 2) + (newScale.x / 2);
            newZ += (lastScale.x / 2) - (newScale.z / 2);
        }
        else if (!turned)
        {
            newX += (lastScale.x / 2) - (newScale.z / 2);
            newZ += (newScale.x / 2) + (lastScale.z / 2);
        }

        turned = !turned;
        var newPosition = new Vector3(newX, 0, newZ);
        lastPosition = newPosition;
        return newPosition;
    }
}
