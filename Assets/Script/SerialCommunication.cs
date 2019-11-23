using UnityEngine;
using System;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

enum STATUS
{
    STAND_BY,
    CONNECT,
    ERROR,
    DISCONNECT
};

public class SerialCommunication : MonoBehaviour
{
    private SerialManager serialManger;

    private static Vector3 temp_rotation;
    private static Vector3 adjust_rotation = Vector3.zero;
    private static bool button_left, button_ok, button_right;

    private static STATUS status = STATUS.STAND_BY;

    public static Vector3 RoadRotation
    {
        get
        {
            return (temp_rotation + adjust_rotation);
        }
    }
    public static bool LeftButton
    {
        get
        {
            return button_left;
        }
    }
    public static bool OkButton
    {
        get
        {
            return button_ok;
        }
    }
    public static bool RightButton
    {
        get
        {
            return button_right;
        }
    }
    public static string CURRENT_STATUS
    {
        get
        {
            switch(status)
            {
                case STATUS.CONNECT:
                    return "CONNECT";
                case STATUS.DISCONNECT:
                    return "DISCONNECT";
                case STATUS.ERROR:
                    return "ERROR";
                case STATUS.STAND_BY:
                    return "STAND-BY";
            }

            return "???";
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        serialManger = new SerialManager();
        serialManger.SetSerialPort("COM8");
        serialManger.SetReadTimeout(1000);
        serialManger.SetWriteTimeout(1000);
        serialManger.SetSerialOpen();

        status = STATUS.CONNECT;
    }

    void Update()
    {

        try
        {
            temp_rotation = serialManger.GetAngValue();
            temp_rotation = new Vector3(-1*temp_rotation.z, temp_rotation.y, temp_rotation.x);
            //temp_rotation.z *= -1;

            //temp_position = mySerialManager.GetPosValue();
            //temp_position.x += gun.transform.position.x;
            //temp_position.y += gun.transform.position.y;
            //temp_position.z += gun.transform.position.z;
            //camera_position.x = temp_position.x - 2;
            //camera_position.y = temp_position.y - 1;
            //camera_position.z = temp_position.z - 3;

            // UnityEngine.Debug.Log(temp_rotation);


            //bullets.transform.rotation = Quaternion.Euler(temp_rotation);
            //this.transform.position = temp_position;

            //camera.transform.rotation = Quaternion.Euler(camera_rotation);
            //camera.transform.position = camera_position;

        } catch ( System.Exception )
        {
            //UnityEngine.Debug.Log("null exception:serial manage is null");
            status = STATUS.ERROR;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            serialManger.StopSerialThread();
            Application.Quit();
            status = STATUS.DISCONNECT;
        }

    }

    public void ReTryConnect()
    {
        serialManger.SetSerialOpen();
    }

    public void AdjustController()
    {
        adjust_rotation += (adjust_rotation + temp_rotation) * -1;
    }
}