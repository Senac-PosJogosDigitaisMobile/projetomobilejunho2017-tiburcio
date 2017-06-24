using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

    public GameObject scoreFlash;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate(scoreFlash, other.gameObject.transform);
        Destroy(other.gameObject);
    }
}
