using UnityEngine;
using System.Collections;

public class HT_HatController : MonoBehaviour {

	public Camera cam;

	private float maxWidth;
	private bool canControl;

    private int shapeIndex;
    public Sprite[] shapes;
    //0 circulo
    //1 quadrado
    //3 triangulo
    public GameObject shapeHolder;
    private SpriteRenderer shapeRenderer;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
        shapeRenderer = shapeHolder.GetComponent<SpriteRenderer>();
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
        //Debug.Log(hatWidth);
        maxWidth = targetWidth.x - hatWidth;
		canControl = false;

        shapeIndex = 0;

    }
	
	// Update is called once per physics timestep
	void FixedUpdate () {
		if (canControl) {
			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
			Vector3 targetPosition = new Vector3 (rawPosition.x, 0.0f, 0.0f);
			float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
			targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
			GetComponent<Rigidbody2D>().MovePosition (targetPosition);
		}
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
	}

    public void changeShape()
    {
        shapeIndex++;
        if (shapeIndex==shapes.Length)
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

	public void ToggleControl (bool toggle) {
		canControl = toggle;
	}
}
