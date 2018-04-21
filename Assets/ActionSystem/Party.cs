using System.Collections;

public class Party : IWorldItem
{
    private int numberLinePosition;
    public int NumberLinePosition
    {
        get { return numberLinePosition; }
    }
    public int GetPosition()
    {
        return numberLinePosition;
    }
    public void applyAction(IPartyAction action)
    {
        if (action is AdvanceAction)
        {
            numberLinePosition++;
        } else if (action is RetreatAction) {
            numberLinePosition--;
        }
    }
}
