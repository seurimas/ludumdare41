using System;
using System.Collections;
using System.Collections.Generic;

public enum Loot
{
    HONEY,
    SAFRON,
    JUICE,
    RUBY,
    SAPPHIRE,
    GARNET,
    WOOD,
    GOLD,
}

public class Party : WorldItem
{
    public int? attackTarget;
    public int? harvestTarget;
    public List<Loot> loot = new List<Loot>();
    public Dictionary<Notes, int> partyStatus = new Dictionary<Notes, int>();

    public Party()
    {
        partyStatus.Add(Notes.Bard, 10);
        partyStatus.Add(Notes.Cleric, 10);
        partyStatus.Add(Notes.Rogue, 10);
        partyStatus.Add(Notes.Fighter, 10);
    }

    public override int GetFlags()
    {
        return 0;
    }

    public void Damage(Notes role, int amount)
    {
        partyStatus[role] -= amount;
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

    private bool CanHarvest(WorldItem target)
    {
        if (target is Plant && !((Plant)target).harvested)
        {
            switch (((Plant)target).resource)
            {
                case PlantResource.HONEY:
                case PlantResource.JUICE:
                case PlantResource.SAFRON:
                    return partyStatus[Notes.Rogue] > 0;
                case PlantResource.RUBY:
                case PlantResource.SAPPHIRE:
                case PlantResource.GARNET:
                    return partyStatus[Notes.Cleric] > 0;
                case PlantResource.TREE:
                    return partyStatus[Notes.Bard] > 0;
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
}
