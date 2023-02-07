using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    [SerializeField]
    Vector3 rayOffset = new Vector3(0, 0.2f, 0);
    [SerializeField]
    LayerMask obstacleLayer;
    [SerializeField]
    float rayLength = 0.9f;
    [SerializeField]
    float heightRayLength = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public   ObstacleInfo checkObstacle()
    {
        var hitdata = new ObstacleInfo();
     
        var rayOrigin = transform.position + rayOffset;
      hitdata.hitFound=  Physics.Raycast(rayOrigin, transform.forward, out hitdata.hitInfo, rayLength, obstacleLayer);
        Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hitdata.hitFound) ? Color.red : Color.green);

        if(hitdata.hitFound)
        {
            var heightOrign = hitdata.hitInfo.point + Vector3.up * heightRayLength;
            hitdata.HeightHitFound = Physics.Raycast(heightOrign, Vector3.down, out hitdata.HeightInfo, heightRayLength, obstacleLayer);
            Debug.DrawRay(heightOrign, Vector3.down * heightRayLength, (hitdata.HeightHitFound) ? Color.blue : Color.green);
        }


        return hitdata;
    }

}

public struct ObstacleInfo
{
   public  bool hitFound;
   public  RaycastHit hitInfo;
    public RaycastHit HeightInfo;
    public bool HeightHitFound;
}