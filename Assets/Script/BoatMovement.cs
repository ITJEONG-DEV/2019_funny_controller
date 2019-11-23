using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {
    private float time = 0.0f;
    private float height = 0.2f;
    private bool isUp = false;

	void Start () {
		
	}
	
	void Update () {
        time += Time.deltaTime;

        if (isUp)
        {
            transform.Translate(new Vector3(0, 1, 0) * height * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(0, -1, 0) * height * Time.deltaTime);
        }

        if (time>=0.5f)
        {

            isUp = !isUp;
            time = 0.0f;
        }
	}
}
