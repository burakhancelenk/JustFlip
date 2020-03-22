using UnityEngine;

public class PauseButtonTouchInput : MonoBehaviour
{
	public GameObject pausePanel ;

	[HideInInspector] public static bool isPausePanelCreated ;
	[HideInInspector] public static GameObject pausePanelInstance ;

	private void Start()
	{
		isPausePanelCreated = false ;
	}


	private void OnMouseDown()
	{
		GameLogoEvents.isFinalPanel = false ;
		CreatePausePanel();
		SetFalseLineColliders();
		RestartButtonTouchInput.gameBallCurrentPos = GameBall.currentPositionIndice ;
		RestartButtonTouchInput.gameBallDest = GameBall.destination ;
		PausePanelEvents.isPaused = true ;
		AudioManager.railFrictionEffect.Stop();
		GameManager.audioManager.PlaySoundFX("PauseClick",1f);
		gameObject.SetActive(false);
	}
	
	

	public void CreatePausePanel()
	{
		
		
		if (!isPausePanelCreated)
		{
			pausePanelInstance = Instantiate(pausePanel , pausePanel.transform.localPosition , Quaternion.identity) ;
			pausePanelInstance.GetComponent<Animator>().SetTrigger("PauseIn");
			isPausePanelCreated = true ;
		}
		else
		{
			pausePanelInstance.SetActive(true);
			pausePanelInstance.GetComponent<Animator>().SetTrigger("PauseIn");
		}
		
		
	}

	public void SetFalseLineColliders()
	{
		GameObject temp = GameObject.Find("Lines(Clone)");
		
		for (int i = 0; i < 5; i++)
		{
			temp.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled=false ;
		}
	}

}
