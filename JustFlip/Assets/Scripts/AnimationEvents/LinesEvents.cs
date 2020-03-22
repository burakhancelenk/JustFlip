using UnityEngine;

public class LinesEvents : MonoBehaviour
{

	public GameObject lineEffect ;
	public GameObject backLightning ;
	public GameObject remainTime ;
	public GameObject pauseButton ;
	public GameObject tutorialPrefab ;
	public Sprite remainTimeImage_Glow ;
	public Sprite doublePoint ;
	public Sprite pauseButtonImage_Glow ;
	public Sprite lineImage_Glow ;

	private GameObject temp ;

	public void StartLineEffect()
	{
		temp = Instantiate(lineEffect , transform.Find("Line3").localPosition + Vector3.up * -2.1f , Quaternion.identity) ;
	}

	public void StartBackLightning()
	{
		if (GameManager.backLightningInstance==null)
		{
			GameManager.backLightningInstance = Instantiate(backLightning , backLightning.transform.localPosition , Quaternion.identity) ;
		}
		else
		{
			GameManager.backLightningInstance.SetActive(true);
		}
		
	}

	public void CreateGamePlayUIs()
	{
		if (PausePanelEvents.pauseButton==null)
		{
			PausePanelEvents.pauseButton = Instantiate(pauseButton , pauseButton.transform.localPosition , Quaternion.identity) ;
		}
		else
		{
			PausePanelEvents.pauseButton.SetActive(true);
		}

		if (PausePanelEvents.remainTimeLogo == null)
		{
			PausePanelEvents.remainTimeLogo = Instantiate(remainTime , remainTime.transform.localPosition , Quaternion.identity) ;
		}
		else
		{	
			PausePanelEvents.remainTimeLogo.SetActive(true);
		}
		
	}

	public void SwitchInteract()
	{
		if (transform.Find("Line1").GetComponent<BoxCollider2D>().enabled)
		{
			for (int i = 1; i <=5 ; i++)
			{
				transform.Find("Line" + i).GetComponent<BoxCollider2D>().enabled = false;
			}
		}
		else
		{
			for (int i = 1; i <=5 ; i++)
			{
				transform.Find("Line" + i).GetComponent<BoxCollider2D>().enabled = true;
			}
		}
		
	}

	public void AnimateGameBallAndUIs()
	{
		Obstacle.gameBall.GetComponent<Animator>().SetTrigger("StartGame");
		GameObject temp = GameObject.Find("RemainTime(Clone)");
		for (int i = 0; i < temp.GetComponentsInChildren<SpriteRenderer>().Length; i++)
		{
			if (i == 0)
			{
				temp.GetComponentsInChildren<SpriteRenderer>()[i].sprite = remainTimeImage_Glow ;
				temp.GetComponentsInChildren<SpriteRenderer>()[i].transform.localPosition+=Vector3.right*(-0.8f)+Vector3.up*(0.16f);
			}
			else
			{
				temp.GetComponentsInChildren<SpriteRenderer>()[i].sprite = doublePoint ;
				temp.GetComponentsInChildren<SpriteRenderer>()[i].transform.localPosition+=Vector3.right*(0.04f)+Vector3.up*(-0.01f);
			}
		}
		
		temp=GameObject.Find("PauseLogo(Clone)");
		temp.GetComponent<SpriteRenderer>().sprite = pauseButtonImage_Glow ;
		temp.transform.localPosition+=Vector3.right*(0.03f)+Vector3.up*(0.01f);
		GameObject.Find("Canvas").transform.Find("RemainTime").gameObject.SetActive(true);
		GameObject.Find("Canvas").transform.Find("Score").gameObject.SetActive(true);
		Destroy(this.temp);
		
	}

	public void StabilizeValues()
	{
		transform.Find("Line" + 1).GetComponent<SpriteRenderer>().sprite = lineImage_Glow ;
		transform.Find("Line"+1).localPosition=Vector3.right*(-49.99f)+Vector3.up*(-8.4f);
		transform.Find("Line" + 2).GetComponent<SpriteRenderer>().sprite = lineImage_Glow ;
		transform.Find("Line"+2).localPosition=Vector3.right*(-24.69f)+Vector3.up*(-8.4f);
		transform.Find("Line" + 3).GetComponent<SpriteRenderer>().sprite = lineImage_Glow ;
		transform.Find("Line"+3).localPosition=Vector3.right*(0f)+Vector3.up*(-8.4f);
		transform.Find("Line" + 4).GetComponent<SpriteRenderer>().sprite = lineImage_Glow ;
		transform.Find("Line"+4).localPosition=Vector3.right*(25.31f)+Vector3.up*(-8.4f);
		transform.Find("Line" + 5).GetComponent<SpriteRenderer>().sprite = lineImage_Glow ;
		transform.Find("Line"+5).localPosition=Vector3.right*(50.31f)+Vector3.up*(-8.4f);
		StartCoroutine(GetComponent<Lines>().CreateSizzleEffect(2)) ;
		GetComponent<Animator>().enabled = false ;
	}

	public void StartTutorial()
	{
		if (!PlayerPrefs.HasKey("Tutorial"))
		{
			Instantiate(tutorialPrefab , Vector3.zero , Quaternion.identity) ;
		}
	}
	
	
}
