using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class WayPointSystem : MonoBehaviour
{
    private string[] kWayPointNames = {
            "WayPointA", "WayPointB", "WayPointC",
            "WayPointD", "WayPointE", "WayPointF"};
    private GameObject[] mWayPoints;
    private const int kNumWayPoints = 6;
    private const float kWayPointTouchDistance = 25f;
    private bool mPointsInSequence = true;

    public CameraSupport mWayCam = null;
    public Text mCamLabel = null;

    // Start is called before the first frame update
    void Start()
    {
        WayPoint.SetWayPointSystem(this);

        mWayPoints = new GameObject[kWayPointNames.Length];
        int i = 0;
        foreach (string s in kWayPointNames)
        {
            mWayPoints[i] = GameObject.Find(kWayPointNames[i]);
            Debug.Assert(mWayPoints[i] != null);
            i++;
        }

        // cam
        Debug.Assert(mWayCam != null);
        mWayCam.SetLerpParameters(0f, 0f);  // switch off Lerp
        mCamLabel.text = kDisabledMsg;
        DisableWayPointCam();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleVisibility();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleSequenceOrder();
        }
    }

    private void ToggleVisibility()
    {
        foreach (GameObject g in mWayPoints)
            g.SetActive(!g.activeSelf);
    }

    private void ToggleSequenceOrder()
    {
        mPointsInSequence = !mPointsInSequence;
    }
    public bool WayPointInSequence() { return mPointsInSequence; }

    public void CheckNextWayPoint(Vector3 p, ref int index)
    {
        if (Vector3.Distance(p, mWayPoints[index].transform.position) < kWayPointTouchDistance)
        {
            if (mPointsInSequence)
            {
                index++;
                if (index >= kNumWayPoints)
                    index = 0;
            } else
            {
                index = Random.Range(0, 5);
            }
        }
    }

    public int GetInitWayIndex()
    {
        return Random.Range(0, mWayPoints.Length);
    }

    // Update is called once per frame
    public Vector3 GetWayPoint(int index) 
    {
        return mWayPoints[index].transform.position;
    }

    public string GetWayPointState() { return "WayPoints(" + (WayPointInSequence() ? "Sequence" : "Random") + ")"; }
}
