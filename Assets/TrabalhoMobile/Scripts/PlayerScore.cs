using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public PlayerController playerController;
    public Text scoreMultiplierText;
    public GUIText scoreText;
    public int scoreValue;
    public GameObject GameOverUI;
    private int scoreMultiplier, scoreMaxMultiplier = 11;

    private int score;

    void Start()
    {
        scoreMultiplier = 1;
        score = 0;
        UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerController.ReturnShape()))
        {
            score += scoreValue * scoreMultiplier;
            scoreMultiplier++;
            if (scoreMultiplier == scoreMaxMultiplier)
            {
                scoreMultiplier = 1;
                playerController.changeShape();
            }
        }
        else
        {
            //scoreMultiplier = 1;
            //playerController.changeShape();
            GameOverUI.SetActive(true);
        }
        UpdateScore();
    }

    //void OnCollisionEnter2D (Collision2D collision) {
    //	if (collision.gameObject.tag == "Bomb") {
    //		score -= ballValue * 2;
    //		UpdateScore ();
    //	}
    //}

    void UpdateScore()
    {
        scoreMultiplierText.text = scoreMultiplier.ToString();
        scoreText.text = "SCORE:\n" + score;
    }
}
