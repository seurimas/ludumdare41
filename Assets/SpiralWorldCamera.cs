using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldCamera : MonoBehaviour {
    public PartyComponent party;
    public Vector3 position0;
    public float movementSpeed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (party.party.PositionX > 0 && party.party.PositionX < 120)
        {
            transform.Translate(SpiralWorldManager.TranslationToTarget(transform.position.x - position0.x, party.party.NumberLinePosition, movementSpeed * Time.deltaTime));
        }
	}
}
