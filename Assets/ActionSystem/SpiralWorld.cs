using System.Collections;
using System.Collections.Generic;

public abstract class WorldItem
{
    public int NumberLinePosition;
    
    private int id;
    public void SetId(int id)
    {
        this.id = id;
    }
    public int GetId()
    {
        return id;
    }

    public abstract int GetFlags();
    public const int BLOCK_PARTY = 1 << 0;
    public const int HARVESTABLE = 1 << 1;
    public const int ATTACKABLE = 1 << 2;
    public bool BlocksParty()
    {
        return (BLOCK_PARTY & GetFlags()) != 0;
    }
    public bool IsHarvestable()
    {
        return (HARVESTABLE & GetFlags()) != 0;
    }
    public bool IsAttackable()
    {
        return (ATTACKABLE & GetFlags()) != 0;
    }
}


public class SpiralWorld
{
    private Dictionary<int, WorldItem> items = new Dictionary<int, WorldItem>();
    private int lastId = 0;
    public IEnumerable<WorldItem> GetItems()
    {
        return items.Values;
    }
    public List<WorldItem> GetItemsAt(int location)
    {
        List<WorldItem> itemsAt = new List<WorldItem>();
        foreach (WorldItem item in items.Values)
        {
            if (item.NumberLinePosition == location)
            {
                itemsAt.Add(item);
            }
        }
        return itemsAt;
    }
    public List<WorldItem> GetItemsAt(int start, int end)
    {
        List<WorldItem> itemsAt = new List<WorldItem>();
        foreach (WorldItem item in items.Values)
        {
            if (item.NumberLinePosition >= start && item.NumberLinePosition <= end)
            {
                itemsAt.Add(item);
            }
        }
        return itemsAt;
    }
    public void AddItem(WorldItem item)
    {
        int id = lastId++;
        item.SetId(id);
        items.Add(id, item);
    }
    public WorldItem GetItem(int id)
    {
        return items[id];
    }
    public void RemoveItem(int id)
    {
        items.Remove(id);
    }
}
