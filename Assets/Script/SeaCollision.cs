using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCollision : MonoBehaviour {
    public GameObject left;
    public GameObject right;
    public GameObject player;
    static string pre = "";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "paddle")
        {
            if(other.gameObject.name == "left")
            {
                if(pre.Equals("") || pre.Equals("right"))
                {
                    player.SendMessage("Move");

                    pre = other.gameObject.name;
                }
            }
            else if(other.gameObject.name == "right")
            {
                if(pre.Equals("") || pre.Equals("left"))
                {
                    other.gameObject.transform.parent.gameObject.SendMessage("Move");

                    pre = other.gameObject.name;
                }
            }
        }

        Debug.Log("tag: " + other.gameObject.tag + " name: " + other.gameObject.name);
    }

    private void OnTriggerEnter(Collision other)
    {
        Debug.Log("name : " + other.transform.name + "tag : " + other.transform.tag);

        if(other.transform.tag == "paddle")
        {
            if(other.transform.name == "left")
            {
                if (pre.Equals("right") || pre.Equals(""))
                {
                    // score & move
                    other.transform.SendMessage("Move");
                }
                pre = other.transform.name;
            }
            else if(other.transform.name =="right")
            {
                if(pre.Equals("left") || pre.Equals(""))
                {
                    other.transform.SendMessage("Move");
                }

                pre = other.transform.name;
            }

            Debug.Log("paddle pre : " + pre + "cur : " + other.transform.name);
        }
    }
}
