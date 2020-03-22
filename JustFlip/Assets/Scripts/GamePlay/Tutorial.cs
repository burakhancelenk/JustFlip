using System.Collections;
using UnityEngine;
using UnityEngine.UI ;

public class Tutorial : MonoBehaviour
{
	[HideInInspector] public RectTransform tutorialBackground ;
	[HideInInspector] public Text tutorialInformation ;
	[HideInInspector] public GameObject doneButton ;

	[HideInInspector] public static bool firstStop ;
	[HideInInspector] public static byte obstacleCounter ;
	[HideInInspector] public static bool obstacleHitTutorialFinished ;
	[HideInInspector] public static bool sameLineHighlighted ;
	[HideInInspector] public static bool differentLineHighlighted ;
	[HideInInspector] public static bool destroyerModeTutorial ;
	[HideInInspector] public static bool scoreTutorial ;
	[HideInInspector] public static bool lastTutorial ;
	[HideInInspector] public static Canvas canvas ;
	[HideInInspector] public static bool languageEnglish ;

	private bool sameLineHighlightedFinished ;
	private bool differentLineHighlightedFinished ;
	
	void Start ()
	{
		firstStop = false ;
		obstacleHitTutorialFinished = false ;
		sameLineHighlighted = false ;
		differentLineHighlighted = false ;
		destroyerModeTutorial = false ;
		scoreTutorial = false ;
		sameLineHighlightedFinished = false ;
		differentLineHighlightedFinished = false ;
		lastTutorial = false ;
		obstacleCounter = 0 ;
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>() ;
		tutorialInformation = GameObject.Find("Canvas").transform.Find("TutorialInformation").GetComponent<Text>() ;
		doneButton = GameObject.Find("Canvas").transform.Find("DoneButton").gameObject;
		tutorialBackground =
			GameObject.Find("Canvas").transform.Find("TutorialBackground").GetComponent<RectTransform>() ;
		StartCoroutine(StartFirstTutorial()) ;
	}

	public IEnumerator StartFirstTutorial()
	{
		yield return new WaitForSeconds(0.5f);
		firstStop = true ;
	    tutorialBackground.localPosition = new Vector3(0,-7,0);
		tutorialBackground.sizeDelta = new Vector2(320,190);
		while (true)
		{
			if (firstStop)
			{
				canvas.sortingOrder = 20 ;
				Time.timeScale = 0 ;
				firstStop = false ;
				tutorialBackground.localPosition = new Vector3(0,-2,0);
				tutorialBackground.sizeDelta = new Vector2(320,200);
				tutorialBackground.gameObject.SetActive(true);
				tutorialInformation.gameObject.SetActive(true);
				
				if (languageEnglish)
				{
					tutorialInformation.text =
						"How to play?\n\n DON'T MOVE !!!\n Wait for triangles !" ;
				}
				else
				{
					tutorialInformation.text = 
						"Nasıl Oynanır?\n\nHAREKET ETME !!!\nÜçgen gelene kadar bekle !" ;
				}
				
				tutorialInformation.gameObject.SetActive(true);
				doneButton.SetActive(true);
				for (byte i = 0; i < 5; i++)
				{
					Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = false ;
				}

				GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = false ;
				break;
			}

			yield return null ;
		}
		StartCoroutine(StartObsctacleHitTutorial()) ;
		StartCoroutine(StartDestroyerModeTutorial()) ;
	}

	public IEnumerator StartObsctacleHitTutorial()
	{
		while (true)
		{
			if (obstacleCounter<2)
			{
				if (sameLineHighlighted && !sameLineHighlightedFinished)
				{
					canvas.sortingOrder = 20 ;
					tutorialBackground.localPosition = new Vector3(0,-7,0);
					tutorialBackground.sizeDelta = new Vector2(320,190);
					sameLineHighlightedFinished = true ;
					Time.timeScale = 0 ;
					sameLineHighlighted = false ;
					obstacleCounter++ ;
					tutorialBackground.gameObject.SetActive(true);
					tutorialInformation.gameObject.SetActive(true);
					if (languageEnglish)
					{
						tutorialInformation.text =
							"How to play?\n\nSome of the triangles lead you to your line. Stay !" ;
					}
					else
					{
						tutorialInformation.text = 
							"Nasıl Oynanır?\n\nBazı üçgenler seni olduğun çizgiye yönlendirir. Olduğun yerde kal." ;
					}
					
					tutorialInformation.gameObject.SetActive(true);
					doneButton.SetActive(true);
					for (byte i = 0; i < 5; i++)
					{
						Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = false ;
					}

					GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = false ;
				}

				if (differentLineHighlighted && !differentLineHighlightedFinished)
				{
					canvas.sortingOrder = 20 ;
					tutorialBackground.localPosition = new Vector3(0,48,0);
					tutorialBackground.sizeDelta = new Vector2(320,300);
					differentLineHighlightedFinished = true ;
					Time.timeScale = 0 ;
					differentLineHighlighted = false ;
					tutorialBackground.gameObject.SetActive(true);
					tutorialInformation.gameObject.SetActive(true);
					if (languageEnglish)
					{
						tutorialInformation.text =
							"How to play?\n\nThe triangle highlighted the line that you must jump.\nJump! before time is up(Upper Left Corner).";
					}
					else
					{
						tutorialInformation.text = 
							"Nasıl Oynanır?\n\nÜçgen atlayacağın çizgiyi aydınlattı.\nSol üst köşedeki süre bitmeden atla." ;
					}
					
					tutorialInformation.gameObject.SetActive(true);
					doneButton.SetActive(true);
					for (byte i = 0; i < 5; i++)
					{
						Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = false ;
					}

					GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = false ;
					obstacleCounter++ ;
				}
			}
			else
			{
				obstacleHitTutorialFinished = true ;
			}

			if (obstacleHitTutorialFinished)
			{
				break;
			}

			yield return null ;

		}

		
	}

	public IEnumerator StartDestroyerModeTutorial()
	{
		while (true)
		{
			if (destroyerModeTutorial)
			{
				canvas.sortingOrder = 20 ;
				tutorialBackground.localPosition = new Vector3(0,58,0);
				tutorialBackground.sizeDelta = new Vector2(320,320);
				Time.timeScale = 0 ;
				destroyerModeTutorial = false ;
				DoneButtonTouchInput.destroyerMode = true ;
				tutorialBackground.gameObject.SetActive(true);
				tutorialInformation.gameObject.SetActive(true);
				if (languageEnglish)
				{
					tutorialInformation.text = "How to play?\n\nIf you hit 3 triangles on the same line, triangles will be red. Don't forget, red triangles destroy the ball." ;
				}
				else
				{
					tutorialInformation.text = "Nasıl Oynanır?\n\nAynı çizgide 3 üçgene çarparsan, üçgenler kırmızı olacaktır.\nUnutma" +
					                           " kırmızı renkli üçgen topu patlatır !" ;
				}
				
				tutorialInformation.gameObject.SetActive(true);
				doneButton.SetActive(true);
				for (byte i = 0; i < 5; i++)
				{
					Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = false ;
				}

				GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = false ;
				break;
			}

			yield return null ;
		}

		lastTutorial = true ;
		
	}

	public IEnumerator StartScoreTutorial()
	{
		yield return new WaitForSeconds(0.5f);
		while (true)
		{
			if (scoreTutorial)
			{
				canvas.sortingOrder = 20 ;
				tutorialBackground.localPosition = new Vector3(0,-2,0);
				tutorialBackground.sizeDelta = new Vector2(320,200);
				Time.timeScale = 0 ;
				scoreTutorial = false ;
				tutorialBackground.gameObject.SetActive(true);
				tutorialInformation.gameObject.SetActive(true);
				
				if (languageEnglish)
				{
					tutorialInformation.text =
						"How to play?\n\nThe number upper right corner is your score." ;
				}
				else
				{
					tutorialInformation.text = 
						"Nasıl Oynanır?\n\nSağ üst köşedeki sayı senin skorun." ;
				}
				
				tutorialInformation.gameObject.SetActive(true);
				doneButton.SetActive(true);
				for (byte i = 0; i < 5; i++)
				{
					Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = false ;
				}

				GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = false ;
				break;
			}

			yield return null ;
		}

		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (sameLineHighlightedFinished && differentLineHighlightedFinished)
			{
				PlayerPrefs.SetInt("Tutorial",0);
				Destroy(gameObject);
				break;
			}
			
		}
		
	}
	
	
}
