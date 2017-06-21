using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float secondsToWait = 2;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitToDestroy());
	}

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(secondsToWait);
    }
	
}
