using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
    public int targetX;
	// Use this for initialization
	void Start () {
        StartCoroutine(Pendulum(3));
    }
	
	// Update is called once per frame
	void Update () {
        MoveTo(targetX);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(transform.position.x + 2, transform.position.y), 1);
    }

    void MoveTo(int targetX)
    {
        if (Mathf.Abs(transform.position.x - targetX) > .2f)
        {
            if (targetX > transform.position.x)
            {
                transform.Translate(Time.deltaTime, 0, 0);
            }
            else if (targetX < transform.position.x)
            {
                transform.Translate(-Time.deltaTime, 0, 0);
            }
        }
    }


    private IEnumerator Pendulum(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            targetX *= -1;
        }
    }
}
