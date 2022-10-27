using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlobalBehavior : MonoBehaviour {
    public static GlobalBehavior sTheGlobalBehavior = null;

    public Text mGameStateEcho = null;  // Defined in UnityEngine.UI
    public HeroBehavior mHero = null;
    public EnemySpawnSystem mEnemySystem = null;
    public WayPointSystem mWayPoints = null;

    private CameraSupport mMainCamera;

    private const float kInitSpawnRegionHeight = 80f;

    private void Awake()
    {
        // This must occur before EnemySystem's Start();
        Debug.Assert(mEnemySystem != null);
        Debug.Assert(mWayPoints != null);
        Debug.Assert(mHero != null);

        // initialize the Enemy spawn region: before enemy system's Start()!!
        float w = kInitSpawnRegionHeight * Camera.main.aspect;
        Vector2 max = new Vector2(w, kInitSpawnRegionHeight);
        mEnemySystem.SetSpawnRegion(-max, max);

        // Make sure all enemy sees the same EnemySystem and WayPointSystem
        EnemyBehavior.InitializeEnemySystem(mEnemySystem, mWayPoints);

        GlobalBehavior.sTheGlobalBehavior = this;  // Singleton pattern
        mMainCamera = Camera.main.GetComponent<CameraSupport>();
        Debug.Assert(mMainCamera != null);
    }

    // Use this for initialization
    void Start () {
    }
    
	void Update () {
        EchoGameState(); // always do this
    }

    #region Bound Support
    public CameraSupport.WorldBoundStatus CollideWorldBound(Bounds b) { return mMainCamera.CollideWorldBound(b); }
    #endregion 

    public void HeroHitByEnemy() { mHero.HeroHit(); }
    private void EchoGameState()
    {
        mGameStateEcho.text = mWayPoints.GetWayPointState() + mHero.GetHeroState() + mEnemySystem.GetEnemyState();
    }
}