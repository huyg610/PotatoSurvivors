using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private static WayPointSystem sWayPointSystem = null;
    public static void SetWayPointSystem(WayPointSystem s) { sWayPointSystem = s; }

    private Vector3 mInitPosition = Vector3.zero;
    private int mHitCount = 0;
    private const int kHitLimit = 3;
    private const float kRepositionRange = 15f; // +- this value
    private Color mNormalColor = Color.white;

    private ShakePosition mShake = new ShakePosition(kShakeFrequency, 1f);
    private const float kShakeFrequency = 10f;
    private bool mHasCam = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mInitPosition = transform.position;
    }

    private void Update()
    {
        if (!mShake.ShakeDone())
        {
            transform.position = mShake.UpdateShake();
        }
        
    }


    private void Reposition() 
    {
        Vector3 p = mInitPosition;
        p += new Vector3(Random.Range(-kRepositionRange, kRepositionRange),
                         Random.Range(-kRepositionRange, kRepositionRange),
                         0f);
        transform.position = p;
        GetComponent<SpriteRenderer>().color = mNormalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Egg(Clone)")
        {
            mHitCount++;
            Color c = mNormalColor * (float)(kHitLimit - mHitCount + 1) / (float)(kHitLimit + 1);
            GetComponent<SpriteRenderer>().color = c;
            if (mHitCount > kHitLimit)
            {
                mHitCount = 0;
                Reposition();
            } else
            {   
                if (!mHasCam)
                    mHasCam = sWayPointSystem.EnableWayPointCam(transform.position);

                Vector3 p = transform.position;
                if (!mShake.ShakeDone())
                    p = mShake.GetShakeFinalPos();  // this is where the final position should be
                mShake.SetShakeParameters(kShakeFrequency, (float)mHitCount);
                mShake.SetShakeMagnitude(new Vector2(mHitCount, mHitCount), p, ShakeDone);
            }
        }
    }

    private void ShakeDone()
    {
        if (mHasCam)
        {
            mHasCam = false;
            sWayPointSystem.DisableWayPointCam();
        }
    }
}
