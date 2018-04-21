using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public int targetX;
    public float speed;
    int threshold = 1;
    bool onTarget = false;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        MoveTo(targetX);
    }

    void MoveTo(int targetX)
    {
        if (Mathf.Abs(targetX - transform.position.x) > threshold)
        {
            if (targetX > transform.position.x)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            }
            else if (targetX < transform.position.x)
            {
                transform.Translate(-Time.deltaTime * speed, 0, 0);
            }

            Debug.Log("I'm not on target");

        }
        else
        {
            Debug.Log("I'm on target");
        }


    }
}
