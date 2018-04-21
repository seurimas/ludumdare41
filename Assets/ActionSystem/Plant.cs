using System.Collections;

public enum PlantResource
{
    HONEY,
    SAFRON,
    JUICE,
};

public class Plant : WorldItem
{
    public bool harvested = false;
    public PlantResource resource;
    public override int GetFlags()
    {
        return WorldItem.HARVESTABLE;
    }
}
