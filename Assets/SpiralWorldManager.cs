using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldManager : MonoBehaviour {
    private SpiralWorld world = new SpiralWorld();
    public PartyComponent party;
	// Use this for initialization
	void Start () {
        world.AddItem(party.party);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool CanPartyEnter(int position)
    {
        List<WorldItem> items = world.GetItemsAt(position);
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
        int currentPosition = party.party.GetPosition();
        if (CanPartyEnter(currentPosition + 1))
            party.party.NumberLinePosition++;
    }
    public void RetreatParty()
    {
        int currentPosition = party.party.GetPosition();
        if (CanPartyEnter(currentPosition - 1))
            party.party.NumberLinePosition--;
    }
    public void PartyAttack()
    {
        int currentPosition = party.party.GetPosition();
        foreach (WorldItem item in world.GetItemsAt(currentPosition - 1, currentPosition + 1))
        {
            if (item.IsAttackable())
            {
                party.party.Attack(item);
            }
        }
    }
    public void PartyHarvest()
    {
        int currentPosition = party.party.GetPosition();
        foreach (WorldItem item in world.GetItemsAt(currentPosition - 1, currentPosition + 1))
        {
            if (item.IsHarvestable())
            {
                party.party.Harvest(item);
            }
        }
    }
}
