using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public float speed;
    Coroutine active;

	// Use this for initialization
	void Start () {
        StartCoroutine(Pendulum(4));
    }

    // Update is called once per frame
    void Update () {
    }

    public void Move(int targetX)
    {
        if (active != null)
        {
            StopCoroutine(active);
        }

        active = StartCoroutine(MoveCoroutine(targetX));
    }

    public void Attack()
    {
        if (active != null)
        {
            StopCoroutine(active);
        }

        active = StartCoroutine(Jump(0.5f));
    }

    public void Craft()
    {
        if (active != null)
        {
            StopCoroutine(active);
        }

        active = StartCoroutine(Pulse(1.0f, 0.5f, 1.25f));
    }

    IEnumerator MoveCoroutine(int finalX)
    {
        while ((int)transform.position.x != finalX)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(finalX, transform.position.y), Time.deltaTime * speed);
            yield return 0;
        }

        active = null;
    }

    private IEnumerator Pulse(float breakTime, float delay, float scaleFactor)
    {
        var baseScale = transform.localScale;
        while (true)
        {
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

            yield return new WaitForSeconds(breakTime);
        }
    }

    IEnumerator Jump(float waitTime)
    {
        int originalY = (int)transform.position.y;
        int maxJump = originalY + 2;

        while (true)
        {
            while ((int)transform.position.y != maxJump)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, maxJump), Time.deltaTime * speed);
                yield return 0;
            }

            while ((int)transform.position.y != originalY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, originalY), Time.deltaTime * 2 *speed);
                yield return 0;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator Pendulum(float waitTime)
    {
        int target = -11;
        while (true)
        {
            switch ((int)Time.time % 3)
            {
                case 0:
                    Move(target *= -1);
                    break;
                case 1:
                    Attack();
                    break;
                case 2:
                    Craft();
                    break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
