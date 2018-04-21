using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public int targetX;
    public float speed;
    int threshold = 1;
    bool onTarget = false;
    public bool interrupt = false;
    public bool routineActive = false;
    string activeCoroutine;

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveToX(2));
    }

    // Update is called once per frame
    void Update () {

    }

    void Interrupt()
    {
        interrupt = true;
        while (routineActive)
        interrupt = false;
    }


    IEnumerator MoveToX(int targetX)
    {
        if (routineActive)
        {
            Interrupt();
        }
        else
        {
            while (!interrupt && (int)transform.position.x != targetX)
            {
                routineActive = true;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y), Time.deltaTime * speed);
                yield return 0;
            }
        }

        routineActive = false;
    }
}
