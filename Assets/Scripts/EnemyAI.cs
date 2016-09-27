using UnityEngine;
using System.Collections;
using Pathfinding;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]

public class EnemyAI : MonoBehaviour {

	public Transform target;
	//update rate
	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	//calculate path
	public Path path;

	public bool searchForPlayer=false;
	//ai speed	
	public float speed=300f;
	public ForceMode2D fmode;
	 

	[HideInInspector]
	public bool pathIsEnded = false;

	//max distance from ai to way point to  continue to next way point
	public float nextWayPointDistance = 3f;

	private int currentWayPoint=0;
	void Start()
	{
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) 
		{
			if (!searchForPlayer) {
				searchForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			
			}
			return;
		}


		//create path to target and return result to onpathcomplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);


		StartCoroutine (UpdatePath ());

	}

	IEnumerator SearchForPlayer()
	{
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if (sResult == null) {

			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		}
		else 
		{	//Debug.LogError ("we found the player");
			searchForPlayer = false;
			target = sResult.transform;
			StartCoroutine (UpdatePath ());
			//return false;
		}

	}


	IEnumerator UpdatePath()
	{
		//TODO : Insert Player Search Here


		if (target == null) 
		{
			if (!searchForPlayer) {
				searchForPlayer = true;
				StartCoroutine (SearchForPlayer ());

			}
			return false ;
		}

		//create path to target and return result to onpathcomplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);

		StartCoroutine (UpdatePath ());
	
	
	}
	public void OnPathComplete(Path p)
	{
		//Debug.Log ("We Got a Path ... btw did we have an error "+ p.error);

		if (!p.error) 
		{
			path = p;
			currentWayPoint = 0;

		}
	
	
	}


	void FixedUpdate()
	{	//print (GameObject.FindGameObjectWithTag ("Menu"));
		if (GameObject.FindGameObjectWithTag ("Menu") != null ) 
		{	target = null;
			return;	
		}
		if (target == null) 
		{
			if (!searchForPlayer) {
				searchForPlayer = true;
				StartCoroutine (SearchForPlayer ());

			}
			return;
		}

		if (path == null)
			return;
		if (currentWayPoint >= path.vectorPath.Count) 
		{
			if (pathIsEnded) {
				return;
			}

			//Debug.Log ("End of Path Reached ");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;


		Vector3 dir = (path.vectorPath [currentWayPoint] - transform.position).normalized;

		dir *= speed * Time.fixedDeltaTime;

		rb.AddForce (dir, fmode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWayPoint]);

		if (dist < nextWayPointDistance) 
		{
			currentWayPoint++;
			return;
		}
	}

}
