using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCollision : MonoBehaviour {
    static string pre = "";

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "paddle")
        {
            if(collision.collider.name == "left")
            {
                if(pre.Equals("right"))
                {
                    // score & move
                    collision.collider.SendMessage("Move");
                }

                pre = collision.collider.name;
            }
            else if(collision.collider.name =="right")
            {
                if(pre.Equals("left"))
                {
                    collision.collider.SendMessage("Move");
                }

                pre = collision.collider.name;
            }
        }
    }
}
