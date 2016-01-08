using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    Ball ball;

	void Update () 
    {
        if (ball && ball.transform.position.y > 0)
            transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + 1.0f, ball.transform.position.z);
	}

    public void Reset()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }
}
