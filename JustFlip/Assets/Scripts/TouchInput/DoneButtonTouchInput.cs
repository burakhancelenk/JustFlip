using UnityEngine;

public class DoneButtonTouchInput : MonoBehaviour
{

	public GameObject tutorialInformation ;
	public GameObject tutorialBackground ;
	public static bool destroyerMode ;

	private void Start()
	{
		destroyerMode = false ;
	}

	private void OnMouseDown()
	{
		
		if (!destroyerMode)
		{
			Time.timeScale = 1 ;
		}

		destroyerMode = false ;

		if (Tutorial.scoreTutorial)
		{
			Tutorial.scoreTutorial = false ;
		}
	
		Tutorial.canvas.sortingOrder = 0 ;
		tutorialInformation.SetActive(false);
		tutorialBackground.SetActive(false);
		for (byte i = 0; i < 5; i++)
		{
			Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<BoxCollider2D>().enabled = true ;
		}

		GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = true ;
		gameObject.SetActive(false);
		
	}
	
}
