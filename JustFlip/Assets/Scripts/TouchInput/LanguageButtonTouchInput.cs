using UnityEngine;
using UnityEngine.UI ;

public class LanguageButtonTouchInput : MonoBehaviour
{

	public GameObject languageBackground ;
	public GameObject languageInformation ;
	public GameObject englishButton ;
	public GameObject turkishButton ;
	public GameObject gameLogo ;
	public Text doneButtonText ;

	void Start () {
		
	}

	private void OnMouseDown()
	{
		languageBackground.SetActive(false);
		languageInformation.SetActive(false);
		englishButton.SetActive(false);
		turkishButton.SetActive(false);

		if (gameObject.name == "englishButton")
		{
			Tutorial.languageEnglish = true ;
			doneButtonText.text = "Got it!" ;
		}
		else
		{
			Tutorial.languageEnglish = false ;
		}
		
		GameObject temp = Instantiate(gameLogo , gameLogo.transform.localPosition , Quaternion.identity) ;
		temp.GetComponent<Animator>().SetTrigger("GameIntro");
	}
}
