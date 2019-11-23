using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject controller;
    public Text statusText;

    private int select;
    private int[] x;
    private Vector3 initRotation;
    private Vector3 controllerRotation;

	void Start () {
        x = new int[2] {-299, 54 };

        select = 0;
        ShowArrow();

        initRotation = controller.transform.rotation.eulerAngles;
	}
	
	void Update () {

        try
        {
            controllerRotation = SerialCommunication.RoadRotation;

            controller.transform.rotation = Quaternion.Euler(initRotation + controllerRotation);

            statusText.text = SerialCommunication.CURRENT_STATUS;

            if (SerialCommunication.LeftButton)
            {
                select = 0;
                ShowArrow();
            }
            else if (SerialCommunication.RightButton)
            {
                select = 1;
                ShowArrow();
            }
            else if (SerialCommunication.OkButton)
            {
                MoveScene();
            }

        }catch(Exception exception)
        {
            Debug.Log("disconnected? " + exception.Message);
        }

    }

    void ShowArrow()
    {
        if(select==0)
        {
            arrow1.SetActive(true);
            arrow2.SetActive(false);
        }
        else if(select==1)
        {
            arrow1.SetActive(false);
            arrow2.SetActive(true);
        }
    }

    void MoveScene()
    {
        if(select==0)
        {
            SceneManager.LoadScene("Pipe-Running");
        }
        else if(select==1)
        {
            SceneManager.LoadScene("Row-Row-Row-YOUR-BOAT");
        }
    }
}
