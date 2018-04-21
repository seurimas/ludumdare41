using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        MoveTo(party.NumberLinePosition);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(party.NumberLinePosition, transform.position.y), 1);
    }

    void MoveTo(int targetPosition)
    {
        if (targetPosition > transform.position.x)
        {
            transform.Translate(Time.deltaTime, 0, 0);
        }
        else if (targetPosition < transform.position.x)
        {
            transform.Translate(-Time.deltaTime, 0, 0);
        }
    }
}
