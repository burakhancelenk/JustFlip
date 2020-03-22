using UnityEngine;

public class LeaderboardBackButtonTouchInput : MonoBehaviour {
	
	
	private void OnMouseDown()
	{
		GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 0 ;
		GameObject.Find("PlayButton(Clone)").GetComponent<CircleCollider2D>().enabled = true ;
		LeaderboardInitializer.leaderboardIsLoaded = false ;
		
		if (GameManager.audioManager!=null)
		{
			GameManager.audioManager.PlaySoundFX("PauseClick",1f);
		}
		else
		{
			GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
			GameManager.audioManager.PlaySoundFX("PauseClick",1f);
		}
		
	   ScoreBoardLogoTouchInput.leaderboard.gameObject.SetActive(false);
	}
	
}
