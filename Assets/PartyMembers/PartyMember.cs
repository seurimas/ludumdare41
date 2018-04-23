﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour, IRhythmListener {
    public Notes role;
    public Sprite[] idleFrames;
    public Sprite restSprite;
    public Sprite deadSprite;
    private int frameState = 0;
    private new PartyMemberAnimation animation;
    private PartyComponent party;
    int offset;
    public GameObject attackObject;
    public event EventHandler<PartyMemberEventArgs> NoteProcessed;

	// Use this for initialization
	void Start () {
        offset = (int)transform.localPosition.x;
        animation = GetComponent<PartyMemberAnimation>();
        party = GetComponentInParent<PartyComponent>();
        RhythmManager.instance.AddListener(this);
        GetComponent<SpriteRenderer>().sprite = idleFrames[0];
    }

    void Update()
    {
        if (IsDead())
        {
            GetComponent<SpriteRenderer>().sprite = deadSprite;
        }
    }

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
    }

    public bool IsDead()
    {
        return party.party.GetStatus(role).health <= 0;
    }

    public bool Damage(int amount)
    {
        if (IsDead())
        {
            return false;
        }
        animation.Hurt();
        party.party.Damage(role, amount);
        return true;
    }

    public void Attack(GameObject target)
    {
        if (IsDead())
        {
            return;
        }
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
        if(note == role)
        {
            Debug.Log("Pm says note correct");
            if (NoteProcessed != null)
                NoteProcessed.Invoke(this, new PartyMemberEventArgs(transform,role));
        }
        return false;
    }

    public void OnBeatRight(int beatNumber)
    {
        if (MyTurn(beatNumber))
        {
            if (party.party.attackTarget.HasValue)
            {
                GameObject target = party.spiralWorldManager.GetWorldGameObject(party.party.attackTarget.Value);
                if (target != null)
                {
                    Attack(target);
                    return;
                }
            }
        }
        frameState = (frameState + 1) % idleFrames.Length;
        if (!IsDead())
            GetComponent<SpriteRenderer>().sprite = idleFrames[frameState];
    }



    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {

    }

    bool MyTurn(int beatNumber)
    {
        if (IsDead())
            return false;
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

public class PartyMemberEventArgs : EventArgs
{
    public Transform transform;
    public Notes role;

    public PartyMemberEventArgs(Transform transform, Notes role)
    {
        this.transform = transform;
        this.role = role;
    }
}
