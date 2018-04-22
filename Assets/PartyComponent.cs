using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
    List<PartyMember> PartyMembers = new List<PartyMember>();
	// Use this for initialization
	void Start () {
        WorldPartyActions.Move += OnPartyMove;
        GetChildren();
    }

    void GetChildren()
    {
        foreach (Transform child in transform)
        {
            PartyMember member = child.GetComponent<PartyMember>();
            if (member != null)
            {
                PartyMembers.Add(member);
            }

        }
    }

    void OnPartyMove(object sender, EventArgs e)
    {
        Debug.Log("Number line position: " + party.NumberLinePosition);

    }
}
