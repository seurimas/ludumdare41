using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
    List<PartyMember> PartyMembers = new List<PartyMember>();
	// Use this for initialization
	void Start () {
        WorldPartyActions.Forward += OnPartyForward;
        WorldPartyActions.Backward += OnPartyBackward;
        WorldPartyActions.Attack += OnPartyAttack;
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

    void OnPartyAttack(object sender, EventArgs e)
    {
        foreach (PartyMember pm in PartyMembers)
        {
            pm.Attack();
        }
    }

    void OnPartyForward(object sender, EventArgs e)
    {
        Debug.Log("Forward");
        foreach(PartyMember pm in PartyMembers)
        {
            pm.Forward();
        }
    }

    void OnPartyBackward(object sender, EventArgs e)
    {
        Debug.Log("Backward");
        foreach (PartyMember pm in PartyMembers)
        {
            pm.Backward();
        }
    }
}
