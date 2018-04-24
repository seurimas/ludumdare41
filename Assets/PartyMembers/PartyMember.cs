using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour, IRhythmListener {
    public Notes role;
    public Sprite[] idleFrames;
    public Sprite restSprite;
    public Sprite deadSprite;
    private int frameState = 0;
    private new PartyMemberAnimation animation;
    private PartyComponent party;
    int offset;
    public GameObject attackObject;
    public event EventHandler<PartyMemberEventArgs> NoteProcessed;

    public Sprite rubyWeaponSprite;
    public Sprite sapphireWeaponSprite;
    public Sprite garnetWeaponSprite;
    public Sprite honeyWeaponSprite;
    public Sprite safronWeaponSprite;
    public Sprite juiceWeaponSprite;

    // Use this for initialization
    void Start () {
        offset = (int)transform.localPosition.x;
        animation = GetComponent<PartyMemberAnimation>();
        party = GetComponentInParent<PartyComponent>();
        RhythmManager.instance.AddListener(this);
        GetComponent<SpriteRenderer>().sprite = idleFrames[0];
    }

    void Update()
    {
        if (IsDead())
        {
            GetComponent<SpriteRenderer>().sprite = deadSprite;
        }
    }

    void OnDestroy()
    {
        RhythmManager.instance.RemoveListener(this);
    }

    public bool IsDead()
    {
        return party.party.GetStatus(role).health <= 0;
    }

    public bool Damage(int amount)
    {
        if (IsDead())
        {
            return false;
        }
        animation.Hurt();
        party.party.Damage(role, amount);
        return true;
    }

    public void Attack(GameObject target)
    {
        if (IsDead())
        {
            return;
        }
        animation.Attack();
        float distance = target.transform.position.x - transform.position.x;
        GameObject newItem = Instantiate(attackObject, party.transform.parent);
        ProjectileComponent projectile = newItem.GetComponent<ProjectileComponent>();
        projectile.friendly = true;
        SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        int damage = 1;
        switch (role)
        {
            case Notes.Bard:
                if (party.party.loot.Contains(Loot.RUBY_WEAPON))
                {
                    sprite = rubyWeaponSprite;
                    damage = 3;
                }
                break;
            case Notes.Cleric:
                if (party.party.loot.Contains(Loot.SAPPHIRE_WEAPON))
                {
                    sprite = sapphireWeaponSprite;
                    damage = 3;
                } else if (party.party.loot.Contains(Loot.HONEY_WEAPON))
                {
                    sprite = honeyWeaponSprite;
                    damage = 2;
                }
                break;
            case Notes.Rogue:
                if (party.party.loot.Contains(Loot.GARNET_WEAPON))
                {
                    sprite = rubyWeaponSprite;
                    damage = 3;
                } else if (party.party.loot.Contains(Loot.JUICE_WEAPON))
                {
                    sprite = juiceWeaponSprite;
                    damage = 2;
                }
                break;
            case Notes.Fighter:
                if (party.party.loot.Contains(Loot.SAFRON_WEAPON))
                {
                    sprite = safronWeaponSprite;
                    damage = 2;
                }
                break;
        }
        spriteRenderer.sprite = sprite;
        projectile.damage = damage;
        newItem.transform.position = transform.position;
        Rigidbody2D rigidBody2D = newItem.GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = new Vector3(distance, 5);
    }


    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        if(note == role)
        {
            Debug.Log("Pm says note correct");
            if (NoteProcessed != null)
                NoteProcessed.Invoke(this, new PartyMemberEventArgs(transform,role));
        }
        return false;
    }

    public void OnBeatRight(int beatNumber)
    {
        if (MyTurn(beatNumber))
        {
            if (party.party.attackTarget.HasValue)
            {
                GameObject target = party.spiralWorldManager.GetWorldGameObject(party.party.attackTarget.Value);
                if (target != null)
                {
                    Attack(target);
                    return;
                }
            }
            PartyMemberStatus status = party.party.GetStatus(role);
            if (status.resting && (status.health < status.maxHealth || status.hunger < status.maxHunger / 2))
            {
                foreach (Loot loot in party.party.loot)
                {
                    bool ate = false;
                    switch (loot)
                    {
                        case Loot.HONEY:
                        case Loot.JUICE:
                        case Loot.SAFRON:
                            status.health = Math.Min(status.health + 5, status.maxHealth);
                            status.hunger = status.maxHunger;
                            party.party.loot.Remove(loot);
                            ate = true;
                            break;
                    }
                    if (ate)
                    {
                        break;
                    }
                }
            }
            if (status.resting && status.health >= status.maxHealth)
            {
                status.resting = false;
            }
        }
        frameState = (frameState + 1) % idleFrames.Length;
        if (!IsDead())
        {
            if (party.party.GetStatus(role).resting)
            {
                GetComponent<SpriteRenderer>().sprite = restSprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = idleFrames[frameState];
            }
        }
    }



    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {

    }

    bool MyTurn(int beatNumber)
    {
        if (IsDead())
            return false;
        switch (role)
        {
            case Notes.Bard:
                return beatNumber % 4 == 0;
            case Notes.Cleric:
                return beatNumber % 4 == 1;
            case Notes.Rogue:
                return beatNumber % 4 == 2;
            case Notes.Fighter:
                return beatNumber % 4 == 3;
        }
        return false;
    }
}

public class PartyMemberEventArgs : EventArgs
{
    public Transform transform;
    public Notes role;

    public PartyMemberEventArgs(Transform transform, Notes role)
    {
        this.transform = transform;
        this.role = role;
    }
}
