using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public float speed;
    Coroutine active;

	// Use this for initialization
	void Start () {
        StartCoroutine(Pendulum(4));
    }

    // Update is called once per frame
    void Update () {
    }

    public void Move(int targetX)
    {
        if (active != null)
        {
            StopCoroutine(active);
        }

        active = StartCoroutine(MoveCoroutine(targetX));
    }

    IEnumerator MoveCoroutine(int finalX)
    {
        while ((int)transform.position.x != finalX)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(finalX, transform.position.y), Time.deltaTime * speed);
            yield return 0;
        }

        active = null;
    }

    private IEnumerator Pendulum(float waitTime)
    {
        int destiny = -11;

        while (true)
        {
            destiny *= -1;
            Move(destiny);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
