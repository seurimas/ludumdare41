using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

public class PlantComponent : MonoBehaviour {
    private Plant plant;
    private SpriteRenderer spriteRenderer;
    public Sprite honeySprite;
    public Sprite safronSprite;
    public Sprite juiceSprite;
    public Sprite emptySprite;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        plant = GetComponent<WorldItemComponent>().GetItem<Plant>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(plant.NumberLinePosition, 0, 0);
		if (plant.harvested)
        {
            spriteRenderer.sprite = emptySprite;
        } else
        {
            switch (plant.resource)
            {
                case PlantResource.HONEY:
                    spriteRenderer.sprite = honeySprite;
                    break;
                case PlantResource.JUICE:
                    spriteRenderer.sprite = juiceSprite;
                    break;
                case PlantResource.SAFRON:
                    spriteRenderer.sprite = safronSprite;
                    break;
            }
        }
	}
}
