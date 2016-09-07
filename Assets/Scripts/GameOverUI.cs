using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour {

	[SerializeField]
	string onMouseHove = "ButtonHover";

	[SerializeField]
	string buttonPressSound = "ButtonPress";

	[SerializeField]
	string gameOverSound = "GameOver";
	AudioManager audioManager;

	void Start()
	{
		audioManager = AudioManager.instance;
		if (audioManager == null)
			Debug.LogError ("No Audio Manger For GameOver");
		audioManager.PlaySound (gameOverSound);
	}
	public void Quit()
	{	
		audioManager.PlaySound (buttonPressSound);
		Debug.Log ("Apllication Quit");
		Application.Quit();
	}


	public void Retry()
	{	
		audioManager.PlaySound (buttonPressSound);
		//Application.LoadLevel (Application.loadedLevel);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}


	public void OnMouseHover()
	{
		audioManager.PlaySound (onMouseHove);
	}
}
