using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
    public List<PartyMember> PartyMembers = new List<PartyMember>();
    public SpiralWorldManager spiralWorldManager;
    public GameObject weaponPrefab;
    // Use this for initialization
    void Start () {
        spiralWorldManager = GetComponentInParent<SpiralWorldManager>();
        WorldPartyActions.Forward += OnPartyForward;
        WorldPartyActions.Backward += OnPartyBackward;
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
            if (member.name == "Fighter")
            {
                member.role = Notes.Fighter;
            } else if (member.name == "Cleric")
            {
                member.role = Notes.Cleric;
            } else if (member.name == "Bard")
            {
                member.role = Notes.Bard;
            } else if (member.name == "Rogue")
            {
                member.role = Notes.Rogue;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(party.PositionX, 0), 1);
    }

    void OnPartyForward(object sender, EventArgs e)
    {
        Debug.Log("Forward");
        foreach(PartyMember pm in PartyMembers)
        {
            // pm.Forward();
        }
    }

    void OnPartyBackward(object sender, EventArgs e)
    {
        Debug.Log("Backward");
        foreach (PartyMember pm in PartyMembers)
        {
            // pm.Backward();
        }
    }
}
