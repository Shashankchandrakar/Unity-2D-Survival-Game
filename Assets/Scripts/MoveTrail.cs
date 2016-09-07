using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

	// Use this for initialization
	public int moveSpeed=230;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (this.gameObject, 1f);
	}
}
