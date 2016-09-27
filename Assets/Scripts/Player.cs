using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour {
	

	public PlayerStats stats;

	public int fallBoundary = -20;


	public string deathSoundName = "DeathVoice";
	public string damageSoundName = "Grunt";

	AudioManager audioManager;

	[SerializeField]
	private StatusIndicator statusIndicator;

	void Start()
	{
		//stats.Init();
		stats = PlayerStats.instance;
		stats.curHealth = stats.maxHealth ;
		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}

		GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToogle;
		audioManager = AudioManager.instance;
		if (audioManager == null)
			Debug.LogError ("No audio manager for player");

		InvokeRepeating ("RegenHealth", 1f/stats.healthRegenRate,1f/ stats.healthRegenRate);
	}

	void Update () {
		//InvokeRepeating ("RegenHealth", 1f/stats.healthRegenRate,1f/ stats.healthRegenRate);
		if (GameObject.FindGameObjectWithTag ("Menu") != null) {
			
		}
		if (transform.position.y <= fallBoundary)
			DamagePlayer (9999999);
	}

	public void DamagePlayer (int damage) {
		try
		{stats.curHealth -= damage;}
		catch(System.Exception e) {
			print (e);
			print ("I KNOW ABOUT THIS ERROR  IT HAPPENS COZ PLAYER IS INVINCIBLE !!!");
		}
		try{
			if (stats.curHealth <= 0) 
			{	
				//play death sound
				audioManager.PlaySound(deathSoundName);
				//kill player
				GameMaster.KillPlayer (this);
			}
			else
			{
				//play damage sound
				audioManager.PlaySound(damageSoundName);
			}
		}
		catch(System.Exception e)
		{	print (e);
			print ("YAY I KNOW ABOUT THIS ERROR  IT HAPPENS COZ PLAYER IS INVINCIBLE !!!");
		}
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

	void OnUpgradeMenuToogle(bool active)
	{	CancelInvoke ();
		InvokeRepeating ("RegenHealth", 1f/stats.healthRegenRate,1f/ stats.healthRegenRate);
		//handle what happen upgrade menu is toggled
		GetComponent<Platformer2DUserControl>().enabled = !active;
		Weapon _weapon = GetComponentInChildren <Weapon> ();
		if (_weapon == null) {
			Debug.Log ("Cant Find Weapon Reference in Player for upgrade menu ");
		}
		else if (_weapon != null) 
		{
			_weapon.enabled = !active;
		}

	}

	void RegenHealth()
	{	
		stats.curHealth = (int) (stats.curHealth+ stats.healthRegenRate);
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

	void OnDestroy()
	{
		GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToogle;
	}
}
