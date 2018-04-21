using System.Collections;

public class Party
{
    private int numberLinePosition;
    public int NumberLinePosition
    {
        get { return numberLinePosition; }
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
