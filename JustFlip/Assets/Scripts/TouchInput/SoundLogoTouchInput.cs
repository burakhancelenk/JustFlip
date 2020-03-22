using UnityEngine;

public class SoundLogoTouchInput : MonoBehaviour
{

	public bool isOpen ;

	private void Start()
	{
		isOpen = false ;
	}

	private void OnMouseDown()
	{
		if (!isOpen)
		{
			if (GameManager.audioManager!=null)
			{
				GameManager.audioManager.PlaySoundFX("PauseClick",1f);
			}
			else
			{
			 GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
				GameManager.audioManager.PlaySoundFX("PauseClick",1f);
			}
			
			transform.Find("SoundOpenedLine").gameObject.SetActive(true);
			transform.Find("MusicLogo").gameObject.SetActive(true);
			transform.Find("FXLogo").gameObject.SetActive(true);
			isOpen = true ;
		}
		else
		{
			GameManager.audioManager.PlaySoundFX("PauseClick",1f);
			transform.Find("SoundOpenedLine").gameObject.SetActive(false);
			transform.Find("MusicLogo").gameObject.SetActive(false);
			transform.Find("FXLogo").gameObject.SetActive(false);
			isOpen = false ;
		}
	}
}
