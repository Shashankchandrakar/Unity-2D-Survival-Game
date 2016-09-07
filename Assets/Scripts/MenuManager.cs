using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	AudioManager audioManager;
	[SerializeField]
	string hoverOverSound = "ButtonHover";

	[SerializeField]
	string pressButtonSound = "ButtonPress";

	void Start()
	{	
		audioManager = AudioManager.instance;
		if (audioManager == null) 
		{
			Debug.LogError ("No Audio Manager Found ");
		}
	}


	public void StartGame()
	{	audioManager.PlaySound (pressButtonSound);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}


	public void QuitGame()
	{	audioManager.PlaySound (pressButtonSound);
		Debug.Log ("We Quit The Game");
		Application.Quit ();
	}

	public void OnMouseOver()
	{	
		audioManager.PlaySound (hoverOverSound);
	}


}
