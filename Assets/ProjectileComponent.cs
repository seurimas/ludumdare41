using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour {
    public int damage = 1;
    public bool friendly = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthComponent>() != null)
        {
            collision.GetComponent<HealthComponent>().AcceptDamage(this);
        }
    }
}
