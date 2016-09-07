using UnityEngine;
using System.Collections;

public class Parallexing : MonoBehaviour {

	public Transform[] backGround;
	private float[] parallexScales;
	public float smoothnes=1f;

	private Transform cam;
	private Vector3 previousCamPosition;


	// Use this for initialization
	void Awake () {
		cam = Camera.main.transform;
	}

	void Start()
	{
		previousCamPosition = cam.position;
		parallexScales = new float[backGround.Length];

		for (int i = 0; i < backGround.Length; i++) 
		{
			parallexScales [i] = backGround [i].position.z * -1;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
		for (int i = 0; i < backGround.Length; i++) 
		{
			float parallex = (previousCamPosition.x - cam.position.x) * parallexScales [i];
			//float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];


			float backGroundTargetPosX = backGround [i].position.x + parallex;

			Vector3 backGroundTargetPos = new Vector3 (backGroundTargetPosX, backGround [i].position.y, backGround [i].position.z);

			backGround [i].position = Vector3.Lerp (backGround [i].position, backGroundTargetPos, smoothnes * Time.deltaTime);

		}
		previousCamPosition = cam.position;
	}
}
