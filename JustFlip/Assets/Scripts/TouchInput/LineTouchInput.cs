using UnityEngine;

public class LineTouchInput : MonoBehaviour
{

	[HideInInspector] public static GameObject gameBall ;
	public byte lineIndice ;
	[HideInInspector] public static bool gameBallDestroyed ;


	private void OnMouseDown()
	{
		if (!gameBallDestroyed)
		{


			if (!PlayerPrefs.HasKey("Tutorial"))
			{
				if (Tutorial.lastTutorial)
				{
					Time.timeScale = 1 ;
					StartCoroutine(GameObject.Find("Tutorial(Clone)").GetComponent<Tutorial>().StartScoreTutorial()) ;
					Tutorial.scoreTutorial = true ;
					Tutorial.lastTutorial = false ;
				}

			}


			StartCoroutine(gameBall.GetComponent<GameBall>().ChangePosition(lineIndice)) ;



		}
	}
}
