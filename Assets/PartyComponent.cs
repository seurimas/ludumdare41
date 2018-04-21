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
        int targetPosition = party.NumberLinePosition;
        if (targetPosition > transform.position.x)
        {
            transform.Translate(Time.deltaTime, 0, 0);
        } else if (targetPosition < transform.position.x)
        {
            transform.Translate(-Time.deltaTime, 0, 0);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(party.NumberLinePosition, 0), 1);
    }
}
