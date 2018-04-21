using System.Collections;

public class Party : WorldItem
{
    public int? attackTarget;
    public int? harvestTarget;
    
    public int GetPosition()
    {
        return NumberLinePosition;
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
        attackTarget = null;
        harvestTarget = target.GetId();
    }
}
