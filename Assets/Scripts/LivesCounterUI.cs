using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour {

	[SerializeField]
	private Text livesText;

	void Awake()
	{
		livesText = GetComponent<Text> ();

	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		livesText.text="Lives: "+GameMaster.RemaninLive.ToString();
	}
}
