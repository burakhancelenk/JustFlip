using UnityEngine;

public class ResumeButtonTouchInput : MonoBehaviour {
	
	

	private void OnMouseDown()
	{
		PausePanelEvents.buttonName = "Resume" ;
		GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
		
		Animator temp = GameObject.Find("ResumeButton(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");

		temp = GameObject.Find("RestartButton(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");

		temp = GameObject.Find("MainMenuButton(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");

		temp = GameObject.Find("MusicButton(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");

		temp = GameObject.Find("FXButton(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");

		temp = GameObject.Find("GameLogo(Clone)").GetComponent<Animator>() ;
		temp.enabled = true ;
		temp.SetTrigger("PauseOut");
		
		gameObject.SetActive(false);
	}
}
