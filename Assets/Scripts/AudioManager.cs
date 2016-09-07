using UnityEngine;

[System.Serializable] 
public class Sound
{
	public string Name;
	public AudioClip clip;



	[Range(0f,1f)]
	public float Volume = 0.7f;
	[Range(0.5f,1.5f)]
	public float Pitch = 1f;

	[Range(0f,0.5f)]
	public float randomVolume = .1f;
	[Range(0f,0.5f)]
	public float randomPitch = 0.1f;

	private AudioSource source;
	public bool loopSound = false; 

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
		source.loop = loopSound;
	}


	public void Play()
	{	source.volume = Volume * (1 + Random.Range ( -randomVolume/2f , randomVolume/2f ) );
		source.pitch = Pitch * (1 + Random.Range ( -randomPitch/2f , randomPitch/2f ) );
		source.Play();
	}

	public void Stop()
	{	
		source.Stop();
	}
}

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;

	void Awake()
	{
		if (instance != null) {
			if (instance != this)
				Destroy (this.gameObject);
		} 
		else
		{
			instance = this;
			DontDestroyOnLoad (this);
		}

	}

	[SerializeField]
	Sound [] sound;

	void Start()
	{
		for (int i = 0; i < sound.Length; i++) 
		{
			GameObject _go = new GameObject ("Sound_" + i + "_" + sound [i].Name);
			_go.transform.SetParent (this.transform);
			sound [i].SetSource (_go.AddComponent<AudioSource> ());
		}
		PlaySound ("Music");
	}


	public void PlaySound(string _name)
	{
		for (int i = 0; i < sound.Length; i++) 
		{
			if (sound [i].Name == _name) 
			{
				sound [i].Play ();
				return;
			}
		}
		Debug.Log ("AudioManager Sound Not Found in PlaySound : " + _name);
	}



	public void StopSound(string _name)
	{
		for (int i = 0; i < sound.Length; i++) 
		{
			if (sound [i].Name == _name) 
			{
				sound [i].Stop();
				return;
			}
		}
		Debug.Log ("AudioManager Sound Not Found in PlaySound : " + _name);
	}


}

