using UnityEngine;

public class MainMenuTouchInput : MonoBehaviour
{

	public GameObject gameBall ;



	private void OnMouseDown()
	{
		PausePanelEvents.buttonName = "MainMenu" ;
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
			Obstacle.gameBall = Instantiate(gameBall , Vector3.zero , Quaternion.identity) ;
			LineTouchInput.gameBall = Obstacle.gameBall ;
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
		
		
		if (GameLogoEvents.gameManager == null)
		{
			GameLogoEvents.gameManager=GameObject.Find("GameManager(Clone)");
		}
		
		for (int i = 0; i < 5; i++)
		{
			Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = true ;
		}
		
		GameLogoEvents.gameManager.SetActive(false) ;
		Obstacle.gameBall.transform.localPosition=Vector3.zero ;
		Obstacle.gameBall.GetComponent<Animator>().enabled = true ;
		Obstacle.gameBall.SetActive(false) ;
		Obstacle.lines.SetActive(false) ;
		PausePanelEvents.pauseButton.GetComponent<BoxCollider2D>().enabled = true ;
		AudioManager.stopMusicActive = true ;
		LineTouchInput.gameBallDestroyed = false ;


	}
}
