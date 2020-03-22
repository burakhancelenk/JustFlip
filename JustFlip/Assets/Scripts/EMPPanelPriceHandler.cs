using System.Collections;
using UnityEngine;
using UnityEngine.UI ;

public class EMPPanelPriceHandler : MonoBehaviour
{

	private Text cost50 ;
	private Text cost100 ;
	private Text cost250 ;

	public Button okButton ;
	public Button watchAds ;
	public GameObject infPanel ;
	public CircleCollider2D backButtonCollider ;

	public Text[] buyButtonTexts ;
	

	private void Start()
	{
		cost50 = transform.Find("BuyOffer50").transform.Find("Cost").GetComponent<Text>() ;
		cost100 = transform.Find("BuyOffer100").transform.Find("Cost").GetComponent<Text>() ;
		cost250 = transform.Find("BuyOffer250").transform.Find("Cost").GetComponent<Text>() ;

		if (Tutorial.languageEnglish)
		{
			buyButtonTexts[0].text = "Buy" ;
			buyButtonTexts[1].text = "Buy" ;
			buyButtonTexts[2].text = "Buy" ;
		}
		else
		{
			buyButtonTexts[0].text = "Satın al" ;
			buyButtonTexts[1].text = "Satın al" ;
			buyButtonTexts[2].text = "Satın al" ;
		}

		if (Tutorial.languageEnglish)
		{
			watchAds.transform.Find("Text").GetComponent<Text>().text = "Watch Ad" ;
		}
		else
		{
			watchAds.transform.Find("Text").GetComponent<Text>().text = "Reklam izle" ;
		}
		
		
		
		
		
		JustFlipClient.GetInstance(GetType()).SetReferenceOfAOSForPricePanel(
			GameObject.Find("Canvas").transform.Find("EMPPanel").transform.Find("AmountOfShocks").GetComponent<Text>()
			);

		okButton.onClick.AddListener(OnClickOkButton);
		watchAds.onClick.AddListener(OnClickWatchAdsButton);
	}

	private void OnEnable()
	{
		JustFlipClient.GetInstance(GetType()).GetTitleDataForPrices();
		JustFlipClient.GetInstance(GetType()).GetStatistic("TotalCurrentEMPs","EMPPricePanel");
		StartCoroutine(ForSecureSetAndClose()) ;
	}

	private void OnDisable()
	{
		cost50.text = string.Empty ;
		cost100.text = string.Empty ;
		cost250.text = string.Empty ;
		transform.Find("AmountOfShocks").GetComponent<Text>().text = string.Empty;
		ResetAllEMPPriceChecks();
		JustFlipClient.GetInstance(GetType()).ResetEMPPulled();
	}

	private void ResetAllEMPPriceChecks()
	{
		JustFlipClient.EMPPricesPulled = false ;
	}

	private IEnumerator ForSecureSetAndClose()
	{
		transform.Find("BackButton").gameObject.SetActive(false);
		while (!JustFlipClient.EMPPricesPulled ||
		       !JustFlipClient.GetInstance(GetType()).GetEMPPulled())
		{
			yield return null ;
		}
		cost50.text = JustFlipClient.EMP50Price + "$" ;
		cost100.text = JustFlipClient.EMP100Price + "$" ;
		cost250.text = JustFlipClient.EMP250Price + "$" ;
		transform.Find("BackButton").gameObject.SetActive(true);
	}
	
	

	private void OnClickWatchAdsButton()
	{
		ADStarter.showAd = true ;
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

	private void OnClickOkButton()
	{
		infPanel.SetActive(false);
		backButtonCollider.enabled = true ;
		if (GameManager.audioManager!=null)
		{
			GameManager.audioManager.PlaySoundFX("PauseClick",1f);
		}
		else
		{
			GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
			GameManager.audioManager.PlaySoundFX("PauseClick",1f);
		}
	}
	
}
