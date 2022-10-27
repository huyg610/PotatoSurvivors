using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class EnemySpawnSystem : MonoBehaviour
{
    public CameraSupport mEnemyCam = null;
    public Text mCamLabel = null;

    private const int kMaxEnemy = 5;

    private int mTotalEnemy = 0;
    private GameObject mPatrolEnemy = null;
    private Vector2 mSpawnRegionMin, mSpawnRegionMax;

    private int mEnemyDestroyed = 0;

    private const float kLerpPeriod = 0f;
    private const float kLerpRate = 60f; // very quick

    void Start()
    {
        Debug.Assert(mEnemyCam != null);
        Debug.Assert(mCamLabel != null);

        mPatrolEnemy = Resources.Load<GameObject>("Prefabs/Enemy");
        GenerateEnemy();

        mEnemyCam.SetLerpParameters(kLerpPeriod, kLerpRate);
        DisableEnemyCam();
    }

    void Update()
    {
    }

    public void SetSpawnRegion(Vector2 min, Vector2 max)
    {
        mSpawnRegionMin = min;
        mSpawnRegionMax = max;
    }

    private void GenerateEnemy()
    {
        for (int i = mTotalEnemy; i < kMaxEnemy; i++)
        {
            GameObject p = Instantiate(mPatrolEnemy);
            float x = Random.Range(mSpawnRegionMin.x, mSpawnRegionMax.x);
            float y = Random.Range(mSpawnRegionMin.y, mSpawnRegionMax.y);
            p.transform.position = new Vector3(x, y, 0f);
            mTotalEnemy++;
        }
    }

    public void OneEnemyDestroyed() { mEnemyDestroyed++; ReplaceOneEnemy(); }
    public void ReplaceOneEnemy() { mTotalEnemy--; GenerateEnemy(); }
    public string GetEnemyState() { return "   Enemy: Count(" + mTotalEnemy + ") Destroyed(" + mEnemyDestroyed + ")"; }
}