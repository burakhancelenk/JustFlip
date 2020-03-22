using System ;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button ;

public class ReviverShockScreenTouchInput : MonoBehaviour
{

	private bool reviveButtonClicked ;
	private bool noThanksButtonClicked ;
	private bool UICheck ;
	private bool destroyGameBallCheck ;

	public Button reviveButton ;
	public Button noThanksButton ;

	private void Start()
	{
		reviveButtonClicked = false ;
		noThanksButtonClicked = false ;
		UICheck = false ;
		destroyGameBallCheck = false ;

		if (Tutorial.languageEnglish)
		{
			transform.Find("Question").GetComponent<Text>().text = "Do you want to revive ?" ;
			reviveButton.transform.Find("Text").GetComponent<Text>().text = "Revive!" ;
			noThanksButton.transform.Find("Text").GetComponent<Text>().text = "No, thanks!" ;
		}
		else
		{
			transform.Find("Question").GetComponent<Text>().text = "Canlanmak istiyor musun ?" ;
			reviveButton.transform.Find("Text").GetComponent<Text>().text = "Canlan!" ;
			noThanksButton.transform.Find("Text").GetComponent<Text>().text = "Hayır!" ;
		}
		
		reviveButton.onClick.AddListener(OnReviveButtonClicked);
		noThanksButton.onClick.AddListener(OnNoThanksButtonClicked);
	}

	private void OnDisable()
	{
		transform.Find("AmountOfShocks").GetComponent<Text>().text=String.Empty;
	}


	private void OnReviveButtonClicked()
	{
			reviveButtonClicked = true ;
			UICheck = true ;
			destroyGameBallCheck = true ;
			gameObject.SetActive(false);
	}

	private void OnNoThanksButtonClicked()
	{
		noThanksButtonClicked = true ;
		ScoreHandler.syncScore = true ;
		UICheck = true ;
		destroyGameBallCheck = true ;
		gameObject.SetActive(false);
	}

	public bool GetReviveButtonClicked()
	{
		return reviveButtonClicked ;
	}

	public bool GetNoThanksButtonClicked()
	{
		return noThanksButtonClicked ;
	}

	public bool GetUICheck()
	{
		return UICheck ;
	}

	public bool GetDestoryGameBallCheck()
	{
		return destroyGameBallCheck ;
	}
	
	public void ResetAllButtonClickedTriggers()
	{
		reviveButtonClicked = false ;
		noThanksButtonClicked = false ;
	}

	public void ResetDestroyGameBallCheck()
	{
		destroyGameBallCheck = false ;
	}

	public void ResetUICheck()
	{
		UICheck = false ;
	}
	
}
