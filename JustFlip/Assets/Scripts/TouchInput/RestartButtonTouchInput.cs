using UnityEngine;

public class RestartButtonTouchInput : MonoBehaviour
{

	public GameObject gameBall ;

	[HideInInspector]public static byte gameBallDest ;
	[HideInInspector] public static byte gameBallCurrentPos ;

	private void OnMouseDown()
	{
		PausePanelEvents.buttonName = "Restart" ;
		GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);

		if (GameLogoEvents.isFinalPanel)
		{
			GameLogoEvents.restartButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.restartButtonInstance.GetComponent<Animator>().SetTrigger("FinalOut");
			
			GameLogoEvents.mainMenuButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.mainMenuButtonInstance.GetComponent<Animator>().SetTrigger("FinalOut");
			
			GameLogoEvents.highestScoreInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.highestScoreInstance.GetComponent<Animator>().SetTrigger("FinalOut");
			
			GameLogoEvents.scoreInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.scoreInstance.GetComponent<Animator>().SetTrigger("FinalOut");
			
			GameObject temp2 = GameObject.Find("Canvas") ;
			temp2.GetComponent<Canvas>().sortingOrder = 0 ;
			temp2.transform.Find("FinalScore").gameObject.SetActive(false);
			temp2.transform.Find("HighestScore").gameObject.SetActive(false);
			temp2.transform.Find("Score").gameObject.SetActive(true);
			temp2.transform.Find("RemainTime").gameObject.SetActive(true);

			Obstacle.gameBall = Instantiate(gameBall , Vector3.zero , Quaternion.identity) ;
			LineTouchInput.gameBall = Obstacle.gameBall ;
			Obstacle.gameBall.GetComponent<Animator>().enabled = false ;
		}
		else
		{
			GameLogoEvents.resumeButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.resumeButtonInstance.GetComponent<Animator>().SetTrigger("PauseOut");

			GameLogoEvents.restartButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.restartButtonInstance.GetComponent<Animator>().SetTrigger("PauseOut");
			
			GameLogoEvents.mainMenuButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.mainMenuButtonInstance.GetComponent<Animator>().SetTrigger("PauseOut");

			GameLogoEvents.musicButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.musicButtonInstance.GetComponent<Animator>().SetTrigger("PauseOut");

			GameLogoEvents.fxButtonInstance.GetComponent<Animator>().enabled = true ;
			GameLogoEvents.fxButtonInstance.GetComponent<Animator>().SetTrigger("PauseOut");
		}
		
		Animator temp = GameObject.Find("GameLogo(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");
		

		if (PausePanelEvents.gameManager == null)
		{
			PausePanelEvents.gameManager=GameObject.Find("GameManager(Clone)");
		}

		
		if (gameBallDest!=gameBallCurrentPos)
		{
			Obstacle.lines.GetComponent<Lines>().TurnOffHighLightedLine(gameBallDest);
		}
		
		for (byte i = 0; i < 5; i++)
		{
			Obstacle.lines.transform.Find("Line"+(i+1)).GetComponent<SpriteRenderer>().color=Color.white ;
		}

		
		PausePanelEvents.gameManager.SetActive(false);
		Obstacle.gameBall.SetActive(false);
		PausePanelEvents.pauseButton.GetComponent<BoxCollider2D>().enabled = true ;
		LineTouchInput.gameBallDestroyed = false ;
		Obstacle.lines.GetComponent<Lines>().CreateSizzleEffectForRestart();
		AudioManager.stopMusicActive = true ;
		gameObject.SetActive(false);
		
	}
}
