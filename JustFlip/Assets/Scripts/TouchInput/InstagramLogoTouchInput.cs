using UnityEngine;

public class InstagramLogoTouchInput : MonoBehaviour {

	private void OnMouseUp()
	{
		if (GameManager.audioManager!=null)
		{
			GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
		}
		else
		{
			GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
			GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
		}
		
		Application.OpenURL("https://www.instagram.com/justflipgame0/");
	}
}
