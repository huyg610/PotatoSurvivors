using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class WayPointSystem : MonoBehaviour
{
    const string kEnabledMsg = "WayPoint Cam: Active";
    const string kDisabledMsg = "WayPoint Cam: Shut Off";

    public bool EnableWayPointCam(Vector3 p)
    {
        if (mWayCam.gameObject.activeSelf)
            return false;  // simply ignore this

        mWayCam.gameObject.SetActive(true);
        mWayCam.MoveTo(p.x, p.y);
        mCamLabel.text = kEnabledMsg;
        return true;
    }

    public void DisableWayPointCam()
    {
        mWayCam.gameObject.SetActive(false);
        mCamLabel.text = kDisabledMsg;
    }
}
