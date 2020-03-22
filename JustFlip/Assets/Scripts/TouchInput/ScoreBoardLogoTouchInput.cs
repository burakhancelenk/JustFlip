using UnityEngine;

public class ScoreBoardLogoTouchInput : MonoBehaviour
{

	[HideInInspector] public static Transform leaderboard ;
	
	private void OnMouseDown()
	{
		if (leaderboard == null)
		{
			leaderboard = GameObject.Find("Canvas").transform.Find("Leaderboard") ;
		}
		
		if (!leaderboard.gameObject.activeSelf && !GameObject.Find("Canvas").transform.Find("EMPPanel").gameObject.activeSelf)
		{
			leaderboard.gameObject.SetActive(true);
			GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 10 ;
			GameObject.Find("PlayButton(Clone)").GetComponent<CircleCollider2D>().enabled = false ;
		}
		
		if (GameManager.audioManager!=null)
		{
			GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
		}
		else
		{
			GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
			GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
		}
	}
}
