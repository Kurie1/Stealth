using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;
    public float Speed = 5f;
    public float WaitTime = 2f;
    public float turnSpeed = 90f;

    private void Start()
    {
        Vector3[] wayPoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }
        StartCoroutine(FollowPath(wayPoints));
       
    }

    private IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, Speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return TurnToFace(targetWaypoint);
            }
            yield return null;
        }
    }
    private IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float counter = 0;
        while (transform.forward != dirToLookTarget)
        {
            transform.forward = Vector3.Lerp(transform.forward, dirToLookTarget, counter/turnSpeed);
            counter += Time.deltaTime;
            yield return null;
        }

    }
    

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach(Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }
    // Start is called before the first frame update

}
