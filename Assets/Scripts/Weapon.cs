using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;

	public LayerMask whatToHit;
	public float effectSpawnRate = 10f;
	public Transform BulletPrefab;
	public Transform HitPrefab;
	public Transform MuzzleFlashPrefab;
	private float timeToSpawnEffect=0f;


	public float camShakeAmt= 0.05f;
	public float camShakeLenght=0.1f;

	CameraShaking camShake;

	public string weaponShootSound = "DefaultShot";

	float timeToFire = 0;
	Transform firePoint;

	//caching
	AudioManager audiomanager;


	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");

		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}

	void Start()
	{
		camShake = GameMaster.gm.GetComponent<CameraShaking> ();



		if (camShake == null)
			Debug.LogError ("No CameraShake Object Found in GM  ");
		audiomanager = AudioManager.instance;
		if (audiomanager == null)
			Debug.LogError ("No Audio Manager Found");
	}


	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot();
			}
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);


		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);


			Enemy enemy=hit.collider.GetComponent<Enemy>();

			if (enemy != null) 
			{
				enemy.DamageEnemy(PlayerStats.instance.Damage);
				//Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
			}

		}
		if(Time.time >= timeToSpawnEffect)
		{
			Vector3 hitPos;
			Vector3 hitNormal;

			if (hit.collider == null) {
				hitPos = (mousePosition - firePointPosition) * 30;
				hitNormal = new Vector3 (999, 999, 999);
			}
			else 
			{
				hitPos = hit.point;
				hitNormal = hit.normal;
			
			}
			Effect (hitPos,hitNormal);
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}

	}

	void Effect(Vector3 hitpos,Vector3 hitNormal)
	{
		Transform trail=Instantiate (BulletPrefab,firePoint.position,firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer> ();
		if (lr != null) 
		{
			lr.SetPosition (0, firePoint.position);
			lr.SetPosition (1, hitpos);
		}
		//Transform fix = firePoint;
		//fix.position.x += 3;
		Destroy (trail.gameObject, 0.06f);


		if (hitNormal != new Vector3 (999, 999, 999)) 
		{
			Transform hitparticle=(Transform)Instantiate(HitPrefab,hitpos,Quaternion.FromToRotation(Vector3.right,hitNormal));
			Destroy (hitparticle.gameObject, 1f);
		}
		Transform clone=(Transform)Instantiate (MuzzleFlashPrefab,firePoint.position,firePoint.rotation);	
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);

		camShake.Shake (camShakeAmt, camShakeLenght);

		//play shoot sound
		audiomanager.PlaySound(weaponShootSound);
	}


}
