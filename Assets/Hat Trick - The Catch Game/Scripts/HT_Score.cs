using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HT_Score : MonoBehaviour
{

    public HT_HatController hatController;
    public Text scoreMultiplierText;
    public GUIText scoreText;
    public int ballValue;
    private int scoreMultiplier,scoreMaxMultiplier=11;

    private int score;

    void Start()
    {
        scoreMultiplier = 1;
        score = 0;
        UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(hatController.ReturnShape()))
        {
            score += ballValue*scoreMultiplier;
            scoreMultiplier++;
            if (scoreMultiplier == scoreMaxMultiplier)
            {
                scoreMultiplier = 1;
                hatController.changeShape();
            }
        }
        else
        {
            scoreMultiplier = 1;
            hatController.changeShape();
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
