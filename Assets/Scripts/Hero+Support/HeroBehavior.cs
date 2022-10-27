using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    public CameraSupport mHeroCam = null;
    public EggSpawnSystem mEggSystem = null;
    private const float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds

    private Vector2 kShakeSize = new Vector2(1, 1);
    private float kCamLerpDuration = 0.5f; // short
    private float kCamLerpRate = 8f;  // 8 units per second, much faster

    //  Hero state
    private int mHeroHit = 0;
    public void HeroHit() { mHeroHit++; }
    public string GetHeroState() { return "   Hero: Hit(" + mHeroHit + ") EggCount(" + EggsOnScreen() + ")"; }

    private void Awake()
    {
        // Actually since Hero spwans eggs, this can be done in the Start() function, but, 
        // just to show this can also be done here.
        Debug.Assert(mEggSystem != null);
        EggBehavior.InitializeEggSystem(mEggSystem);

        Debug.Assert(mHeroCam != null);
        mHeroCam.SetLerpParameters(kCamLerpDuration, kCamLerpRate);
    }

    void Start ()
    { 
    }
	
	// Update is called once per frame
	void Update () {
        mHeroCam.MoveTo(transform.position.x, transform.position.y);
        UpdateMotion();
        ProcessEggSpwan();
    }

    private int EggsOnScreen() { return mEggSystem.GetEggCount();  }

    private void UpdateMotion()
    {
        // Only support rotation
        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") *
                                    (kHeroRotateSpeed * Time.smoothDeltaTime));
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        transform.position = p;
    }

    private void ProcessEggSpwan()
    {
        if (mEggSystem.CanSpawn())
        {
            if (Input.GetKey("space"))
            {
                mEggSystem.SpawnAnEgg(transform.position, transform.up);
                mHeroCam.ShakeCamera(kShakeSize);
            }
        }
    }
}