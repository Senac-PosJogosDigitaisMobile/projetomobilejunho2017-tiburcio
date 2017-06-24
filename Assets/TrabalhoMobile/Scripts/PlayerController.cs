using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //main camera
    public Camera cam;

    //tamanho da tela
    private float maxWidth;
    //controle habilitado
    private bool canControl;

    //index do sprite atual do jogador
    private int shapeIndex;
    //array com sprites
    //0 circulo
    //1 quadrado
    //3 triangulo
    public Sprite[] shapes;
    public Sprite starShape;
    public float starPickupTime;
    bool hasStar = false;

    float playerAlpha = 1;

    //game object que tem o sprite renderer
    public GameObject shapeHolder;
    //sprite renderer obtido atraves do gameobject
    private SpriteRenderer shapeRenderer;

    public Animator bgAnimator;
    Animator playerAnimator;
    
    public float timeSmoothDamp;

    PlayerScore playerScore;
    public AudioSource bgMusicSrc,starMusicSrc;

    void Start()
    {
        //se não setar camera é ulitizada a main camera
        if (cam == null)
        {
            cam = Camera.main;
        }
        //pegar o sprite renderer
        shapeRenderer = shapeHolder.GetComponent<SpriteRenderer>();
        //setar o tamanho da tela
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float playerWidth = GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - playerWidth;
        //começar sem controle do jogador
        canControl = false;
        //começar com o circulo
        shapeIndex = 0;

        hasStar = false;

        playerAnimator = GetComponent<Animator>();
        playerScore = GetComponent<PlayerScore>();
    }
    

    void FixedUpdate()
    {
        bgAnimator.SetFloat("xPosition",transform.position.x);
        if (canControl)
        {
            playerAnimator.SetBool("Fire1", Input.GetButton("Fire1"));
            GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, playerAlpha);
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(rawPosition.x, 0f, 0.0f);
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            Vector3 velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().MovePosition(Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, timeSmoothDamp));
        }
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }

    public void changeShape()
    {
        hasStar = false;
        shapeIndex++;
        if (shapeIndex == shapes.Length)
        {
            shapeIndex = 0;
        }
        shapeRenderer.sprite = shapes[shapeIndex];
    }

    public void PickupStar()
    {
        bgMusicSrc.volume = 0f;
        starMusicSrc.Play();
        hasStar = true;

        playerScore.shapeChangeFlash();
        shapeRenderer.sprite = starShape;
        StartCoroutine(StarPowerTime());
    }

    IEnumerator StarPowerTime()
    {
        yield return new WaitForSeconds(starPickupTime);
        playerScore.shapeChangeFlash();

        starMusicSrc.Stop();
        bgMusicSrc.volume = 0.2f;
        changeShape();
    }


    public string ReturnShape()
    {
        if (hasStar)
            return "Star";
        if (shapeIndex == 0)
            return "Circulo";
        if (shapeIndex == 1)
            return "Quadrado";
        if (shapeIndex == 2)
            return "Triangulo";
        return null;
    }

    public void ToggleControl(bool toggle)
    {
        canControl = toggle;
    }
}
