using UnityEngine;
using System.Collections;

public partial class EnemySpawnSystem : MonoBehaviour {

    private const string kEnabledMsg =  "Enemy Chase Cam: Active";
    private const string kDisabledMsg = "Enemy Chase Cam: Shut Off";

    public void UpdateEnemyCam(Vector3 from, Vector3 to)
    {
        // Debug.Assert(mEnemyCam.gameObject.activeSelf);
        Vector3 d = from - to;
        Vector3 at = to + 0.5f * d;
        mEnemyCam.MoveTo(at.x, at.y);
        mEnemyCam.ZoomToNewSize(d.magnitude);
    }

    public bool EnableEnemyCam(Vector3 from, Vector3 to)
    {
        if (mEnemyCam.gameObject.activeSelf)
            return false;  // simply ignore this

        mEnemyCam.gameObject.SetActive(true);
        UpdateEnemyCam(from, to);
        mCamLabel.text = kEnabledMsg;
        return true;
    }

    public void DisableEnemyCam()
    {
        mEnemyCam.gameObject.SetActive(false);
        mCamLabel.text = kDisabledMsg;
    }
}