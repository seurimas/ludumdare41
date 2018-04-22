using System;
using System.Collections;
using System.Collections.Generic;

public enum Loot
{
    HONEY,
    SAFRON,
    JUICE,
    GOLD,
}

public class Party : WorldItem
{
    public int? attackTarget;
    public int? harvestTarget;
    public List<Loot> loot = new List<Loot>();
    public Dictionary<Notes, bool> partyStatus = new Dictionary<Notes, bool>();

    public Party()
    {
        partyStatus.Add(Notes.Bard, true);
        partyStatus.Add(Notes.Cleric, true);
        partyStatus.Add(Notes.Rogue, true);
        partyStatus.Add(Notes.Fighter, true);
    }

    public override int GetFlags()
    {
        return 0;
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
        if (target is Plant)
        {
            return !((Plant)target).harvested && partyStatus[Notes.Rogue];
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
            }
        }
        return Loot.GOLD;
    }

    public void AddItem(Loot loot)
    {
        this.loot.Add(loot);
    }
}
