using UnityEngine;
using System.Collections;

public class WaveSpawnner : MonoBehaviour {

	public enum SpawnState { SPAWING, WAITING , COUNTING };
	
	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform Enemy;
		public int count;	
		public float rate;
	}

	public int increaseInEnemy = 2;
	public float increaseInEnemySpeed = 0.6f;
	private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

	public Wave [] wave;
    public Transform[] SpawnPoints;

	private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave+1; }
    }

	public float timeBetweenWaves = 5f;

    private float searchCountDown = 1f;

	public int moneyDropIncreas = 5;
	public int enemyHealthIncrease = 50;
	public int enemyHealth_increaselvl = 50;
	public int damageIncrease = 15;
	[SerializeField]
	int damage_multiplier = 2; 
	private float WaveCountDown;
    public float waveCoutDown
    {
        get { return WaveCountDown; }
    }

	void Start()
	{	for (int i = 0; i < wave.Length; i++) 
		{	wave [i].Enemy.GetComponent <Enemy> ().moneyDrop = wave [i].Enemy.GetComponent <Enemy> ().gameStartMoney;
			wave [i].Enemy.GetComponent <Enemy> ().stats.damage = wave [i].Enemy.GetComponent <Enemy> ().stats.gameStartdamage;
			wave [i].Enemy.GetComponent <Enemy> ().stats.maxHealth = wave [i].Enemy.GetComponent <Enemy> ().stats.gameStartingHealth;
		}


        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("NO Spwan Points Found");
        }
        WaveCountDown = timeBetweenWaves;

	}


	void WaveCompleted()
	{
		Debug.Log("Wave Completed");

		state = SpawnState.COUNTING;
		WaveCountDown = timeBetweenWaves;

		if (nextWave + 1 > wave.Length - 1)
		{
			nextWave = 0;
			Debug.Log("ALL WAVE COMPLETED !!!! LOOPING TO START NOW !!!");
		}
		else
		{
			nextWave++;
		}
	}


	bool EnemyIsAlive()
	{
		searchCountDown -= Time.deltaTime;

		if (searchCountDown <= 0f)
		{
			// Debug.LogError("Searching Enemy Result " + GameObject.FindGameObjectWithTag("Enemy"));
			searchCountDown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				// Debug.LogError("All ENemy DIed");
				return false;
			}

		}

		return true;
	}


	void Update()
	{

		if (state == SpawnState.WAITING) 
		{
            if (!EnemyIsAlive() )
            {
               // Debug.LogError("Wave is COmpleted");
                WaveCompleted();

            }
            else
            {

                return;
            }
		}
		if (WaveCountDown <= 0) 
		{
			if (state != SpawnState.SPAWING) 
			{
				StartCoroutine( SpawnWave(wave[nextWave]));
				
			}
			
		}
        else
        {
            WaveCountDown -= Time.deltaTime;

        }
    }


	IEnumerator SpawnWave(Wave _wave)
	{	
        Debug.Log("Spawning Wave"+_wave.name);
		state = SpawnState.SPAWING;
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy (_wave.Enemy);
			yield return new WaitForSeconds (1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		_wave.count +=increaseInEnemy ;
		increaseInEnemy += 3;
		_wave.rate += increaseInEnemySpeed;
		_wave.Enemy.GetComponent <Enemy>().stats.maxHealth += enemyHealthIncrease;
		enemyHealthIncrease += enemyHealth_increaselvl;
		_wave.Enemy.GetComponent <Enemy>().stats.damage += damageIncrease;
		damageIncrease *= damage_multiplier;
		_wave.Enemy.GetComponent<Enemy> ().moneyDrop += moneyDropIncreas;

		yield break;
	}

    

   

	void SpawnEnemy(Transform _enemy)
	{   
        Transform _sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
		Debug.Log("Spawning Enemy : "+ _enemy.name);
	}
		
}
