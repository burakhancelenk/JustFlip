using UnityEngine;

public class PlayButtonTouch : MonoBehaviour
{
	

	private void OnMouseUp()
	{
		
		GetComponent<Animator>().SetTrigger("StartGame");
		GameObject.Find("GameLogo(Clone)").GetComponent<Animator>().SetTrigger("StartGame") ;
		if (GameLogoEvents.facebookLogoInstance!=null)
		{
			GameLogoEvents.facebookLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.twitterLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.instagramLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.soundLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.scoreBoardLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.empLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
		}
		else
		{
			GameLogoEvents.facebookLogoInstance = GameObject.Find("FacebookLogo(Clone)") ;
			GameLogoEvents.twitterLogoInstance = GameObject.Find("TwitterLogo(Clone)") ;
			GameLogoEvents.instagramLogoInstance = GameObject.Find("InstagramLogo(Clone)") ;
			GameLogoEvents.soundLogoInstance = GameObject.Find("SoundLogo(Clone)") ;
			GameLogoEvents.scoreBoardLogoInstance = GameObject.Find("ScoreBoardLogo(Clone)");
			GameLogoEvents.empLogoInstance = GameObject.Find("EMPLogo(Clone)");
			
			GameLogoEvents.facebookLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.twitterLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.instagramLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.soundLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.scoreBoardLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			GameLogoEvents.empLogoInstance.GetComponent<Animator>().SetTrigger("StartGame");
			
		}
		
	}

}
