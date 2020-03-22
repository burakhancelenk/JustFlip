using UnityEngine;

public class EMPLogoTouchInput : MonoBehaviour
{

	[HideInInspector] public static GameObject EMPPanel ;

	private void Start()
	{
		EMPPanel = GameObject.Find("Canvas").transform.Find("EMPPanel").gameObject; 
	}

	private void OnMouseDown()
	{
		if (!EMPPanel.activeSelf && !GameObject.Find("Canvas").transform.Find("Leaderboard").gameObject.activeSelf)
		{
			GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 10 ;
			GameObject.Find("PlayButton(Clone)").GetComponent<CircleCollider2D>().enabled = false ;
			EMPPanel.SetActive(true);
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
