using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType
{
    BOUNCING_BUNNY,
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
    private int animationState = 0;
    private Sprite[] animationFrames;
    private SpriteRenderer spriteRenderer;
    private Animal animal;

    public void OnBeatRight()
    {
        animationState = (animationState + 1) % animationFrames.Length;
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
        }
        transform.position = new Vector3(animal.NumberLinePosition, 0);
	}

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
    }

    // Update is called once per frame
    void Update () {
        spriteRenderer.sprite = animationFrames[animationState];
        if (transform.position.x != animal.NumberLinePosition)
        {
            if (Mathf.Abs(transform.position.x - animal.NumberLinePosition) < Time.deltaTime)
            {
                transform.position = new Vector3(animal.NumberLinePosition, transform.position.y);
            } else if (transform.position.x > animal.NumberLinePosition)
            {
                transform.Translate(-Time.deltaTime, 0, 0);
            } else
            {
                transform.Translate(Time.deltaTime, 0, 0);
            }
        }
	}
}
