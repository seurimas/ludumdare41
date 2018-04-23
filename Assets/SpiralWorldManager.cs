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
    public GameObject monsterPrefab;
    public int renderStart;
    public int renderEnd;
    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        party = GetComponentInChildren<PartyComponent>();
        world.AddItem(party.party);
        worldObjects.Add(party.party.GetId(), party.gameObject);
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
                    Debug.Log(string.Format("Spawning ({0})", item.GetId()));
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
        Debug.Log(worldObjects[0]);
        Debug.Log(itemId);
        if (worldObjects.ContainsKey(itemId))
        {
            return worldObjects[itemId];
        }
        return null;
    }

    public void RemoveObject(int itemId)
    {
        Debug.Log(string.Format("Removing ({0})", itemId));
        worldObjects.Remove(itemId);
        world.RemoveItem(itemId);
    }

    GameObject InstantiateWorldItem(WorldItem worldItem)
    {
        GameObject spawned = null;
        if (worldItem is Plant)
        {
            spawned = Instantiate(plantPrefab, transform);
        }
        if (worldItem is Animal)
        {
            spawned = Instantiate(animalPrefab, transform);
        }
        if (worldItem is Monster)
        {
            spawned = Instantiate(monsterPrefab, transform);
        }
        if (spawned != null)
        {
            spawned.GetComponent<WorldItemComponent>().worldItem = worldItem;
        }
        return spawned;
    }
}
