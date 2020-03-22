using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels ;
using UnityEngine;
using UnityEngine.UI ;

public class LeaderboardInitializer : MonoBehaviour
{

	public GameObject playerInf ;
	public RectTransform content ;

	public Color myPlayerColor ;
	public static string myDisplayName ;
	[HideInInspector]public static bool leaderboardIsLoaded ;

	private GameObject[] players = new GameObject[100] ;

	private void Start()
	{
		if (Tutorial.languageEnglish)
		{
			transform.Find("LeaderboardText").GetComponent<Text>().text = "LEADERBOARD" ;
			transform.Find("LoadingText").GetComponent<Text>().text = "Loading..." ;
		}
		else
		{
			transform.Find("LeaderboardText").GetComponent<Text>().text = "SKOR PANELI" ;
			transform.Find("LoadingText").GetComponent<Text>().text = "Yükleniyor..." ;
		}
		
	}

	private void OnEnable()
	{
		StartCoroutine(FillLeaderboard());
	}

	private void OnDisable()
	{
		RectTransform[] temp = content.GetComponentsInChildren<RectTransform>() ;
		for (byte i = 1; i < temp.Length; i++)
		{
			Destroy(temp[i].gameObject);
		}
	}


	private IEnumerator FillLeaderboard()
	{
		if (string.IsNullOrEmpty(myDisplayName))
		{
			JustFlipClient.GetInstance(GetType()).GetPlayerDisplayName();
		}
		
		List<PlayerLeaderboardEntry> lb ;
		JustFlipClient.GetInstance(this.GetType()).TryGetLeaderBoard();
		transform.Find("LoadingText").gameObject.SetActive(true);
		
		while (!leaderboardIsLoaded || string.IsNullOrEmpty(myDisplayName))
		{
			yield return null ;
		}
		
		transform.Find("LoadingText").gameObject.SetActive(false);

		lb = JustFlipClient.GetInstance(this.GetType()).GetLeaderBoard() ;

		for (byte i = 0; i < lb.Count; i++)
		{
			players[i] = Instantiate(playerInf , playerInf.transform.position , playerInf.transform.rotation) ;
			players[i].GetComponent<RectTransform>().SetParent(content);
			players[i].GetComponent<RectTransform>().localPosition =
				Vector3.right * players[i].GetComponent<RectTransform>().localPosition.x +
				Vector3.up * (players[i].GetComponent<RectTransform>().localPosition.y-(i)*30) ;

			players[i].transform.Find("Order").GetComponent<Text>().text = (lb[i].Position + 1).ToString() ;
			players[i].transform.Find("Nickname").GetComponent<Text>().text = lb[i].DisplayName ;
			players[i].transform.Find("Score").GetComponent<Text>().text = (lb[i].StatValue).ToString() ;

			if (myDisplayName == lb[i].DisplayName)
			{
				players[i].transform.Find("Order").GetComponent<Text>().color = myPlayerColor ;
				players[i].transform.Find("Nickname").GetComponent<Text>().color = myPlayerColor ;
				players[i].transform.Find("Score").GetComponent<Text>().color = myPlayerColor ;
			}
		}
		
		content.sizeDelta= Vector2.right*content.sizeDelta.x + Vector2.up*(lb.Count)*30;

	}
	
	
}
