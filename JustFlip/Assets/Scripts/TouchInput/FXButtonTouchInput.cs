using UnityEngine;

public class FXButtonTouchInput : MonoBehaviour
{

	public Sprite normalImage ;
	public Sprite glowImage ;
	
	private void Start()
	{
		if (PlayerPrefs.HasKey("FX"))
		{
			if (PlayerPrefs.GetInt("FX") == 1)
			{
				transform.localPosition += Vector3.right * (-0.1f) + Vector3.up * (-0.17f);
				GetComponent<SpriteRenderer>().sprite = glowImage;
			
			}
		}
		else
		{
			transform.localPosition += Vector3.right * (-0.1f) + Vector3.up * (-0.17f);
			GetComponent<SpriteRenderer>().sprite = glowImage;
			PlayerPrefs.SetInt("FX",1);
		}
		
	
	}

	private void OnMouseDown()
	{
		GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
	
		if (PlayerPrefs.GetInt("FX") == 1)
		{
			PlayerPrefs.SetInt("FX",0);
			transform.localPosition -= Vector3.right * (-0.1f) + Vector3.up * (-0.17f);
			GetComponent<SpriteRenderer>().sprite = normalImage;
			
			for (byte i = 1; i < 7; i++)
			{
				GameManager.audioManager.transform.Find("fx"+i).gameObject.SetActive(false);
			}
			GameManager.audioManager.transform.Find("RailFrictionEffect").gameObject.SetActive(false);
			
		}
		else
		{
			PlayerPrefs.SetInt("FX",1);
			transform.localPosition += Vector3.right * (-0.1f) + Vector3.up * (-0.17f);
			GetComponent<SpriteRenderer>().sprite = glowImage;
			
			for (byte i = 1; i < 7; i++)
			{
				GameManager.audioManager.transform.Find("fx"+i).gameObject.SetActive(true);
			}
			GameManager.audioManager.transform.Find("RailFrictionEffect").gameObject.SetActive(true);
		}
	}
}
