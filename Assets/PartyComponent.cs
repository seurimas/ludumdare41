using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
    public PartyMember Nick;
	// Use this for initialization
	void Start () {
        WorldPartyActions.Move += OnPartyMove;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnPartyMove(object sender, EventArgs e)
    {
        Debug.Log("Number line position: " + party.NumberLinePosition);
        Nick.Move(party.NumberLinePosition);
    }
}
