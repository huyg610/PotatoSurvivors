using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {

    private const float kHitBackDistance = 4f;
    private const float kLerpPeroid = 2f;
    private const float kLerpRate = 2f; // 
    private TimedLerp mHitLerp = new TimedLerp(kLerpPeroid, kLerpRate);

    private void HitByHero(GameObject g)
    {
        if (mState == EnemyState.ePatrolState)
        {
            mState = EnemyState.eCWRotateState;
            mStateFrameTick = 0;
            GetComponent<SpriteRenderer>().color = kChaseColor;
            mTarget = g;
        }
        else if (mState == EnemyState.eChaseState)
        {
            DisableEnemyCam();

            // Debug.Log("Got you!");
            GlobalBehavior.sTheGlobalBehavior.HeroHitByEnemy();
            sEnemySystem.ReplaceOneEnemy();
            Destroy(gameObject);  // 
        }
    }

    private void HitByEgg(GameObject egg)
    {
        mHitCount++;
        if (mHitCount == 1)
        {
            if (mState == EnemyState.eChaseState)
                DisableEnemyCam();

            mState = EnemyState.eStunState;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/EnemyStunned");

            ComputeHitLerp(1f, egg.transform.up);
        }
        else if (mHitCount == 2)
        {
            mState = EnemyState.eEggState;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Egg");

            ComputeHitLerp(2f, egg.transform.up);
        } else
        {
            ThisEnemyIsHit();
        }
    }

    private void ComputeHitLerp(float s, Vector3 dir)
    {
        Vector3 orgPos = transform.position;
        Vector3 endPos = orgPos + (s * dir * kHitBackDistance);
        mHitLerp.BeginLerp(orgPos, endPos);
    }

    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        Destroy(gameObject);
    }

    private void DisableEnemyCam()
    {
        if (mHasCam)
        {
            sEnemySystem.DisableEnemyCam();
            mHasCam = false;
        }
    }
}