using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantComponent : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public Sprite honeySprite;
    public Sprite safronSprite;
    public Sprite juiceSprite;
    public Sprite emptySprite;

    private Plant GetPlant()
    {
        return (Plant)GetComponent<WorldItemComponent>().worldItem;
    }

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Plant plant = GetPlant();
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
