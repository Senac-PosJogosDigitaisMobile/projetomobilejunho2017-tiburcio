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
    public int shapesPerWave;
}

public class GameController : MonoBehaviour
{

    public Camera cam;
    public GameObject[] shapes;
    public GameObject starShape;
    //public int shapesPerWave = 10;
    public SpawnData[] spawnData;
    int spawnDataIndex;
    public int scoreIntervalToChangeSpawnIndex;

    public float nextStarDrop, stardropMinInterval, stardropMaxInterval;

    public float maxSpeed = 2, startingSpeed = 0.4f;


    public GameObject gameOverText;
    // public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject splashTittle;
    public GameObject startButton;
    public GameObject scoreTxt;
    public GameObject splashScreenInfoBtns;
    public GameObject shapeInfo;
    public PlayerController playerController;
    public PlayerScore playerScore;


    private bool isGameOver = false;

    private float maxWidth;

    public AudioSource audioSrc;
    public AudioClip selectSound;

    public bool hasSound;

    void Start()
    {
        if (AudioListener.volume==0)
        {
            hasSound = false;
        }
        else
        {
            hasSound = true;
        }
        audioSrc = GetComponent<AudioSource>();
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
        nextStarDrop = Random.Range(stardropMinInterval, stardropMaxInterval);
        audioSrc.PlayOneShot(selectSound);
        spawnDataIndex = 0;
        isGameOver = false;
        splashScreen.SetActive(false);
        scoreTxt.SetActive(true);
        shapeInfo.SetActive(true);
        splashScreenInfoBtns.SetActive(false);
        startButton.SetActive(false);
        playerController.ToggleControl(true);
        StartCoroutine(Spawn());
    }

    public void RestartGame()
    {

        audioSrc.PlayOneShot(selectSound);
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
        int spawnCount = 0;
        while (!isGameOver)
        {
            float spawnDropIndex = Random.Range(1, spawnData[spawnDataIndex].shapesPerWave);
            changeSpawnIndex();
            for (int i = 0; i < spawnData[spawnDataIndex].shapesPerWave; i++)
            {
                //Debug.Log(i);
                //Debug.Log(spawnDropIndex);
                if (spawnCount > nextStarDrop && spawnDropIndex == i)
                {

                    nextStarDrop = spawnCount + Random.Range(stardropMinInterval, stardropMaxInterval);
                    Vector3 spawnPosition = new Vector3(
                        transform.position.x + Random.Range(-maxWidth, maxWidth),
                        transform.position.y,
                        0.0f
                    );
                    Quaternion spawnRotation = Quaternion.identity; Instantiate(starShape, spawnPosition, spawnRotation);
                }
                else
                {
                    InstantiateShape(
                        Random.Range(0, shapes.Length),
                        spawnData[spawnDataIndex].shapeColor[Random.Range(0, spawnData[spawnDataIndex].shapeColor.Length)],
                        startingSpeed
                        );
                }
                yield return new WaitForSeconds(Random.Range(
                    spawnData[spawnDataIndex].spawnTimeIntervalMin,
                    spawnData[spawnDataIndex].spawnTimeIntervalMax));
            }
            if (startingSpeed < maxSpeed)
            {
                startingSpeed = startingSpeed + 0.02f;
            }
            yield return new WaitForSeconds(1f);
            spawnCount++;
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

    public void toggleGlobalVolume()
    {
        hasSound = !hasSound;
        if (hasSound)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }

    public void HideSplash()
    {
        splashTittle.SetActive(!splashTittle.activeSelf);
        startButton.SetActive(!startButton.activeSelf);
    }

}
