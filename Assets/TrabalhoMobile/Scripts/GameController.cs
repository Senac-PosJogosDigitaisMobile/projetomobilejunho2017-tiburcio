using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SpawnData
{
    public float spawnTimeIntervalMin, spawnTimeIntervalMax;
    public bool canChangeColor, isWhite;
    public Color[] shapeColor;
}

public class GameController : MonoBehaviour
{

    public Camera cam;
    public GameObject[] shapes;
    public int shapesPerWave = 10;
    public SpawnData[] spawnData;
    int spawnDataIndex;
    public int scoreIntervalToChangeSpawnIndex;
    

    public float maxSpeed = 2, startingSpeed = 0.4f;


    public GameObject gameOverText;
   // public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject startButton;
    public PlayerController playerController;
    public PlayerScore playerScore;


    private bool isGameOver = false;

    private float maxWidth;


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
    }



    public void StartGame()
    {
        spawnDataIndex = 0;
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
        gameOverText.SetActive(setBool);

    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.0f);

        while (!isGameOver)
        {
            changeSpawnIndex();
            for (int i = 0; i < shapesPerWave; i++)
            {
                InstantiateShape(
                    Random.Range(0, shapes.Length),
                    spawnData[spawnDataIndex].shapeColor[Random.Range(0, spawnData[spawnDataIndex].shapeColor.Length)],
                    startingSpeed
                    );
                yield return new WaitForSeconds(Random.Range(
                    spawnData[spawnDataIndex].spawnTimeIntervalMin,
                    spawnData[spawnDataIndex].spawnTimeIntervalMax));
            }
            if (startingSpeed < maxSpeed)
            {
                startingSpeed = startingSpeed + 0.05f;

            }
            yield return new WaitForSeconds(2f);
        }

    }



    void InstantiateShape(int shapeIndex, Color shapeColor, float gravityScale)
    {
        GameObject objShape = shapes[shapeIndex];
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-maxWidth, maxWidth),
            transform.position.y,
            0.0f
        );
        Quaternion spawnRotation = Quaternion.identity;
        GameObject obj = Instantiate(objShape, spawnPosition, spawnRotation);
        obj.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        if (spawnData[spawnDataIndex].canChangeColor)
            obj.GetComponent<SpriteRenderer>().color = shapeColor;
        if (spawnData[spawnDataIndex].isWhite)
            obj.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void changeSpawnIndex()
    {
        if (playerScore.score > scoreIntervalToChangeSpawnIndex)
        {
            scoreIntervalToChangeSpawnIndex = scoreIntervalToChangeSpawnIndex + playerScore.score;
            spawnDataIndex++;
            if (spawnDataIndex == spawnData.Length)
                spawnDataIndex--;
        }
        
    }


}
