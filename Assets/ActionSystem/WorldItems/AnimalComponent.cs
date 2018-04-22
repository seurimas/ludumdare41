using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType
{
    BOUNCING_BUNNY,
    JUMPING_WORM,
}

public class Animal : WorldItem
{
    public AnimalType animalType;
    public override int GetFlags()
    {
        return WorldItem.ATTACKABLE;
    }
}

public class AnimalComponent : MonoBehaviour, IRhythmListener {
    public Sprite bunnyIdle0;
    public Sprite bunnyIdle1;
    public Sprite wormIdle0;
    public Sprite wormIdle1;
    private int animationState = 0;
    private const int idleFrames = 2;
    private Sprite[] animationFrames;
    private SpriteRenderer spriteRenderer;
    private Animal animal;
    public int health = 2;

    public void OnBeatRight(int beatNumber)
    {
        animationState = (animationState + 1) % idleFrames;
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        return false;
    }

    // Use this for initialization
    void Start () {
        RhythmManager.instance.AddListener(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animal = GetComponent<WorldItemComponent>().GetItem<Animal>();
        switch (animal.animalType)
        {
            case AnimalType.BOUNCING_BUNNY:
                animationFrames = new Sprite[] { bunnyIdle0, bunnyIdle1 };
                break;
            case AnimalType.JUMPING_WORM:
                animationFrames = new Sprite[] { wormIdle0, wormIdle1 };
                break;
        }
        transform.position = new Vector3(animal.PositionX, 0);
	}

    public void Damage(int amount)
    {
        health -= amount;
    }

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
        SpiralWorldManager.instance.RemoveObject(animal.GetId());
    }

    // Update is called once per frame
    void Update () {
        spriteRenderer.sprite = animationFrames[animationState];
        if (transform.position.x != animal.PositionX)
        {
            if (Mathf.Abs(transform.position.x - animal.PositionX) < Time.deltaTime)
            {
                transform.position = new Vector3(animal.PositionX, transform.position.y);
            } else if (transform.position.x > animal.PositionX)
            {
                transform.Translate(-Time.deltaTime, 0, 0);
            } else
            {
                transform.Translate(Time.deltaTime, 0, 0);
            }
        }
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
