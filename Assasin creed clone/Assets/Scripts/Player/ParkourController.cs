using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField]
    private EnvironmentChecker environmentChecker;


    // Update is called once per frame
    void Update()
    {
      var hitdata = environmentChecker.checkObstacle();
        if(hitdata.hitFound)
        {
            Debug.Log(hitdata.hitInfo.transform.name);
        }
    }
}
