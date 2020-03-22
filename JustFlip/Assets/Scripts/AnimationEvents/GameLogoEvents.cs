using UnityEngine;

public class GameLogoEvents : MonoBehaviour
{

	public GameObject crackedPlayButton ;
	public GameObject loopingSparkEffect ;
	public GameObject flipBlastSparkEffect ;
	public GameObject triangleBlastSparkEffect ;
	public GameObject resumeButton ;
	public GameObject restartButton ;
	public GameObject mainMenuButton ;
	public GameObject musicButton ;
	public GameObject fxButton ;
	public GameObject score ;
	public GameObject highestScore ;
	

	[HideInInspector] public static GameObject resumeButtonInstance ;
	[HideInInspector] public static GameObject restartButtonInstance ;
	[HideInInspector] public static GameObject mainMenuButtonInstance ;
	[HideInInspector] public static GameObject musicButtonInstance ;
	[HideInInspector] public static GameObject fxButtonInstance ;
	[HideInInspector] public static GameObject soundLogoInstance ;
	[HideInInspector] public static GameObject facebookLogoInstance ;
	[HideInInspector] public static GameObject twitterLogoInstance ;
	[HideInInspector] public static GameObject instagramLogoInstance ;
	[HideInInspector] public static GameObject scoreBoardLogoInstance ;
	[HideInInspector] public static GameObject empLogoInstance ;
	[HideInInspector] public static GameObject highestScoreInstance ;
	[HideInInspector] public static GameObject scoreInstance ;
	[HideInInspector] public static GameObject finalScoreText ;
	[HideInInspector] public static GameObject highestScoreText ;
	[HideInInspector] public static GameObject gameManager ;
	[HideInInspector] public static bool isFinalPanel ;
	

	private bool isUIObjectsCreated ;

	
	private void Start()
	{
		isUIObjectsCreated = false ;
	}


	public void CallShakeCamera()
    {
	    StartCoroutine(GameObject.Find("Camera").GetComponent<CameraShakeEffect>().ShakeCamera(5f,0.2f)) ;   
    }

	public void CreateCrackedPlayButton()
	{
		Instantiate(crackedPlayButton , Vector3.zero , Quaternion.identity) ;
	}

	public void CreateSparkAndPlayAnimation(string sparkType)
	{
		if (sparkType == "triangleOrange")
		{
			 GameObject temp = Instantiate(loopingSparkEffect , Vector3.zero , Quaternion.identity) ;
			temp.GetComponent<Animator>().SetTrigger("TriangleSparkEffect");
		}
		else if (sparkType == "flipBlast")
		{
			 Instantiate(flipBlastSparkEffect , flipBlastSparkEffect.transform.localPosition , Quaternion.identity) ;
		}
		else if (sparkType=="triangleBlast")
		{
			 Instantiate(triangleBlastSparkEffect , Vector3.zero , Quaternion.identity) ;
		}
		
	}
	


	public void CreatePauseUIs()
	{
		if (!isUIObjectsCreated)
		{
			if (isFinalPanel)
			{
				GameObject temp = Instantiate(restartButton , restartButton.transform.localPosition , Quaternion.identity) ;
				restartButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("FinalIn");
				temp = Instantiate(mainMenuButton , mainMenuButton.transform.localPosition , Quaternion.identity) ;
				mainMenuButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("FinalIn");
				temp = Instantiate(highestScore , highestScore.transform.localPosition , Quaternion.identity) ;
				highestScoreInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("FinalIn");
				temp = Instantiate(score , score.transform.localPosition , Quaternion.identity) ;
				scoreInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("FinalIn");
				temp = Instantiate(resumeButton , resumeButton.transform.localPosition , Quaternion.identity) ;
				resumeButtonInstance = temp ;
				temp.SetActive(false);
				temp = Instantiate(musicButton , musicButton.transform.localPosition , Quaternion.identity) ;
				musicButtonInstance = temp ;
				temp.SetActive(false);
				temp = Instantiate(fxButton , fxButton.transform.localPosition , Quaternion.identity) ;
				fxButtonInstance = temp ;
				temp.SetActive(false);
				GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 7 ;
				GameObject.Find("Canvas").transform.Find("Score").gameObject.SetActive(false);
				GameObject.Find("Canvas").transform.Find("RemainTime").gameObject.SetActive(false);
				highestScoreText = GameObject.Find("Canvas").transform.Find("HighestScore").gameObject;
				highestScoreText.SetActive(true);
				finalScoreText=GameObject.Find("Canvas").transform.Find("FinalScore").gameObject;
				finalScoreText.SetActive(true);
				isUIObjectsCreated = true ;
			}
			else
			{
				GameObject temp = Instantiate(resumeButton , resumeButton.transform.localPosition , Quaternion.identity) ;
				resumeButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("PauseIn");
				temp = Instantiate(restartButton , restartButton.transform.localPosition , Quaternion.identity) ;
				restartButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("PauseIn");
				temp = Instantiate(mainMenuButton , mainMenuButton.transform.localPosition , Quaternion.identity) ;
				mainMenuButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("PauseIn");
				temp = Instantiate(musicButton , musicButton.transform.localPosition , Quaternion.identity) ;
				musicButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("PauseIn");
				temp = Instantiate(fxButton , fxButton.transform.localPosition , Quaternion.identity) ;
				fxButtonInstance = temp ;
				temp.GetComponent<Animator>().SetTrigger("PauseIn");
				temp = Instantiate(highestScore , highestScore.transform.localPosition , Quaternion.identity) ;
				highestScoreInstance = temp ;
				temp.SetActive(false);
				temp = Instantiate(score , score.transform.localPosition , Quaternion.identity) ;
				scoreInstance = temp ;
				temp.SetActive(false);
				highestScoreText = GameObject.Find("Canvas").transform.Find("HighestScore").gameObject;
				finalScoreText = GameObject.Find("Canvas").transform.Find("FinalScore").gameObject;
				isUIObjectsCreated = true ;
			}
			
		}
		else
		{
			if (isFinalPanel)
			{
				restartButtonInstance.SetActive(true);
				mainMenuButtonInstance.SetActive(true);
				highestScoreInstance.SetActive(true);
				scoreInstance.SetActive(true);
				highestScoreText.SetActive(true);
				finalScoreText.SetActive(true);
				GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 7 ;
				GameObject.Find("Canvas").transform.Find("Score").gameObject.SetActive(false);
				GameObject.Find("Canvas").transform.Find("RemainTime").gameObject.SetActive(false);
				restartButtonInstance.GetComponent<Animator>().SetTrigger("FinalIn");
				mainMenuButtonInstance.GetComponent<Animator>().SetTrigger("FinalIn");
				highestScoreInstance.GetComponent<Animator>().SetTrigger("FinalIn");
				scoreInstance.GetComponent<Animator>().SetTrigger("FinalIn");
			}
			else
			{
				resumeButtonInstance.SetActive(true);
				restartButtonInstance.SetActive(true);
				mainMenuButtonInstance.SetActive(true);
				musicButtonInstance.SetActive(true);
				fxButtonInstance.SetActive(true);
				
				resumeButtonInstance.GetComponent<Animator>().SetTrigger("PauseIn");
				restartButtonInstance.GetComponent<Animator>().SetTrigger("PauseIn");
				mainMenuButtonInstance.GetComponent<Animator>().SetTrigger("PauseIn");
				musicButtonInstance.GetComponent<Animator>().SetTrigger("PauseIn");
				fxButtonInstance.GetComponent<Animator>().SetTrigger("PauseIn");
			}
			
		}
		
	}

	public void PlayPausePanelPOAnim()
	{
		GameObject.Find("PausePanel(Clone)").GetComponent<Animator>().SetTrigger("PauseOut");
	}

	public void PlayFx(string fxName)
	{
		GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySoundFX(fxName,1f) ;
	}
	public void PlayBackgroundMusic()
	{
		AudioManager.backgroundMusicActive = true ;
	}
	

}