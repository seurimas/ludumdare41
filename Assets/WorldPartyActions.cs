using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPartyActions : MonoBehaviour {
    private SpiralWorldManager world;
    private PartyComponent party;
    public static event EventHandler<EventArgs> Attack;
    public static event EventHandler<EventArgs> Forward;
    public static event EventHandler<EventArgs> Backward;
    // Use this for initialization
    void Start ()
    {
        party = GetComponentInChildren<PartyComponent>();
        world = GetComponent<SpiralWorldManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool CanPartyEnter(int position)
    {
        if (position < 0 || position >= WorldRenderer.forestSize / WorldItem.WorldScale)
            return false;
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
        {
            party.party.NumberLinePosition++;
        }
        party.party.retreating = false;
        TriggerForward();
    }
    public void RetreatParty()
    {
        int currentPosition = party.party.NumberLinePosition;
        if (CanPartyEnter(currentPosition - 1))
        {
            party.party.NumberLinePosition--;
        }
        party.party.retreating = true;
        TriggerBackward();
    }
    public void PartyAttack()
    {
        int currentPosition = party.party.NumberLinePosition;
        foreach (WorldItem item in world.world.GetItemsAt(currentPosition - 1, currentPosition + 1))
        {
            if (item.IsAttackable())
            {
                party.party.Attack(item);
                return;
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
                party.party.Harvest(item);
                return;
            }
        }
    }
    public void PartyRest()
    {
        party.party.Rest();
    }
    public void PartyCraft()
    {
        party.party.Craft();
    }

    void TriggerPartyAttack()
    {
        if (Attack != null)
            Attack.Invoke(this, new EventArgs());
    }

    void TriggerForward()
    {
        if (Forward != null)
            Forward.Invoke(this, new EventArgs());
    }

    void TriggerBackward()
    {
        if (Backward != null)
            Backward.Invoke(this, new EventArgs());
    }
}
