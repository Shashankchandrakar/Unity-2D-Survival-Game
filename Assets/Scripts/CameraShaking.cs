using UnityEngine;
using System.Collections;

public class CameraShaking : MonoBehaviour {

	public Camera mainCam;

	float shakeAmount = 0;

	void Start()
	{
		//Debug.LogError ("HERE");
	}
	void Awake()
	{
		if (mainCam == null) 
		{
			mainCam = Camera.main;
		}
	}

	void Update()
	{ 
		if (Input.GetKeyDown (KeyCode.T)) 
		{
			Shake (0.1f, 0.2f);
		}
	}


	public void Shake(float amt, float length)
	{
		shakeAmount = amt;
		InvokeRepeating ("BeginSHake", 0, 0.01f);
		Invoke ("StopShake", length);
	}


	void BeginSHake()
	{

		if (shakeAmount > 0) 
		{	
			Vector3 camPos = mainCam.transform.position;
			float offSetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offSetY = Random.value * shakeAmount * 2 - shakeAmount;

			camPos.x += offSetX;
			camPos.y += offSetY;

			mainCam.transform.position = camPos;

		}
		
	}

	void StopShake()
	{
		CancelInvoke ("BeginSHake");
		mainCam.transform.localPosition = Vector3.zero;
	}

}
