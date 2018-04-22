using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {
    private WorldItemComponent worldItem;
    private PartyMember partyMember;
	// Use this for initialization
	void Start () {
        worldItem = GetComponent<WorldItemComponent>();
        partyMember = GetComponent<PartyMember>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AcceptDamage(ProjectileComponent projectile)
    {
        if (partyMember != null && !projectile.friendly)
        {
            if (partyMember.Damage(projectile.damage))
                Destroy(projectile.gameObject);
        } else if (worldItem != null && projectile.friendly)
        {
            if (worldItem.Damage(projectile.damage))
                Destroy(projectile.gameObject);
        }
    }
}
