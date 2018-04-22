using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AggressiveComponent : MonoBehaviour, IRhythmListener
{
    public int followRange = 2;
    public int attackRange = 1;
    public int damage = 2;
    private WorldItem worldItem;
    // Use this for initialization
    void Start()
    {
        RhythmManager.instance.AddListener(this);
        worldItem = GetComponent<WorldItemComponent>().worldItem;
    }

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        return false;
    }

    public void OnBeatRight(int beatNumber)
    {
        if (beatNumber % 4 != 0)
        {
            return;
        }
        foreach (WorldItem item in SpiralWorldManager.instance.world.GetItemsAt(worldItem.NumberLinePosition - followRange, worldItem.NumberLinePosition + followRange))
        {
            if (item is Party)
            {
                if (Mathf.Abs(item.NumberLinePosition - worldItem.NumberLinePosition) > attackRange)
                {
                    if (item.NumberLinePosition > worldItem.NumberLinePosition)
                    {
                        worldItem.NumberLinePosition++;
                    } else
                    {
                        worldItem.NumberLinePosition--;
                    }
                } else if (worldItem is Animal && ((Animal)worldItem).animalType == AnimalType.JUMPING_WORM)
                {

                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PartyMember>() != null)
        {

        }
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
    }
}
