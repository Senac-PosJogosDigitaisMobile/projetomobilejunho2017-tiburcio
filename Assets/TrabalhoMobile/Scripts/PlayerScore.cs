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
    public int starMultiplier;
    public SpriteRenderer shapeRenderer;
    private AudioSource audioSrc;
    public AudioClip pickupSound, changeShapeSound;

    public Color[] scoreMultiplierColor;

    public GameObject playerExplosion;
    public GameObject playerChangeShape;

    public Animator scoreAnimator;


    public Text currentShapeCounter;

    public int score;

    void Start()
    {
        scoreMultiplier = 1;
        score = 0;
        audioSrc = GetComponent<AudioSource>();
        UpdateScore();
        currentShapeCounter.text = scoreMultiplier.ToString() + "x";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            playerController.PickupStar();
            return;
        }
        if (playerController.ReturnShape()=="Star")
        {
            score += scoreValue * starMultiplier;
            UpdateScore();
            scoreMultiplierText.color = scoreMultiplierColor[scoreMultiplierColor.Length-1];
            scoreMultiplierText.text = (starMultiplier*scoreValue).ToString();
            scoreAnimator.SetTrigger("Score");
            audioSrc.pitch = (starMultiplier * 0.1f) + 1;
            audioSrc.PlayOneShot(pickupSound);
            //shapeChangeFlash();
            return;
        }
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
                shapeChangeFlash();
            }
            else
            {
                scoreAnimator.SetTrigger("Score");
                audioSrc.pitch = (scoreMultiplier * 0.1f) + 1;
                audioSrc.PlayOneShot(pickupSound);
            }

            currentShapeCounter.text = scoreMultiplier.ToString()+"x";
        }
        else
        {
            gameController.setGameOver(true);
            playerController.ToggleControl(false);
            Destroy(gameObject);
            Instantiate(
                playerExplosion,
                new Vector3(transform.position.x, 3f, -10f),
                transform.rotation);
        }
    }

    public void shapeChangeFlash()
    {
        Instantiate(
                playerChangeShape,
                new Vector3(transform.position.x, 3f, -10f),
                transform.rotation);
    }

    void UpdateScore()
    {
        scoreMultiplierText.text = (scoreMultiplier * scoreValue).ToString();//scoreMultiplier.ToString()+"X";
        scoreText.text = "SCORE:\n" + score;
    }
}
