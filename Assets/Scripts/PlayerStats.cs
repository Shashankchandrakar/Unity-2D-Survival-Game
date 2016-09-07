using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public static PlayerStats instance;
	public int maxHealth = 100;
	public float increaseInRateAfterUpgrade = 0.25f;
	private int _curHealth;
	public float movementSpeed = 10f;
	public float healthRegenRate = 2f;
	public int Damage = 50;

	public int curHealth
	{
		get { return _curHealth; }
		set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
	}
	 void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
		curHealth = maxHealth;
	}

}


