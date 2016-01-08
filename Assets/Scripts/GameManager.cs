using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Canvas startCanvas;
    public Canvas gameCanvas;
    public Canvas endCanvas;
    public Canvas optionsCanvas;
    public Text scoreText;
    public Text highScoreText;
    public Text endScoreText;
    public Text endHighScoreText;
    public GameObject ballPrefab;

    Ball ball;
    FloorManager floor;
    CameraFollow cameraFollow;
    static float aspect;    
    int score = 0;
    int highScore = 0;

    void Start () 
    {
        cameraFollow = GameObject.FindObjectOfType<CameraFollow>();
        floor = GameObject.FindObjectOfType<FloorManager>();

        aspect = Mathf.Round(Camera.main.aspect * 100f) / 100f;
        if (aspect >= 1) aspect = 1;

        ShowStartMenu();
	}
	
	void Update () 
    {
        scoreText.text = score.ToString();
	}

    public void AddScore(int points)
    {
        score += points;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public static float GetScreenAspect()
    {
        return aspect;
    }

    public void ShowStartMenu()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        startCanvas.GetComponent<Canvas>().enabled = true;
        gameCanvas.GetComponent<Canvas>().enabled = false;
        endCanvas.GetComponent<Canvas>().enabled = false;

        floor.BuildInitialFloors();
        ball = GameObject.Instantiate(ballPrefab).GetComponent<Ball>();
        cameraFollow.Reset();
    }

    public void StartGame()
    {
        ResetScore();
        startCanvas.GetComponent<Canvas>().enabled = false;
        gameCanvas.GetComponent<Canvas>().enabled = true;
        endCanvas.GetComponent<Canvas>().enabled = false;
        ball.StartBallMoving();
    }

    public void GameOver()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            GameObject.Find("NewHighScore").GetComponent<Text>().enabled = true;
        }
        else
        {
            GameObject.Find("NewHighScore").GetComponent<Text>().enabled = false;
        }
        
        startCanvas.GetComponent<Canvas>().enabled = false;
        gameCanvas.GetComponent<Canvas>().enabled = false;
        endCanvas.GetComponent<Canvas>().enabled = true;

        endHighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        endScoreText.text = score.ToString();
    }
}