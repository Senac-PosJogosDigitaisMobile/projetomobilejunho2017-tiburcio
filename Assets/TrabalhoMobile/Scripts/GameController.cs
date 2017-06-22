using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Color[] shapeColors;
    public float objectSpeed=5;

    private bool isGameOver = false;

    private float maxWidth;
    //private bool counting;

    // Use this for initialization
    void Start()
    {
        
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float playerWidth = shapes[0].GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - playerWidth;
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
        //if (transform.position.x>maxWidth||transform.position.x<-maxWidth)
        //{
        //    objectSpeed = -1 * objectSpeed;
        //}
        //transform.position = new Vector3(transform.position.x+(objectSpeed*Time.deltaTime),transform.position.y,transform.position.z);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        playerController.ToggleControl(true);
        StartCoroutine(Spawn());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void setGameOver(bool setBool)
    {
        isGameOver = setBool;
        Debug.Log(setBool);
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.0f);

        //counting = true;
        while (!isGameOver)
        {
            //Debug.Log(ReturnSpawnWave());
            switch (ReturnSpawnWave())
            {
                case "1":
                    InstantiateShape(Random.Range(0, shapes.Length), 0);
                    yield return new WaitForSeconds(Random.Range(1.4f,1.8f));
                    break;
                case "2":
                    InstantiateShape(Random.Range(0, shapes.Length), 0);
                    yield return new WaitForSeconds(Random.Range(1f, 1.4f));
                    break;
                case "3":
                    InstantiateShape(Random.Range(0, shapes.Length), 0);
                    yield return new WaitForSeconds(Random.Range(0.8f, 1f));
                    break;
                case "4":
                    InstantiateShape(Random.Range(0, shapes.Length), Random.Range(1, shapeColors.Length));
                    yield return new WaitForSeconds(Random.Range(0.4f,0.5f));
                    break;
                default:
                    break;
            }
           
        }
        playerController.ToggleControl(false);
        yield return new WaitForSeconds(0.5f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        restartButton.SetActive(true);
    }

   

    void InstantiateShape(int shapeIndex,int colorIndex)
    {
        GameObject objShape = shapes[shapeIndex];
        SpriteRenderer shapeRenderer = objShape.GetComponent<SpriteRenderer>();
        shapeRenderer.color = shapeColors[colorIndex];
        Vector3 spawnPosition = new Vector3(
            //transform.position.x,transform.position.y,transform.position.z
            transform.position.x + Random.Range(-maxWidth, maxWidth),
            transform.position.y,
            0.0f
        );
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(objShape, spawnPosition, spawnRotation);
    }

    string ReturnSpawnWave()
    {
        if (Time.timeSinceLevelLoad<15)
        {
            return "1";
        }
        else
        {
            if (Time.timeSinceLevelLoad < 30)
            {
                return "2";
            }
            else
            {
                if (Time.timeSinceLevelLoad < 45)
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
