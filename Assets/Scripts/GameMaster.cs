using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	//public static GameMaster instance;

	private static int _remaninlives = 3;

	[SerializeField]
	private int maxLives = 3;

	[SerializeField]
	private GameObject upgradeMenu;

	//catch
	private AudioManager audioManager;

	public KeyCode upgradeButton;

	public delegate void UpgradeMenuCallBack(bool active);

	public UpgradeMenuCallBack onToggleUpgradeMenu ;

	public static int Money ;

	[SerializeField]
	private int StartingMoney;

	public static int RemaninLive
	{
		get { return _remaninlives; }
	}
	void Awake ()
	{
		
		
		if (gm == null) {
			//Debug.LogError ("inside Awake : " +GameObject.FindGameObjectWithTag("GM"));
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
			//gm = this;
		}
	
	}

	private void ToggleUpgradeMenu()
	{
		upgradeMenu.SetActive (! upgradeMenu.activeSelf);
		wave.enabled = !upgradeMenu.activeSelf;
		onToggleUpgradeMenu(upgradeMenu.activeSelf);
	}


	void Update()
	{
		if (Input.GetKeyDown (upgradeButton)) 
		{
			ToggleUpgradeMenu ();
		}
	}



	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2f;
	public CameraShaking camShake;
	public Transform spawnPrefab;

	public string respawnCountdownSound = "ReSpawnCountdown";
	public string spawnSoundName = "Spawn";


	[SerializeField]
	private GameObject gameOverUI;
	//public Transform enemyDeathParticles;

	[SerializeField]
	private WaveSpawnner wave;

	void Start()
	{	
		if (gm == null) {
			//Debug.LogError ("inside Start");

			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
		_remaninlives = maxLives;
		Money = StartingMoney; 
		//print ("SOMETHING HAPPENED : "+ spawnSoundName);

		if (camShake == null)
			Debug.LogError ("NO camera shake found in gamemaster");


		audioManager = AudioManager.instance;
		if (audioManager == null)
			Debug.LogError ("No Audio Manager Found in Scene");
	}


	public IEnumerator RespawnPlayer ()
	{
		audioManager.PlaySound (respawnCountdownSound);

		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		audioManager.PlaySound (spawnSoundName);
		Transform spawnParticlesClone= Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
		Destroy (spawnParticlesClone.gameObject,2f);

		GameObject play = GameObject.FindGameObjectWithTag ("Player");
		play.GetComponent<Player> ().enabled = false;

		Animator anim = play.GetComponent<Animator> ();
		anim.SetBool ("Invincible", true);
		yield return new WaitForSeconds (4f);
		play.GetComponent<Player> ().enabled = true;
		anim.SetBool ("Invincible", false);

	}



	public static void KillPlayer (Player player) 
	{
		Destroy (player.gameObject);
		_remaninlives -= 1;
		if(_remaninlives <= 0)
		{
			gm.EndGame();
		}
		else
		{
			gm.StartCoroutine(gm.RespawnPlayer());
		}

	}


	public void EndGame()
	{
		Debug.Log("GAME OVER !!!");
		gameOverUI.SetActive (true); 
	}

	public static void KillEnemy (Enemy enemy) 
	{	Money += enemy.moneyDrop;
		AudioManager.instance.PlaySound ("Money");
		gm._KillEnemy (enemy);
		//gm.StartCoroutine (gm.RespawnPlayer());
	}


	public void _KillEnemy(Enemy _enemy)
	{	//death sound
		audioManager.PlaySound(_enemy.deathSoundName);

		//add particles
		Transform _clon=(Transform)Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
		Destroy (_clon.gameObject, 3f);
		//cam shake
		camShake.Shake(_enemy.shakeAmt,_enemy.shakeLength);
		//
		Destroy (_enemy.gameObject);
	}

}