using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPartyActions : MonoBehaviour {
    private SpiralWorldManager world;
    private PartyComponent party;
    public static event EventHandler<EventArgs> Attack;
    public static event EventHandler<EventArgs> Move;
    // Use this for initialization
    void Start ()
    {
        party = GetComponentInChildren<PartyComponent>();
        world = GetComponent<SpiralWorldManager>();
        world.world.AddItem(party.party);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool CanPartyEnter(int position)
    {
        List<WorldItem> items = world.world.GetItemsAt(position);
        foreach (WorldItem item in items)
        {
            if (item.BlocksParty())
            {
                return false;
            }
        }
        return true;
    }

    public void AdvanceParty() {
        int currentPosition = party.party.NumberLinePosition;
        if (CanPartyEnter(currentPosition + 1))
            party.party.NumberLinePosition++;
        TriggerPartyMove();
    }
    public void RetreatParty()
    {
        int currentPosition = party.party.NumberLinePosition;
        if (CanPartyEnter(currentPosition - 1))
            party.party.NumberLinePosition--;
        TriggerPartyMove();
    }
    public void PartyAttack()
    {
        int currentPosition = party.party.NumberLinePosition;
        foreach (WorldItem item in world.world.GetItemsAt(currentPosition - 1, currentPosition + 1))
        {
            if (item.IsAttackable())
            {
                party.party.Attack(item);
            }
        }

        TriggerPartyAttack();
    }
    public void PartyHarvest()
    {
        int currentPosition = party.party.NumberLinePosition;
        foreach (WorldItem item in world.world.GetItemsAt(currentPosition - 1, currentPosition + 1))
        {
            if (item.IsHarvestable())
            {
                Debug.Log(item);
                party.party.Harvest(item);
            }
        }
    }

    void TriggerPartyAttack()
    {
        if (Attack != null)
            Attack.Invoke(this, new EventArgs());
    }

    void TriggerPartyMove()
    {
        if (Move != null)
            Move.Invoke(this, new EventArgs());
    }
}
