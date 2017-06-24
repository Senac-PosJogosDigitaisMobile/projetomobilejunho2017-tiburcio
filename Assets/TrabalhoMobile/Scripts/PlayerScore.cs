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
    private int scoreMultiplier, scoreMaxMultiplier = 5;
    public SpriteRenderer shapeRenderer;
    private AudioSource audioSrc;
    public AudioClip pickupSound, changeShapeSound;

    public Color[] scoreMultiplierColor;

    public GameObject playerExplosion;
    public GameObject playerChangeShape;

    public Animator scoreAnimator;


    public int score;

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
            score += scoreValue * scoreMultiplier;
            UpdateScore();
            scoreMultiplierText.color = scoreMultiplierColor[scoreMultiplier - 1];
            scoreMultiplier++;
            if (scoreMultiplier == scoreMaxMultiplier)
            {
                scoreAnimator.SetTrigger("Score");
                scoreMultiplier = 1;
                playerController.changeShape();
                audioSrc.PlayOneShot(changeShapeSound);
                Instantiate(
                playerChangeShape,
                new Vector3(transform.position.x, 3f, -10f),
                transform.rotation);
            }
            else
            {
                scoreAnimator.SetTrigger("Score");
                audioSrc.pitch = (scoreMultiplier * 0.1f) + 1;
                audioSrc.PlayOneShot(pickupSound);
            }
        }
        else
        {
            gameController.setGameOver(true);
            playerController.ToggleControl(false);
            Destroy(gameObject);
            Instantiate(
                playerExplosion, 
                new Vector3(transform.position.x,3f,-10f),
                transform.rotation);
        }
    }


    void UpdateScore()
    {
        scoreMultiplierText.text = (scoreMultiplier * scoreValue).ToString();//scoreMultiplier.ToString()+"X";
        scoreText.text = "SCORE:\n" + score;
    }
}
