using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Loot
{
    HONEY,
    SAFRON,
    JUICE,
    RUBY,
    SAPPHIRE,
    GARNET,
    HONEY_WEAPON,
    SAFRON_WEAPON,
    JUICE_WEAPON,
    RUBY_WEAPON,
    SAPPHIRE_WEAPON,
    GARNET_WEAPON,
    WOOD,
    GOLD,
}

public class PartyMemberStatus
{
    public int maxHealth = 10;
    public int health = 10;
    public int maxHunger = 100;
    public int hunger = 100;
    public bool resting = false;
}

public class Party : WorldItem
{
    public int? attackTarget;
    public int? harvestTarget;
    public List<Loot> loot = new List<Loot>();
    public Dictionary<Notes, PartyMemberStatus> partyStatus = new Dictionary<Notes, PartyMemberStatus>();
    public bool retreating = false;

    public Party()
    {
        partyStatus.Add(Notes.Bard, new PartyMemberStatus());
        partyStatus.Add(Notes.Cleric, new PartyMemberStatus());
        partyStatus.Add(Notes.Rogue, new PartyMemberStatus());
        partyStatus.Add(Notes.Fighter, new PartyMemberStatus());
    }

    public override int GetFlags()
    {
        return 0;
    }

    public PartyMemberStatus GetStatus(Notes role)
    {
        return partyStatus[role];
    }

    public void Damage(Notes role, int amount)
    {
        partyStatus[role].health -= amount;
    }

    public void Attack(WorldItem target)
    {
        attackTarget = target.GetId();
        harvestTarget = null;
    }

    public void Harvest(WorldItem target)
    {
        if (CanHarvest(target))
        {
            attackTarget = null;
            harvestTarget = target.GetId();
            HarvestItem(target);
        }
    }

    public void Rest()
    {
        foreach (PartyMemberStatus partyMemberStatus in partyStatus.Values)
        {
            partyMemberStatus.resting = true;
        }
    }

    private bool TryCraft(Loot source, Loot target)
    {
        if (!loot.Contains(target))
        {
            loot.Remove(source);
            loot.Add(target);
            return true;
        }
        return false;
    }

    public void Craft()
    {
        foreach (Loot aLoot in loot)
        {
            switch (aLoot)
            {
                case Loot.HONEY:
                    if (TryCraft(aLoot, Loot.HONEY_WEAPON))
                        return;
                    break;
                case Loot.GARNET:
                    if (TryCraft(aLoot, Loot.GARNET_WEAPON))
                        return;
                    break;
                case Loot.JUICE:
                    if (TryCraft(aLoot, Loot.JUICE_WEAPON))
                        return;
                    break;
                case Loot.RUBY:
                    if (TryCraft(aLoot, Loot.RUBY_WEAPON))
                        return;
                    break;
                case Loot.SAFRON:
                    if (TryCraft(aLoot, Loot.SAFRON_WEAPON))
                        return;
                    break;
                case Loot.SAPPHIRE:
                    if (TryCraft(aLoot, Loot.SAPPHIRE_WEAPON))
                        return;
                    break;
            }
        }
    }

    private bool CanHarvest(WorldItem target)
    {
        if (target is Plant && !((Plant)target).harvested)
        {
            switch (((Plant)target).resource)
            {
                case PlantResource.HONEY:
                case PlantResource.JUICE:
                case PlantResource.SAFRON:
                case PlantResource.RUBY:
                case PlantResource.SAPPHIRE:
                case PlantResource.GARNET:
                case PlantResource.TREE:
                    return true;
            }
        }
        return false;
    }

    private void HarvestItem(WorldItem harvestable)
    {
        if (harvestable is Plant)
        {
            ((Plant)harvestable).harvested = true;
        }
        AddItem(GetLoot(harvestable));
    }

    private Loot GetLoot(WorldItem target)
    {
        if (target is Plant)
        {
            switch (((Plant)target).resource)
            {
                case PlantResource.HONEY:
                    return Loot.HONEY;
                case PlantResource.JUICE:
                    return Loot.JUICE;
                case PlantResource.SAFRON:
                    return Loot.SAFRON;
                case PlantResource.RUBY:
                    return Loot.RUBY;
                case PlantResource.SAPPHIRE:
                    return Loot.SAPPHIRE;
                case PlantResource.GARNET:
                    return Loot.GARNET;
                case PlantResource.TREE:
                    return Loot.WOOD;
            }
        }
        return Loot.GOLD;
    }

    public void AddItem(Loot loot)
    {
        this.loot.Add(loot);
    }

    public IEnumerator BeHungry(float waitTime)
    {
        while (true)
        {
            foreach(KeyValuePair<Notes,PartyMemberStatus> kv in partyStatus)
            {
                if(kv.Value.hunger > 0)
                    kv.Value.hunger -= 5;

                if (kv.Value.hunger <= 0)
                    kv.Value.health -= 1;
                Debug.Log(kv.Key + " hunger is: " + kv.Value.hunger);
            }
            yield return new WaitForSeconds(waitTime);
        }
        
    }
}
