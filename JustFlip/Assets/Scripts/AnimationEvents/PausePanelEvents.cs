using UnityEngine;

public class PausePanelEvents : MonoBehaviour
{

	public GameObject panelBlastEffect ;
	public GameObject rectangleLoopingSparkEffect ;
	public GameObject playButton ;

	
	[HideInInspector] public static GameObject gameManager ;
	[HideInInspector] public static GameObject pauseButton ;
	[HideInInspector] public static GameObject remainTimeLogo ;
	[HideInInspector] public static string buttonName ;
	[HideInInspector] public static bool isPaused ;


	public void CreatePanelBlastEffect()
	{
		Instantiate(panelBlastEffect , panelBlastEffect.transform.localPosition , Quaternion.identity) ;
	}

	public void CreateAndPlayRectangleLoopingSparkEffect()
	{
		GameObject temp = Instantiate(rectangleLoopingSparkEffect ,
			rectangleLoopingSparkEffect.transform.localPosition , Quaternion.identity) ;
		temp.GetComponent<Animator>().SetTrigger("RectangleSparkEffect");
	}
	public void ShakeCamera()
	{
		Animator temp = GameObject.Find("Camera").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("ShakeCamera");

	}

	public void PlayGameLogoAnim()
	{
		GameObject.Find("GameLogo(Clone)").GetComponent<Animator>().SetTrigger("PauseIn");
	}

	public void TimeScaleAnim(float decreaseAmount)
	{
		if (Time.timeScale>0)
		{
			Time.timeScale -= decreaseAmount ;
		}
		else
		{
			Time.timeScale = 0 ;
		}
		
	}

	public void SetTheTimeScaleOne()
	{
		Time.timeScale = 1 ;
	}
	public void SetTheTimeScaleZero()
	{
		Time.timeScale = 0 ;
	}

	public void SetTruePauseButton()
	{
		if (buttonName!="MainMenu")
		{
			pauseButton.SetActive(true);
		}
	}

	public void SetFalseRTAndScore()
	{
		if (PausePanelEvents.buttonName == "MainMenu")
		{
			GameObject.Find("RemainTime(Clone)").SetActive(false);
			GameObject.Find("Canvas").transform.Find("Score").gameObject.SetActive(false) ;
			GameObject.Find("Canvas").transform.Find("RemainTime").gameObject.SetActive(false);
		}
	}

	public void SetActiveLinesColliders()
	{
		if (buttonName != "MainMenu")
		{
			GameObject temp = GameObject.Find("Lines(Clone)");
			for (int i = 0; i < 5; i++)
			{
				temp.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled=true ;
			}
		}
	}

	public void SetFalseCameraAnimator()
	{
		GameObject.Find("Camera").GetComponent<Animator>().enabled = false ;
	}

	public void SetActiveGameManagerAndGameBall()
	{
		if (buttonName!="MainMenu")
		{
			if (GameObject.Find("GameManager(Clone)") == null)
			{
				gameManager.SetActive(true);
			}

			if (GameObject.Find("GameBall(Clone)") == null)
			{
				Obstacle.gameBall.SetActive(true);
			}

		}
			
	}
	
	

	public void SetReadyMainMenu()
	{
		
		if (buttonName == "MainMenu")
		{
			GameObject temp = GameObject.Find("GameLogo(Clone)");
		    temp.GetComponent<Animator>().Play("GameLogoIntro",0,(1f/9.49f)*9.48f);
			temp = GameObject.Find("FacebookLogo(Clone)");
			temp.GetComponent<Animator>().Play("FacebookLogoIntroAnim");
			temp = GameObject.Find("TwitterLogo(Clone)");
			temp.GetComponent<Animator>().Play("TwitterLogoIntroAnim");
			temp = GameObject.Find("InstagramLogo(Clone)");
			temp.GetComponent<Animator>().Play("InstagramLogoIntroAnim");
			temp = GameObject.Find("SoundLogo(Clone)");
			temp.GetComponent<Animator>().Play("SoundLogoIntroAnim");
			temp = GameObject.Find("ScoreBoardLogo(Clone)");
			temp.GetComponent<Animator>().Play("ScoreBoardIntroAnim");
			temp = GameObject.Find("EMPLogo(Clone)");
			temp.GetComponent<Animator>().Play("EMPLogoIntroAnim");
			temp = Instantiate(playButton , playButton.transform.localPosition , Quaternion.identity) ;
			temp.GetComponent<PlayButtonEvents>().isThisFirstPlayButton = false ;
			temp.GetComponent<CircleCollider2D>().enabled = false ;
			temp.GetComponent<Animator>().Play("PlayButtonIntro",0,(1f/3.26f)*1.20f);
		}

	}

	public void SetFalseYourSelf()
	{
		gameObject.SetActive(false);
	}

	private void GetHighestScoreForPausePanel()
	{
		if (!isPaused)
		{
			ScoreHandler.getScore = true ;
		}
	}

	public void ResumeOrPlayMusic()
	{
		if (buttonName == "Resume")
		{
			AudioManager.resumeMusicActive = true ;
			
		}
		else if(buttonName == "Restart")
		{
			AudioManager.gamePlayMusicActive = true ;
		}
		
		isPaused = false ;

	}

	public void PlayRailFrictionEffect()
	{
		if (buttonName != "MainMenu")
		{
			AudioManager.railFrictionEffect.Play();
		}
		
	}

	public void StopRailFrictionEffect()
	{
		AudioManager.railFrictionEffect.Stop();
	}

	public void PlayFinalScoreMusic()
	{
		if (!isPaused)
		{
			AudioManager.finalScoreMusicActive = true ;
		}
	}

	

	public void PlayBackgroundMusic()
	{
		if (isPaused)
		{
			AudioManager.backgroundMusicActive = true ;
		}
	}

	public void StopMusic()
	{
		AudioManager.stopMusicActive = true ;
	}
	
	public void PlayFx(string fxName)
	{
		GameManager.audioManager.PlaySoundFX(fxName,1f) ;
	}
	
}
