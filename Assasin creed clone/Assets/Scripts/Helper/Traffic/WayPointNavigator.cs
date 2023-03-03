using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointNavigator : MonoBehaviour
{
    [Header("AI Character")]
    CharacterNavigator character;
    public WayPoint currentWaypoint;
    int direction;


    private void Awake()
    {
        character = GetComponent<CharacterNavigator>();
    }


    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        character.LocateDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }

            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }
            character.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}
