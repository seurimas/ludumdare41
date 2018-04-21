using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    public List<Transform> wayPoints;
    public float speed = 3f;
    // Always true at the beginning because the moving object will always move towards the first waypoint
    public bool inReverse = true;

    private Transform currentWaypoint;
    private int currentIndex = 0;


    void Start()
    {
        if (wayPoints.Count > 0)
        {
            currentWaypoint = wayPoints[0];
        }
    }

    /**
	 * Update is called once per frame
	 * 
	 */
    void Update()
    {
        if (currentWaypoint != null)
        {
            MoveTowardsWaypoint();
        }
    }


    /**
	 * Move the object towards the selected waypoint
	 * 
	 */
    private void MoveTowardsWaypoint()
    {
        // Get the moving objects current position
        Vector3 currentPosition = transform.position;

        // Get the target waypoints position
        Vector3 targetPosition = currentWaypoint.transform.position;

        // Get the direction and normalize
        Vector3 directionOfTravel = targetPosition - currentPosition;
        directionOfTravel.Normalize();

        transform.Translate(directionOfTravel * speed * Time.deltaTime, Space.World);

        // If the moving object isn't that close to the waypoint
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {
            transform.Translate(directionOfTravel * speed * Time.deltaTime, Space.World);
        }
        else
        {
            NextWaypoint();
        }
    }

    /**
	 * Work out what the next waypoint is going to be
	 * 
	 */
    private void NextWaypoint()
    {
        // If at the start or the end then reverse
        if ((!inReverse && currentIndex + 1 >= wayPoints.Count) || (inReverse && currentIndex == 0))
        {
            inReverse = !inReverse;
        }
        currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;

        currentWaypoint = wayPoints[currentIndex];
    }
}