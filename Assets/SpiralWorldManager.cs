﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldManager : MonoBehaviour {
    public readonly SpiralWorld world = new SpiralWorld();
    private readonly Dictionary<int, GameObject> worldObjects = new Dictionary<int, GameObject>();
    private PartyComponent party;
    public GameObject plantPrefab;
    public GameObject animalPrefab;
    public int renderStart;
    public int renderEnd;
	// Use this for initialization
	void Start () {
        party = GetComponentInChildren<PartyComponent>();
        world.AddItem(party.party);
        Plant dummyPlant0 = new Plant
        {
            harvested = false,
            resource = PlantResource.HONEY,
            NumberLinePosition = 0,
        };
        world.AddItem(dummyPlant0);
        Plant dummyPlant1 = new Plant
        {
            harvested = false,
            resource = PlantResource.SAFRON,
            NumberLinePosition = 1,
        };
        world.AddItem(dummyPlant1);
        Plant dummyPlant2 = new Plant
        {
            harvested = false,
            resource = PlantResource.JUICE,
            NumberLinePosition = 2,
        };
        world.AddItem(dummyPlant2);
        Animal dummyAnimal0 = new Animal
        {
            NumberLinePosition = 3,
            animalType = AnimalType.BOUNCING_BUNNY,
        };
        world.AddItem(dummyAnimal0);
    }
    
	// Update is called once per frame
	void Update () {
        renderStart = party.party.NumberLinePosition - 16;
        renderEnd = party.party.NumberLinePosition + 16;
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
