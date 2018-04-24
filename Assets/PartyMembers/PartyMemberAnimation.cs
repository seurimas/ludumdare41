using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberAnimation : MonoBehaviour {

    public float speed;
    Coroutine active;
    Vector3 startPosition;
    private PartyComponent party;
    public int offset;
    public float movementSpeed = 5;

    private void Start()
    {
        party = GetComponentInParent<PartyComponent>();
        offset = (int)transform.localPosition.x;
    }

    void Update()
    {
        if (active == null)
        {
            transform.Translate(SpiralWorldManager.TranslationToTarget(transform.position.x - offset,
                party.party.NumberLinePosition,
                movementSpeed * Time.deltaTime
            ));
        }
    }



    public void Attack()
    {
        Interrupt();
        active = StartCoroutine(Jump(0.5f));
    }
    public void Hurt()
    {
        Interrupt();
        active = StartCoroutine(Jump(0.3f));
    }

    public void Craft()
    {
        Interrupt();
        active = StartCoroutine(Pulse(1.0f, 0.5f, 1.25f));
    }

    void Interrupt()
    {
        if (active != null)
        {
            StopCoroutine(active);
            transform.position = startPosition;
        }
    }

    private IEnumerator Pulse(float breakTime, float delay, float scaleFactor)
    {
        var baseScale = transform.localScale;
        float growingTime = Time.time;
        while (growingTime + delay > Time.time)
        {
            transform.localScale = Vector3.Lerp(baseScale * scaleFactor, baseScale, (growingTime + delay) - Time.time);
            yield return 0;
        }

        float shrinkingTime = Time.time;
        while (shrinkingTime + delay > Time.time)
        {
            transform.localScale = Vector3.Lerp(baseScale, baseScale * scaleFactor, (shrinkingTime + delay) - Time.time);
            yield return 0;
        }
        active = null;
    }

    private IEnumerator Jump(float waitTime)
    {
        startPosition = transform.position;
        int originalY = (int)transform.position.y;
        int maxJump = originalY + 2;
        while (transform.position.y != maxJump)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, maxJump, transform.position.z), Time.deltaTime * speed);
            yield return 0;
        }

        while (transform.position.y != originalY)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, originalY, transform.position.z), Time.deltaTime * 2 * speed);
            yield return 0;
        }
        active = null;
    }

}
