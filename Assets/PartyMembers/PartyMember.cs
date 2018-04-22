using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour, IRhythmListener {
    public Notes role;
    private new PartyMemberAnimation animation;
    private PartyComponent party;
    int offset;
    public GameObject attackObject;

	// Use this for initialization
	void Start () {
        offset = (int)transform.localPosition.x;
        animation = GetComponent<PartyMemberAnimation>();
        party = GetComponentInParent<PartyComponent>();
        RhythmManager.instance.AddListener(this);
    }

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
    }

    public void Damage(int amount)
    {
        animation.Hurt();
        party.party.Damage(role, amount);
    }

    public void Attack(GameObject target)
    {
        animation.Attack();
        float distance = target.transform.position.x - transform.position.x;
        GameObject newItem = Instantiate(attackObject, party.transform.parent);
        newItem.GetComponent<ProjectileComponent>().friendly = true;
        newItem.transform.position = transform.position;
        Rigidbody2D rigidBody2D = newItem.GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = new Vector3(distance, 5);
    }


    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        return false;
    }

    public void OnBeatRight(int beatNumber)
    {
        if (party.party.attackTarget.HasValue)
        {
            if (MyTurn(beatNumber))
            {
                GameObject target = party.spiralWorldManager.GetWorldGameObject(party.party.attackTarget.Value);
                if (target != null)
                    Attack(target);
            }
        }
    }



    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {

    }

    bool MyTurn(int beatNumber)
    {
        switch (role)
        {
            case Notes.Bard:
                return beatNumber % 4 == 0;
            case Notes.Cleric:
                return beatNumber % 4 == 1;
            case Notes.Rogue:
                return beatNumber % 4 == 2;
            case Notes.Fighter:
                return beatNumber % 4 == 3;
        }
        return false;
    }
}
