using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    LASER_WOLF,
}

public class Monster : WorldItem
{
    public MonsterType monsterType;
    public override int GetFlags()
    {
        return WorldItem.ATTACKABLE | WorldItem.BLOCK_PARTY;
    }
}

public class MonsterComponent : MonoBehaviour, IRhythmListener {
    public static int killCount = 0;
    public Sprite laserWolfIdle0;
    public Sprite laserWolfIdle1;
    public Sprite laserWolfAttack;
    public AudioClip laserSound;
    public AudioSource audioSource;
    public GameObject laserPrefab;
    private int animationState = 0;
    private const int idleFrames = 2;
    private Sprite[] animationFrames;
    private GameObject attackPrefab;
    private AudioClip attackSound;
    private SpriteRenderer spriteRenderer;
    private Monster monster;
    public int health = 6;
    public int attackRange = 1;
    public int aggroRange = 3;

    public void OnBeatRight(int beatNumber)
    {
        animationState = (animationState + 1) % idleFrames;
        if (beatNumber % 4 == 0)
        {
            AI();
        }
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
        audioSource = RhythmManager.instance.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monster = GetComponent<WorldItemComponent>().GetItem<Monster>();
        switch (monster.monsterType)
        {
            case MonsterType.LASER_WOLF:
                animationFrames = new Sprite[] { laserWolfIdle0, laserWolfIdle1, laserWolfAttack };
                attackPrefab = laserPrefab;
                attackSound = laserSound;
                break;
        }
        transform.position = new Vector3(monster.PositionX, 0);
	}

    public bool Damage(int amount)
    {
        health -= amount;
        return true;
    }

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
        SpiralWorldManager.instance.RemoveObject(monster.GetId());
    }

    void AI()
    {
        if (Mathf.Abs(transform.position.x - monster.PositionX) < 0.33f)
        {
            foreach (WorldItem item in SpiralWorldManager.instance.world.GetItemsAt(monster.NumberLinePosition - attackRange, monster.NumberLinePosition + attackRange))
            {
                if (item is Party)
                {
                    GameObject target = SpiralWorldManager.instance.GetWorldGameObject(item.GetId());
                    GameObject bullet = Instantiate(attackPrefab, SpiralWorldManager.instance.transform);
                    bullet.transform.position = transform.position;
                    if (target.transform.position.x < transform.position.x)
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1));
                    }
                    else
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1));
                    }
                    animationState = idleFrames;
                    audioSource.PlayOneShot(attackSound);
                    return;
                }
            }
            foreach (WorldItem item in SpiralWorldManager.instance.world.GetItemsAt(monster.NumberLinePosition - aggroRange, monster.NumberLinePosition + aggroRange))
            {
                if (item is Party)
                {
                    if (item.NumberLinePosition > monster.NumberLinePosition)
                    {
                        monster.NumberLinePosition++;
                    }
                    else
                    {
                        monster.NumberLinePosition--;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        spriteRenderer.sprite = animationFrames[animationState];
        if (transform.position.x != monster.PositionX)
        {
            if (Mathf.Abs(transform.position.x - monster.PositionX) < Time.deltaTime)
            {
                transform.position = new Vector3(monster.PositionX, transform.position.y);
            } else if (transform.position.x > monster.PositionX)
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
            killCount += 1;
        }
    }
}
