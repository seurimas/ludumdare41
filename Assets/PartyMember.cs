using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public int targetX;
    public float speed;
    public bool Move;
    int auxTarget;

    private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
        coroutine = WaitAndPrint(5.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update () {
        MoveTo(auxTarget);
    }

    void MoveTo(int targetX)
    {
        if(Mathf.Abs(transform.position.x - targetX) > .2f && Move)
        {
            if (targetX > transform.position.x)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            }
            else if (targetX < transform.position.x)
            {
                transform.Translate(-Time.deltaTime * speed, 0, 0);
            }
        }
    }


    IEnumerator MoveTowardsTarget()
    {        
        while (Mathf.Abs(transform.position.x - targetX) > .1f && Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.right * targetX, Time.deltaTime * speed);
            yield return 0;
        }

    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        int[] xPositions = new int[2];
        xPositions[0] = (int)transform.parent.position.x;
        xPositions[1] = targetX;

        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log(auxTarget);
            auxTarget = xPositions[(int)Time.time % 2];
        }
    }
}
