using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public int targetX;
    public float speed;
    bool isMoving;

    private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveTowards(targetX));
    }

    // Update is called once per frame
    void Update () {
    }

    void MoveTo(int targetX)
    {
        if(Mathf.Abs(transform.position.x - targetX) > .2f)
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


    IEnumerator MoveTowards(int targetX)
    {
        isMoving = true;
        
        while (Mathf.Abs(transform.position.x - targetX) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.right * targetX, Time.deltaTime * speed);
            yield return 0;
        }

        isMoving = false;

        while (isMoving)
            yield return new WaitForSeconds(0.1f);
    }
}
