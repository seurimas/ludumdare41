using System.Collections;
using System.Collections.Generic;

public interface IWorldItem
{
    int GetPosition();
}

public class SpiralWorld 
{
    private List<IWorldItem> items = new List<IWorldItem>();
    public List<IWorldItem> GetItemsAt(int location)
    {
        List<IWorldItem> itemsAt = new List<IWorldItem>();
        foreach (IWorldItem item in items)
        {
            if (item.GetPosition() == location)
            {
                itemsAt.Add(item);
            }
        }
        return itemsAt;
    }
}
