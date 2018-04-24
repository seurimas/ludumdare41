using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldSpawner : MonoBehaviour {
    public float spawnTime = 15;
    private float timeSinceSpawn = 0;
    SpiralWorld world;
	// Use this for initialization
	void Start () {
        world = SpiralWorldManager.instance.world;
        for (int i = 0; i < WorldRenderer.beachSize / WorldItem.WorldScale;i++)
        {
            switch (Random.Range(0, 20))
            {
                case 0:
                case 1:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 2:
                case 3:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.SAFRON,
                        NumberLinePosition = i,
                    });
                    break;
                case 4:
                case 5:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.JUICE,
                        NumberLinePosition = i,
                    });
                    break;
                case 6:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 7:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 8:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 9:
                    world.AddItem(new Animal
                    {
                        NumberLinePosition = i,
                        animalType = AnimalType.JUMPING_WORM,
                    });
                    break;
                case 10:
                    world.AddItem(new Animal
                    {
                        NumberLinePosition = i,
                        animalType = AnimalType.BOUNCING_BUNNY,
                    });
                    break;
                default:
                    break;
            }
        }
        for (int i = WorldRenderer.beachSize / WorldItem.WorldScale; i < WorldRenderer.forestSize / WorldItem.WorldScale;i++)
        {
            switch (Random.Range(0, 20))
            {
                case 0:
                case 1:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.RUBY,
                        NumberLinePosition = i,
                    });
                    break;
                case 2:
                case 3:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.SAPPHIRE,
                        NumberLinePosition = i,
                    });
                    break;
                case 4:
                case 5:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.GARNET,
                        NumberLinePosition = i,
                    });
                    break;
                case 6:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 7:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 8:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.HONEY,
                        NumberLinePosition = i,
                    });
                    break;
                case 9:
                    world.AddItem(new Animal
                    {
                        NumberLinePosition = i,
                        animalType = AnimalType.JUMPING_WORM,
                    });
                    break;
                case 10:
                    world.AddItem(new Animal
                    {
                        NumberLinePosition = i,
                        animalType = AnimalType.BOUNCING_BUNNY,
                    });
                    break;
                case 11:
                case 12:
                    world.AddItem(new Monster
                    {
                        NumberLinePosition = i,
                        monsterType = MonsterType.LASER_WOLF,
                    });
                    break;
                case 13:
                case 14:
                case 15:
                    world.AddItem(new Plant
                    {
                        harvested = false,
                        resource = PlantResource.TREE,
                        NumberLinePosition = i,
                    });
                    break;
                case 16:
                    world.AddItem(new Plant
                    {
                        harvested = true,
                        resource = PlantResource.TREE,
                        NumberLinePosition = i,
                    });
                    break;
                case 17:
                    world.AddItem(new Monster
                    {
                        NumberLinePosition = i,
                        monsterType = MonsterType.LASER_WOLF_2,
                    });
                    break;
                default:
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > spawnTime)
        {
            timeSinceSpawn -= spawnTime;
            int animalPopCount = 0;
            int monsterPopCount = 0;
            foreach (WorldItem item in world.GetItems())
            {
                if (item is Plant)
                {
                    if (Random.Range(0, 6) == 0)
                    {
                        ((Plant)item).harvested = false;
                    }
                }
                else if (item is Animal)
                {
                    animalPopCount++;
                }
                else if (item is Monster)
                {
                    monsterPopCount++;
                }
            }
            if (animalPopCount < 10)
            {
                switch (Random.Range(0, 10))
                {
                    case 0:
                    case 1:
                        world.AddItem(new Animal
                        {
                            NumberLinePosition = Random.Range(5, WorldRenderer.beachSize / WorldItem.WorldScale),
                            animalType = AnimalType.BOUNCING_BUNNY,
                        });
                        break;
                    case 2:
                    case 3:
                        world.AddItem(new Animal
                        {
                            NumberLinePosition = Random.Range(5, WorldRenderer.beachSize / WorldItem.WorldScale),
                            animalType = AnimalType.JUMPING_WORM,
                        });
                        break;
                    case 4:
                        world.AddItem(new Animal
                        {
                            NumberLinePosition = Random.Range(WorldRenderer.beachSize / WorldItem.WorldScale, WorldRenderer.forestSize / WorldItem.WorldScale),
                            animalType = AnimalType.BOUNCING_BUNNY,
                        });
                        break;
                }
            }
            if (monsterPopCount < 10)
            {
                if (Random.Range(0, 3) == 0)
                {
                    world.AddItem(new Monster
                    {
                        NumberLinePosition = Random.Range(WorldRenderer.beachSize / WorldItem.WorldScale, WorldRenderer.forestSize / WorldItem.WorldScale),
                        monsterType = MonsterType.LASER_WOLF,
                    });
                }
            }
        }
	}
}
