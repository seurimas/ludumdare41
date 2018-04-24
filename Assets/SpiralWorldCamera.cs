using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldCamera : MonoBehaviour {
    public PartyComponent party;
    public Vector3 position0;
    public Vector3 position1;
    public float movementSpeed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetOffset = party.party.retreating ? position1 : position0;
        transform.Translate(SpiralWorldManager.TranslationToTarget(transform.position.x - targetOffset.x, party.party.NumberLinePosition, movementSpeed * Time.deltaTime));
	}
}
