using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyComponent : MonoBehaviour {
    public Party party = new Party();
    public List<PartyMember> PartyMembers = new List<PartyMember>();
    private Dictionary<Notes, GameObject> members = new Dictionary<Notes, GameObject>();
    public SpiralWorldManager spiralWorldManager;
    public GameObject weaponPrefab;
    // Use this for initialization
    void Start () {
        spiralWorldManager = GetComponentInParent<SpiralWorldManager>();
        WorldPartyActions.Forward += OnPartyForward;
        WorldPartyActions.Backward += OnPartyBackward;
        GetChildren();
        StartCoroutine(party.BeHungry(5));
    }

    void Update()
    {
        bool alive = false;
        foreach (PartyMemberStatus status in party.partyStatus.Values)
        {
            alive |= status.health > 0;
        }
        if (!alive)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (party.retreating)
        {
            members[Notes.Fighter].GetComponent<PartyMemberAnimation>().offset = 0;
            members[Notes.Rogue].GetComponent<PartyMemberAnimation>().offset = 1;
            members[Notes.Cleric].GetComponent<PartyMemberAnimation>().offset = 2;
            members[Notes.Bard].GetComponent<PartyMemberAnimation>().offset = 3;
            members[Notes.Fighter].transform.localScale = new Vector3(-1, 1, 1);
            members[Notes.Rogue].transform.localScale = new Vector3(-1, 1, 1);
            members[Notes.Cleric].transform.localScale = new Vector3(-1, 1, 1);
            members[Notes.Bard].transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            members[Notes.Fighter].GetComponent<PartyMemberAnimation>().offset = 3;
            members[Notes.Rogue].GetComponent<PartyMemberAnimation>().offset = 2;
            members[Notes.Cleric].GetComponent<PartyMemberAnimation>().offset = 1;
            members[Notes.Bard].GetComponent<PartyMemberAnimation>().offset = 0;
            members[Notes.Fighter].transform.localScale = new Vector3(1, 1, 1);
            members[Notes.Rogue].transform.localScale = new Vector3(1, 1, 1);
            members[Notes.Cleric].transform.localScale = new Vector3(1, 1, 1);
            members[Notes.Bard].transform.localScale = new Vector3(1, 1, 1);
        }
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
            members.Add(member.role, member.gameObject);
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
