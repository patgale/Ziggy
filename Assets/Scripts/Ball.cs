using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour 
{
    public float startingSpeed;
    public float speed;
    public ParticleSystem[] particleTrails;
    public Material[] materials;

    ParticleSystem particleTrail;
    Rigidbody rigidBody;
    bool moveRight = true;
    public bool autoPlay; //TODO set to Private
    public bool isActive = false; // TODO set to Private
    GameManager gameManager;
    int referenceId;

	void Start () 
    {
        referenceId = PlayerPrefs.GetInt("BallId");

        SetColor();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        startingSpeed = speed;

        //speed *= GameManager.GetScreenAspect();

        rigidBody = GetComponent<Rigidbody>();
        
        particleTrail.enableEmission = false;
	}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ChangeDirection();
        }
    }
	
	void FixedUpdate () {
        if (transform.position.y < 0f)
        {
            SetActive(false);
        }

        if (isActive)
        {
            //if (moveRight)
            //    transform.Rotate(0, 1, -(40 * speed));
            //else
            //{
            //    transform.Rotate(40 * speed, 1, 0);
            //}

            if (autoPlay)
            {
                AutoMoveBall();
            }
            else
            {
                MoveBall();
            }
        }
	}

    void SetColor()
    {
        GetComponent<Renderer>().material = materials[referenceId];
        particleTrail = GameObject.Instantiate(particleTrails[referenceId]);
        particleTrail.transform.parent = transform;
        Debug.Log(particleTrail.transform.position);
        particleTrail.transform.position = new Vector3(0, 0, 0);
        Debug.Log(particleTrail.transform.position);
    }

    void AutoMoveBall()
    {
        //var currentX = transform.position.x;
        //var currentZ = transform.position.z;

        //if (!moveRight)
        //{
        //    if (currentX )
        //    transform.position = new Vector3(currentX += speed, transform.position.y, currentZ);
        //}
        //else
        //{
        //    transform.position = new Vector3(currentX, transform.position.y, currentZ += speed);
        //}
    }

    private void MoveBall()
    {
        var currentX = transform.position.x;
        var currentZ = transform.position.z;

        if (!moveRight)
        {
            transform.position = new Vector3(currentX += speed, transform.position.y, currentZ);
            //rigidBody.AddForce(10 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        else
        {
            transform.position = new Vector3(currentX, transform.position.y, currentZ += speed);
            //rigidBody.AddForce(0, 0, 10 * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void SetActive(bool active)
    {
        isActive = active;
        particleTrail.enableEmission = active;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Star>())
        {
            gameManager.AddScore(5);
        }
        else if (col.gameObject.GetComponent<Snail>())
        {
            speed = startingSpeed;
        }
    }

    public void StartBallMoving()
    {
        SetActive(true);
    }

    void ChangeDirection()
    {
        gameManager.AddScore(1);
        moveRight = !moveRight;
        speed += 0.0003f;

        //particleSpeed += 0.0003f;

        //rigidBody.velocity = Vector3.zero;
        //rigidBody.angularVelocity = Vector3.zero; 

        if (!moveRight)
        {
            particleTrail.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            particleTrail.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}