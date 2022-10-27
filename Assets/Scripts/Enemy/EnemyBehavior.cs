using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {

    // All instances of Enemy shares this one WayPoint and EnemySystem
    static private WayPointSystem sWayPoints = null;
    static private EnemySpawnSystem sEnemySystem = null;
    static public void InitializeEnemySystem(EnemySpawnSystem s, WayPointSystem w) { sEnemySystem = s; sWayPoints = w; }

    private const float kSpeed = 5f;
    private int mWayPointIndex = 0;

    private const float kTurnRate = 0.03f/60f;
    private int mHitCount = 0;
		
	// Use this for initialization
	void Start () {
        Debug.Assert(sWayPoints != null);
        Debug.Assert(sEnemySystem != null);
        mWayPointIndex = sWayPoints.GetInitWayIndex();
	}
	
	// Update is called once per frame
	void Update () {
        if (mHitLerp.LerpIsActive())
            transform.position = mHitLerp.UpdateLerp();
        UpdateFSM();	
	}

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerCheck(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Hero")
        {
            HitByHero(g.gameObject);

        } else if (g.name == "Egg(Clone)")
        {
            HitByEgg(g);
        }
    }
    #endregion
}
