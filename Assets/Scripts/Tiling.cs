using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offSetX = 2;

	public bool hasRightPart = false;
	public bool hasLeftPart = false;

	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;


	void Awake()
	{
		cam = Camera.main;
		myTransform = transform;	

	}


	// Use this for initialization
	void Start () {

		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;


	}
	
	// Update is called once per frame
	void Update () {
		if (hasLeftPart==false || hasRightPart==false) 
		{
			float camHorizontalExtends = cam.orthographicSize * Screen.width / Screen.height;

			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtends;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtends;

			if (cam.transform.position.x >= edgeVisiblePositionRight - offSetX && hasRightPart == false) 
			{
				
				addNewPart (1);
				hasRightPart = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offSetX && hasLeftPart == false) 
			{	
				addNewPart (-1);
				hasLeftPart = true;

			}

		}
	}

	void addNewPart(int leftORright)
	{
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * leftORright, myTransform.position.y, myTransform.position.z);

		Transform newPart = (Transform)Instantiate (myTransform, newPosition, myTransform.rotation);

		if (reverseScale == true) 
		{
			newPart.localScale = new Vector3 (newPart.localScale.x * -1, newPart.localScale.y, newPart.localScale.z);

		}

		newPart.parent = myTransform.parent;

		if (leftORright > 0) {
			newPart.GetComponent<Tiling>().hasLeftPart = true;
			} 
		else
		{
			newPart.GetComponent<Tiling>().hasRightPart = true;

		}
	}
}
