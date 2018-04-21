using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party;
    public float visualNumberLinePosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int targetPosition = party.NumberLinePosition;
        if (targetPosition > visualNumberLinePosition)
        {
            visualNumberLinePosition = Mathf.Clamp(targetPosition + Time.deltaTime, visualNumberLinePosition, targetPosition);
        } else if (targetPosition < visualNumberLinePosition)
        {
            visualNumberLinePosition = Mathf.Clamp(targetPosition - Time.deltaTime, targetPosition, visualNumberLinePosition);
        }
	}
}
