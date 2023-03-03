using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour
{
    public Vector3 destination;
    public float MovingSpeed;
    public float TurningSpeed=300f;
    public float StopSpeed;
    public bool destinationReached;

    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        if(transform.position!=destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            if(destinationDistance>=StopSpeed)
            {
                destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurningSpeed * Time.deltaTime);

                transform.Translate(MovingSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                destinationReached = true;
            }
            
        }
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

}
