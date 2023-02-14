using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour Menu/ Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [SerializeField] string animationName;
    [SerializeField] string barrierTag;
    [SerializeField] float minimumHeight;
    [SerializeField] float maxmimumHeight;
   


    public bool lookAtObstacle;
    public string AnimationName { get { return animationName; } }
    public Quaternion RequiredRotation { get; set; }

    [Header("TargetMatching")]
    [SerializeField] bool allowTargetMatching = true;
    [SerializeField] AvatarTarget compareBodyPart;
    [SerializeField] float compareStartTime;
    [SerializeField] float compareEndTime;
    [SerializeField] float parkourActionDelay;
    [SerializeField] Vector3 comparePositionWeight = new Vector3(0, 1, 0);//for matchTarget
    public Vector3 ComparePosition { get; set; }
    public bool AllowTargetMatching => allowTargetMatching;
    public AvatarTarget CompareBodyPart => compareBodyPart;
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;
    public Vector3 ComparePositionWeight => comparePositionWeight;
    public float ParkourActionDelay => parkourActionDelay;

    public bool CheckIfAvailable(ObstacleInfo hitData, Transform player)
    {

        if(!string.IsNullOrEmpty(barrierTag) && hitData.hitInfo.transform.tag != barrierTag)
        {
            return false;
        }

        float checkheight = hitData.HeightInfo.point.y - player.transform.position.y;
        if (checkheight < minimumHeight || checkheight > maxmimumHeight)
            return false;

        if (lookAtObstacle)
        {
            RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
        }
        if (allowTargetMatching)
        {
            ComparePosition = hitData.HeightInfo.point;
        }
        return true;

    }
}
