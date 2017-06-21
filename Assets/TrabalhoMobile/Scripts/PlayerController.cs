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
    //game object que tem o sprite renderer
    public GameObject shapeHolder;
    //sprite renderer obtido atraves do gameobject
    private SpriteRenderer shapeRenderer;

   
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

    }

    // Update is called once per physics timestep
    void FixedUpdate()
    {
        if (canControl)
        {
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f);
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        }
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }

    public void changeShape()
    {
        shapeIndex++;
        if (shapeIndex == shapes.Length)
        {
            shapeIndex = 0;
        }
        shapeRenderer.sprite = shapes[shapeIndex];
    }

    public string ReturnShape()
    {
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
