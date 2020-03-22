using System.Collections;
using UnityEngine;
using UnityEngine.U2D ;
using UnityEngine.UI ;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

public class TempStarter : MonoBehaviour
{

	public GameObject GameLogo ;
	public Text languageInformation ;
	public GameObject englishButton ;
	public GameObject turkishButton ;
	public RectTransform languageBackground ;
	public bool skipTutorial ;
	public static AudioManager audioManager ;
	
	[HideInInspector] public static bool startTutorial ;
	


	private void Awake()
	{
		QualitySettings.vSyncCount = 0 ;
		Application.targetFrameRate = 60 ;
	}


	void Start ()
	{
		SpriteAtlasManager.atlasRequested += GetSpriteAtlases ;
		ArrangeScreenOriaentation();

		if (PlayerPrefs.HasKey("HighestScore"))
		{
			PlayerPrefs.DeleteKey("HighestScore");
		}

		if (!skipTutorial)
		{
			startTutorial = false ;
		}
		else
		{
			startTutorial = true ;
		}

		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>() ;
		
		
		
		
		
		StartCoroutine(StartTutorial()) ;

	}
	
	private void ArrangeScreenOriaentation()
	{
		if ((float)Screen.height/(float)Screen.width <= 1.79f )
		{
			GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 107 ;
		}
		else if ((float)Screen.height/(float)Screen.width >= 1.9f && (float)Screen.height/(float)Screen.width<=2.05f)
		{
			GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 120 ;
		}
		else if ((float)Screen.height/(float)Screen.width >=2.1f)
		{
			GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 130 ;
			
		}
	}
	

	public IEnumerator firstWait()
	{
		yield return new WaitForSeconds(0.5f);
	}

	

	public IEnumerator StartTutorial()
	{
		while (!startTutorial)
		{
			yield return null ;
		}
		
		if (PlayerPrefs.HasKey("Tutorial"))
		{
			GameObject temp = Instantiate(GameLogo , GameLogo.transform.localPosition , Quaternion.identity) ;
			temp.GetComponent<Animator>().SetTrigger("GameIntro");
		}
		else
		{
			languageBackground.gameObject.SetActive(true);
			languageInformation.gameObject.SetActive(true) ;
			englishButton.SetActive(true);
			turkishButton.SetActive(true);
			languageInformation.text =
				"Please choose language for tutorial.\n(Lütfen tutorial için dil seçimi yapın.)" ;
		}
		
		
		
	}

	private void GetSpriteAtlases(string tag,System.Action<SpriteAtlas> callback)
	{
		var sa = Resources.Load<SpriteAtlas>(tag);
		callback(sa);
	}
	
}
