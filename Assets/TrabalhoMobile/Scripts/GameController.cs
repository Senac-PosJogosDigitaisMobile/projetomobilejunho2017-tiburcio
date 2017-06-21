using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera cam;
    public GameObject[] shapes;
    //public float timeLeft;
    //public GUIText timerText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject startButton;
    public PlayerController playerController;
    

    private float maxWidth;
    private bool counting;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = shapes[0].GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;
        //timerText.text = "TIME:\n" + Mathf.RoundToInt(timeLeft);
    }

    void FixedUpdate()
    {
        //if (counting)
        //{
        //    timeLeft -= Time.deltaTime;
        //    if (timeLeft < 0)
        //    {
        //        timeLeft = 0;
        //    }
        //    timerText.text = "TIME:\n" + Mathf.RoundToInt(timeLeft);
        //}
    }

    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        playerController.ToggleControl(true);
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.0f);
        counting = true;
        while (!gameOverText.activeSelf)
        {
            GameObject objShapes = shapes[Random.Range(0, shapes.Length)];
            Vector3 spawnPosition = new Vector3(
                transform.position.x + Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                0.0f
            );
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(objShapes, spawnPosition, spawnRotation);
            Debug.Log(ReturnSpawnWave());
            switch (ReturnSpawnWave())
            {
                case "1":
                    yield return new WaitForSeconds(Random.Range(1.8f,2f));
                    break;
                case "2":
                    yield return new WaitForSeconds(Random.Range(1.2f, 1.4f));
                    break;
                case "3":
                    yield return new WaitForSeconds(Random.Range(0.6f, 1f));
                    break;
                case "4":
                    yield return new WaitForSeconds(Random.Range(0.2f,0.4f));
                    break;
                default:
                    break;
            }
           
        }
        //yield return new WaitForSeconds(2.0f);
        //gameOverText.SetActive(true);
        playerController.ToggleControl(false);
        yield return new WaitForSeconds(1.0f);
        restartButton.SetActive(true);
    }

    string ReturnSpawnWave()
    {
        if (Time.time<10)
        {
            return "1";
        }
        else
        {
            if (Time.time<20)
            {
                return "2";
            }
            else
            {
                if (Time.time < 30)
                {
                    return "3";
                }
                else
                {
                    return "4";
                }
            }
        }
    }
}
