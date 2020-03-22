using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip  mainMenuMusic ;
	public AudioClip  gamePlayMusic ;
	public AudioClip  gamePlayMusic2 ;
	public AudioClip  finalScoreMusic ;
	public AudioClip  backgroundMusic ;

	public AudioClip bgGamePlayTransitionEffect ;
	public AudioClip backLineEffect ;
	public AudioClip ballChangeEffect1 ;
	public AudioClip ballChangeEffect2 ;
	public AudioClip ballChangeEffect3 ;
	public AudioClip ballChangeEffect4 ;
	public AudioClip ballHitEffect ;
	public AudioClip ballDestroyEffect ;
	public AudioClip destroyModeStartEffect ;
	public AudioClip hittingScreenEffect ;
	public AudioClip justEffect ;
	public AudioClip obstacleDestroyEffect1 ;
	public AudioClip obstacleDestroyEffect2 ;
	public AudioClip obstacleDestroyEffect3 ;
	public AudioClip otherButtonClick ;
	public AudioClip pauseClick ;
	public AudioClip pbGamePlayTransitionEffect ;
	public AudioClip pbIntroHitEffect ;
	public AudioClip rectangleSparkEffect ;
	public AudioClip triangleSparkEffect ;

	public AudioSource[] fxs ;

	public static bool mainMenuMusicActive ;
	public static bool gamePlayMusicActive ;
	public static bool finalScoreMusicActive ;
	public static bool backgroundMusicActive ;
	public static bool stopMusicActive ;
	public static bool resumeMusicActive ;
	
	[HideInInspector] public static AudioSource railFrictionEffect ;

	private bool gameplay1 ;
	private byte fxOrderIndice ;
	private byte ballChangeOrder ;
	private byte obstacleDestroyOrder ;
	private AudioSource tempAS ;

	private void Start()
	{
		gameplay1 = true ;
		fxOrderIndice = 0 ;
		ballChangeOrder = 1 ;
		obstacleDestroyOrder = 1 ;
		railFrictionEffect = transform.Find("RailFrictionEffect").GetComponent<AudioSource>() ;

		if (PlayerPrefs.HasKey("FX"))
		{
			if (PlayerPrefs.GetInt("FX") == 0)
			{
				for (byte i = 1; i < 7; i++)
				{
					transform.Find("fx"+i).gameObject.SetActive(false);
				}
				transform.Find("RailFrictionEffect").gameObject.SetActive(false);
			}
		}
		
		if (PlayerPrefs.HasKey("Music"))
		{
			if (PlayerPrefs.GetInt("Music") == 0)
			{
				transform.Find("Music").gameObject.SetActive(false);
			}
		}
		
		StartCoroutine(MusicController()) ;
		
	}

	private IEnumerator MusicController()
	{
		while (true)
		{
			if (mainMenuMusicActive)
			{
				StartCoroutine(playMusic("mainmenu")) ;
				mainMenuMusicActive = false ;
			}

			if (gamePlayMusicActive)
			{
				StartCoroutine(playMusic("gameplay")) ;
				gamePlayMusicActive = false ;
			}

			if (finalScoreMusicActive)
			{
				StartCoroutine(playMusic("finalscore")) ;
				finalScoreMusicActive = false ;
			}

			if (stopMusicActive)
			{
				StartCoroutine(StopMusic()) ;
				stopMusicActive = false ;
			}

			if (resumeMusicActive)
			{
				StartCoroutine(ResumeMusic()) ;
				resumeMusicActive = false ;
			}

			if (backgroundMusicActive)
			{
				StartCoroutine(playMusic("background")) ;
				backgroundMusicActive = false ;
			}

			yield return null ;
		}
		
		
		
	}

	public IEnumerator playMusic(string musicName)
	{
		AudioSource temp ;
		temp = transform.Find("Music").GetComponent<AudioSource>() ;
		temp.volume = 0 ;

		if (musicName == "gameplay")
		{
		
			if (gameplay1)
			{
				temp.clip = gamePlayMusic ;
				gameplay1 = false ;
			}
			else
			{
				temp.clip = gamePlayMusic2 ;
				gameplay1 = true ;
			}
		}
		else if (musicName == "mainmenu")
		{
			temp.clip = mainMenuMusic ;
		}
		else if (musicName == "finalscore")
		{
			temp.clip = finalScoreMusic ;
		}
		else if (musicName == "background")
		{
			temp.clip = backgroundMusic ;
			temp.Play();

			while (temp.volume<0.6f)
			{
				temp.volume += 2.5f * Time.unscaledDeltaTime ;
                yield return null ;
			}

			temp.volume = 0.6f ;
			
			yield break;
		}
		
		temp.Play();
		
		while (temp.volume<1)
		{
			temp.volume += 3f * Time.unscaledDeltaTime ;
			yield return null ;
		}

		temp.volume = 1 ;
		
	}
	
	public IEnumerator StopMusic()
	{
		AudioSource temp ;
		temp = transform.Find("Music").GetComponent<AudioSource>() ;
		while (temp.volume>0)
		{
			temp.volume -= 3f * Time.unscaledDeltaTime ;
			yield return null ;
		}

		temp.volume = 0 ;
	}
	
	public IEnumerator ResumeMusic()
	{
		AudioSource temp ;
		temp = transform.Find("Music").GetComponent<AudioSource>() ;
		while (temp.volume<1)
		{
			temp.volume += 3f * Time.unscaledDeltaTime ;
			yield return null ;
		}

		temp.volume = 1 ;
	}


	public void PlaySoundFX(string fxName,float volume)
	{
		switch (fxName)
		{
			
			case "BGGPTransitionEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = bgGamePlayTransitionEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "BackLineEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = backLineEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1f ;
				tempAS.Play();
				break;
			
			case "BallChangeEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.time = 0.1f ;
				tempAS.volume = volume ;
				switch (GetRandomNumberForBallChangeEffect())
				{
						case 1 :
							tempAS.clip = ballChangeEffect1 ;
							tempAS.Play();
							break;
						case 2:
							tempAS.clip = ballChangeEffect2 ;
							tempAS.Play();
							break;
						case 3:
							tempAS.clip = ballChangeEffect3 ;
							tempAS.Play();
							break;
						case 4:
							tempAS.clip = ballChangeEffect4 ;
							tempAS.Play();
							break;	
				}
				
				break;
			
			case "BallHitEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = ballHitEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 0.7f ;
				tempAS.Play();
				break;
			
			case "BallDestroyEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = ballDestroyEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "DestroyModeStartEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = destroyModeStartEffect ;
				tempAS.time = 0.1f ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "HittingScreenEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = hittingScreenEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "JustEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.volume = 1 ;
				tempAS.clip = justEffect ;
				tempAS.time = 0 ;
				tempAS.Play();
				break;
			
			case "ObstacleDestroyEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.time = 0.1f ;
				tempAS.volume = 1 ;
				switch (GetRandomNumberForObstacleDestroyEffect())
				{
						case 1:
							tempAS.clip = obstacleDestroyEffect1 ;
							tempAS.Play();
							break;
						case 2:
							tempAS.clip = obstacleDestroyEffect2 ;
							tempAS.Play();
							break;
						case 3:
							tempAS.clip = obstacleDestroyEffect3 ;
							tempAS.Play();
							break;
				}
				
				break;
			
			case "OtherButtonClick":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = otherButtonClick ;
				tempAS.time = 0f ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "PauseClick":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = pauseClick ;
				tempAS.time = 0f ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "PBGPTransitionEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = pbGamePlayTransitionEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "PBIntroHitEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = pbIntroHitEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "RectangleSparkEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = rectangleSparkEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
			
			case "TriangleSparkEffect":
				tempAS = GetAvailableAudioSource() ;
				tempAS.clip = triangleSparkEffect ;
				tempAS.time = 0 ;
				tempAS.volume = 1 ;
				tempAS.Play();
				break;
				
		}
		
	}

	private AudioSource GetAvailableAudioSource()
	{
		if (fxOrderIndice<6)
		{
			fxOrderIndice++ ;
			return fxs[fxOrderIndice-1] ;
			
		}
		
			fxOrderIndice = 0 ;
		    return fxs[fxOrderIndice] ;

	}

	private byte GetRandomNumberForBallChangeEffect()
	{
		if (ballChangeOrder<4)
		{
			ballChangeOrder++ ;
			return (byte)(ballChangeOrder-1) ;
		}
		
		
			ballChangeOrder = 1 ;
			return 4 ;
		
	}
	
	private byte GetRandomNumberForObstacleDestroyEffect()
	{
		if (obstacleDestroyOrder<3)
		{
			obstacleDestroyOrder++ ;
			return (byte)(obstacleDestroyOrder-1) ;
		}
		
			obstacleDestroyOrder = 1 ;
			return 3 ;
		
	}
	
	
	
	
}
