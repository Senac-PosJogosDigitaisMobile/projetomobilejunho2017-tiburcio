using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public GameController gameController;
    public PlayerController playerController;
    public Text scoreMultiplierText;
    public Text scoreText;
    public int scoreValue;
    public GameObject GameOverUI;
    private int scoreMultiplier, scoreMaxMultiplier = 6;
    public SpriteRenderer shapeRenderer;
    private AudioSource audioSrc;
    public AudioClip pickupSound;

    public Animator scoreAnimator;


    private int score;

    void Start()
    {
        scoreMultiplier = 1;
        score = 0;
        audioSrc = GetComponent<AudioSource>();
        UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerController.ReturnShape()))
        {
            scoreAnimator.SetTrigger("Score");
            //Debug.Log(scoreMultiplier);
            audioSrc.pitch = (scoreMultiplier*0.1f)+1;
            audioSrc.PlayOneShot(pickupSound);
            //shapeRenderer.color = new Color(1f,1f,scoreMultiplier*0.1f,1f);
            //Debug.Log(shapeRenderer.color);
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
            gameController.setGameOver(true);
            //scoreMultiplier = 1;
            //playerController.changeShape();
            //GameOverUI.SetActive(true);
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
