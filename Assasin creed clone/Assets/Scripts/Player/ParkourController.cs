using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ctrl k+d formatting
public class ParkourController : MonoBehaviour
{
    [SerializeField]
    private EnvironmentChecker environmentChecker;
    public float AutoJumpHeightLimit = 2f;
    public PlayerController PlayerController;
    bool playerInAction;
    [SerializeField]
    Animator animator;
    [SerializeField] NewParkourAction jumpDownAction;

    [Header("Parkour action")]
    public List<NewParkourAction> NewParkourActions;


    // Update is called once per frame
    void Update()
    {
        var hitdata = environmentChecker.checkObstacle();
        if (Input.GetButtonDown("Jump") && !playerInAction)
        {
           
            if (hitdata.hitFound)
            {
                foreach (var action in NewParkourActions)
                {
                    if (action.CheckIfAvailable(hitdata, transform))
                    {
                        Debug.Log(hitdata.hitInfo.transform.name);
                        StartCoroutine(performParkourAction(action));
                        break;
                    }
                }
            }
        }

        if(PlayerController.PlayerOnLedge && !playerInAction&& !hitdata.hitFound )
        {
            bool canJump = true;
            if(PlayerController.LedgeInfo.Height>AutoJumpHeightLimit && !Input.GetButton("Jump"))
            {
                canJump = false;
            }

            if(PlayerController.LedgeInfo.Angle<=50 && canJump)
            {
                PlayerController.PlayerOnLedge = false;
                StartCoroutine(performParkourAction(jumpDownAction));
            }
          
        }

    }

    IEnumerator performParkourAction(NewParkourAction action)
    {
        playerInAction = true;
        PlayerController.SetControl(false);
        animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;
        var animationState = animator.GetNextAnimatorStateInfo(0);
        if (!animationState.IsName(action.AnimationName))
            Debug.Log("animation name is incorrect");


        //  yield return new WaitForSeconds(animationState.length);

        float timerCounter = 0f;

        while (timerCounter <= animationState.length)
        {
            timerCounter += Time.deltaTime;
            if (action.lookAtObstacle)
            {
                Debug.Log(action.RequiredRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, PlayerController.RotSpeed * Time.deltaTime);
            }
            if (action.AllowTargetMatching)
            {
                compareTarget(action);
            }

            if(animator.IsInTransition(0)&& timerCounter>0.5f)
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(action.ParkourActionDelay);

        PlayerController.SetControl(true);
        playerInAction = false;

    }


    void compareTarget(NewParkourAction action)
    {
        animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(action.ComparePositionWeight, 0), action.CompareStartTime, action.CompareEndTime);
    }

}
