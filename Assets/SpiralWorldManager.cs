using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldManager : MonoBehaviour {
    public static Vector3 TranslationToTarget(float self, float numLine, float maxSpeed)
    {
        float distance = numLine * WorldItem.WorldScale - self;
        return new Vector3(Mathf.Clamp(distance, -maxSpeed, maxSpeed), 0);
    }
    public static SpiralWorldManager instance;
    public readonly SpiralWorld world = new SpiralWorld();
    private readonly Dictionary<int, GameObject> worldObjects = new Dictionary<int, GameObject>();
    private PartyComponent party;
    public GameObject plantPrefab;
    public GameObject animalPrefab;
    public int renderStart;
    public int renderEnd;
	// Use this for initialization
	void Start () {
        instance = this;
        party = GetComponentInChildren<PartyComponent>();
        world.AddItem(party.party);
        world.AddItem(new Plant
        {
            harvested = false,
            resource = PlantResource.HONEY,
            NumberLinePosition = 0,
        });
        world.AddItem(new Plant
        {
            harvested = false,
            resource = PlantResource.SAFRON,
            NumberLinePosition = 3,
        });
        world.AddItem(new Plant
        {
            harvested = false,
            resource = PlantResource.JUICE,
            NumberLinePosition = 2,
        });
        world.AddItem(new Plant
        {
            harvested = false,
            resource = PlantResource.RUBY,
            NumberLinePosition = 4,
        });
        world.AddItem(new Plant
        {
            harvested = false,
            resource = PlantResource.SAPPHIRE,
            NumberLinePosition = 5,
        });
        world.AddItem(new Plant
        {
            harvested = false,
            resource = PlantResource.GARNET,
            NumberLinePosition = 6,
        });
        world.AddItem(new Animal
        {
            NumberLinePosition = 1,
            animalType = AnimalType.BOUNCING_BUNNY,
        });
    }
    
	// Update is called once per frame
	void Update () {
        renderStart = party.party.NumberLinePosition - 8;
        renderEnd = party.party.NumberLinePosition + 8;
		foreach (WorldItem item in world.GetItems())
        {
            if (item.NumberLinePosition >= renderStart && item.NumberLinePosition <= renderEnd)
            {
                if (!worldObjects.ContainsKey(item.GetId()))
                {
                    worldObjects.Add(item.GetId(), InstantiateWorldItem(item));
                }
            } else
            {
                if (worldObjects.ContainsKey(item.GetId()))
                {
                    Destroy(worldObjects[item.GetId()]);
                    worldObjects.Remove(item.GetId());
                }
            }
        }
	}

    public GameObject GetWorldGameObject(int itemId)
    {
        if (worldObjects.ContainsKey(itemId))
        {
            return worldObjects[itemId];
        }
        return null;
    }

    public void RemoveObject(int itemId)
    {
        worldObjects.Remove(itemId);
        world.RemoveItem(itemId);
    }

    GameObject InstantiateWorldItem(WorldItem worldItem)
    {
        GameObject spawned = null;
        if (worldItem is Plant)
        {
            spawned = Instantiate(plantPrefab, transform);
            spawned.GetComponent<WorldItemComponent>().worldItem = worldItem;
        }
        if (worldItem is Animal)
        {
            spawned = Instantiate(animalPrefab, transform);
            spawned.GetComponent<WorldItemComponent>().worldItem = worldItem;
        }
        return spawned;
    }
}
