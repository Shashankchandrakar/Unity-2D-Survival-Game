using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {
    [SerializeField]
    WaveSpawnner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text WaveCountdownText;

    [SerializeField]
    Text WaveCountText;


    private WaveSpawnner.SpawnState prevstate;
	// Use this for initialization
	void Start () {
        if (spawner == null)
        { Debug.LogError("No Spawnner Reference");
            this.enabled = false;
            
        }

        if (waveAnimator == null)
        {
            Debug.LogError("No WaveAnimator Reference");
            this.enabled = false;

        }

        if (WaveCountdownText == null)
        {
            Debug.LogError("No WaveCountdownText Reference");
            this.enabled = false;

        }

        if (WaveCountText == null)
        {
            Debug.LogError("No WaveCountText Reference");
            this.enabled = false;

        }



    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(spawner.State)
        {
            case WaveSpawnner.SpawnState.COUNTING:
                UpdateCountDownUI();
                break;
            case WaveSpawnner.SpawnState.SPAWING:
               UpdateSpawningUI();
               break;
        }
        prevstate = spawner.State;
	}


    void UpdateCountDownUI()
    {   if (prevstate != WaveSpawnner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
           // Debug.Log("Counting");
        }
        WaveCountdownText.text = ((int)spawner.waveCoutDown).ToString();
    }

    void UpdateSpawningUI()
    {   if (prevstate != WaveSpawnner.SpawnState.SPAWING)
        {
            waveAnimator.SetBool("WaveIncoming", true);
            waveAnimator.SetBool("WaveCountdown",false);
            WaveCountText.text = spawner.NextWave.ToString();
            //Debug.Log("Spawning UI");
        }
    }
}
