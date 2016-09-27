using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {
	[SerializeField]
	private Text healthText;

	[SerializeField]
	private Text speedText;

	[SerializeField]
	private Text damageText;

	private PlayerStats stats;

	[SerializeField]
	private int healthIncrease = 50;

	[SerializeField]
	private int speedIncrease = 1;

	[SerializeField]
	public int healthUpgradeCost = 50;

	[SerializeField]
	public int speedUpgradeCost = 50;

	[SerializeField]
	public int damageUpgradeCost = 50;

	[SerializeField]
	private int damageIncrease = 5;

	[SerializeField]
	private Text healthUpgradeButton;

	[SerializeField]
	private Text speedUpgradeButton;

	[SerializeField]
	private Text damageUpgradeButton;

	[SerializeField]
	private int Increase_COST_UPGRADE = 10;
	void OnEnable()
	{	stats = PlayerStats.instance;
		UpdateValues ();

		//Time.timeScale = 0.0f;
	}
	void OnDisable()
	{

		//Time.timeScale = 1.0f;
	}
	void UpdateValues()
	{
		healthText.text ="HEALTH: "+ stats.maxHealth.ToString ();
		speedText.text = "SPEED: "+stats.movementSpeed.ToString () ;
		damageText.text = "DAMAGE: "+stats.Damage.ToString ();
		healthUpgradeButton.text = "UPGRADE(" + healthUpgradeCost.ToString () + " $)";
		damageUpgradeButton.text = "UPGRADE(" + damageUpgradeCost.ToString () + " $)";
		speedUpgradeButton.text = "UPGRADE(" + speedUpgradeCost.ToString () + " $)";
	}


	public void UpgradeHealth()
	{	
		if (GameMaster.Money < healthUpgradeCost) 
		{	//TODO:add no money thing 
			AudioManager.instance.PlaySound ("NoMoney");
			return;
		}
		stats.healthRegenRate += stats.increaseInRateAfterUpgrade;
		stats.maxHealth += healthIncrease;
		AudioManager.instance.PlaySound ("Money");
		GameMaster.Money -= healthUpgradeCost;
		healthUpgradeCost +=Increase_COST_UPGRADE ;
		UpdateValues ();
	}


	public void UpgradeDamage()
	{

		if (GameMaster.Money < damageUpgradeCost) 
		{	//TODO:add no money thing 
			AudioManager.instance.PlaySound ("NoMoney");
			return;
		}
		stats.Damage += damageIncrease;
		AudioManager.instance.PlaySound ("Money");
		GameMaster.Money -= damageUpgradeCost;
		damageUpgradeCost += 10;
		UpdateValues ();
	}

	public void UpgradeSpeed()
	{	if (GameMaster.Money < speedUpgradeCost) 
		{	//TODO:add no money thing 
			AudioManager.instance.PlaySound ("NoMoney");
			return;
		}
		stats.movementSpeed += speedIncrease;
		GameMaster.Money -= speedUpgradeCost;
		AudioManager.instance.PlaySound ("Money");
		speedUpgradeCost += 10;
		UpdateValues ();
	}


}
