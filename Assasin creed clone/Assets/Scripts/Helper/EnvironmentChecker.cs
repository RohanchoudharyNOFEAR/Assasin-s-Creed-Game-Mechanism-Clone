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
    [Header("Ledge detection")]
    [SerializeField] float ledgeRayLength = 11f;
    [SerializeField] float ledgeRayHeightThreshold = 0.76f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ObstacleInfo checkObstacle()
    {
        var hitdata = new ObstacleInfo();

        var rayOrigin = transform.position + rayOffset;
        hitdata.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitdata.hitInfo, rayLength, obstacleLayer);
        Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hitdata.hitFound) ? Color.red : Color.green);

        if (hitdata.hitFound)
        {
            var heightOrign = hitdata.hitInfo.point + Vector3.up * heightRayLength;
            hitdata.HeightHitFound = Physics.Raycast(heightOrign, Vector3.down, out hitdata.HeightInfo, heightRayLength, obstacleLayer);
            Debug.DrawRay(heightOrign, Vector3.down * heightRayLength, (hitdata.HeightHitFound) ? Color.blue : Color.green);
        }


        return hitdata;
    }

    public bool CheckLedge(Vector3 movementDirection, out LedgeInfo ledgeInfo)
    {
         ledgeInfo = new LedgeInfo();

        if (movementDirection == Vector3.zero)
        {
            return false;
        }
        float ledgeOrignOffset = 0.5f;
        var ledgeOrigin = transform.position + movementDirection * ledgeOrignOffset + Vector3.up;
        if (Physics.Raycast(ledgeOrigin, Vector3.down, out RaycastHit hit, ledgeRayLength, obstacleLayer))
        {
            Debug.DrawRay(ledgeOrigin, Vector3.down * ledgeRayLength, Color.blue);
            var surfaceRaycastOrigin = transform.position + movementDirection - new Vector3(0, 0.1f, 0);
            if (Physics.Raycast(surfaceRaycastOrigin, -movementDirection, out RaycastHit surfaceHit, 2, obstacleLayer))
            {
                float ledgeHeight = transform.position.y - hit.point.y;
                if (ledgeRayHeightThreshold < ledgeHeight)
                {
                   ledgeInfo.Angle= Vector3.Angle(transform.forward, surfaceHit.normal);
                    ledgeInfo.Height = ledgeHeight;
                    ledgeInfo.SurfaceHit = surfaceHit;
                    return true;
                }
            }

        }
        return false;
    }




}

public struct ObstacleInfo
{
    public bool hitFound;
    public RaycastHit hitInfo;
    public RaycastHit HeightInfo;
    public bool HeightHitFound;
}

public struct LedgeInfo
{
    public float Angle;
    public float Height;
    public RaycastHit SurfaceHit;
}