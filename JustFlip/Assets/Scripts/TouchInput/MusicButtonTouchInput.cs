using UnityEngine;

public class MusicButtonTouchInput : MonoBehaviour {
	
	public Sprite normalImage ;
	public Sprite glowImage ;

	private void Start()
	{
		if (PlayerPrefs.HasKey("Music"))
		{
			if (PlayerPrefs.GetInt("Music") == 1)
			{
				transform.localPosition += Vector3.right * (0.1f) ;
				GetComponent<SpriteRenderer>().sprite = glowImage ;
			
			}
		}
		else
		{
			transform.localPosition += Vector3.right * (0.1f) ;
			GetComponent<SpriteRenderer>().sprite = glowImage ;
			PlayerPrefs.SetInt("Music",1) ;
		}
	}

	private void OnMouseDown()
	{
		GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f) ;
		
		if (PlayerPrefs.GetInt("Music") == 1)
		{
			PlayerPrefs.SetInt("Music",0);
			transform.localPosition -= Vector3.right * (0.1f) ;
			GetComponent<SpriteRenderer>().sprite = normalImage ;
			GameManager.audioManager.transform.Find("Music").gameObject.SetActive(false);	
		}
		else
		{
			PlayerPrefs.SetInt("Music",1);
			transform.localPosition += Vector3.right * (0.1f) ;
			GetComponent<SpriteRenderer>().sprite = glowImage;
			GameManager.audioManager.transform.Find("Music").gameObject.SetActive(true);
			GameManager.audioManager.transform.Find("Music").GetComponent<AudioSource>().Play();
		}
	}
}
