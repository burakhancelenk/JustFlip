using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{

	[HideInInspector] public static bool syncScore ;
	[HideInInspector] public static bool getScore ;
	[HideInInspector] public static bool getEMPValues ;
	[HideInInspector] public static bool setEMPValues ;
	
    private static bool setTEWA ;

	private void OnDisable()
	{
		syncScore = false ;
		getScore = false ;
		getEMPValues = false ;
		setEMPValues = false ;
		setTEWA = false ;

	}


	private void Start()
	{
		syncScore = false ;
		getScore = false ;
		getEMPValues = false ;
		setEMPValues = false ;
		setTEWA = false ;
		
		JustFlipClient.GetInstance(GetType()).SetReferencesForJustFlipClient
		(
			GameObject.Find("Canvas").transform.Find("ReviverShockScreen").transform
				.Find("AmountOfShocks").GetComponent<Text>(),
			GameObject.Find("Canvas").transform.Find("HighestScore").GetComponent<Text>()
		);
	}

	private void Update()
	{
		if (syncScore)
		{
			syncScore = false ;
			JustFlipClient.GetInstance(this.GetType()).TrySubmitScore((int)GameManager.GetScore(GetType()));
		}

		if (getScore)
		{
			getScore = false ;
			JustFlipClient.GetInstance(GetType()).GetYourLeaderboardScore();
		}

		if (getEMPValues)
		{
			getEMPValues = false ;
			JustFlipClient.GetInstance(GetType()).GetStatistic("TotalCurrentEMPs","RevivePanel");
		}

		if (setEMPValues)
		{
			setEMPValues = false ;
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalCurrentEMPs","RevivePanel" , -1)) ;
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalSpentEMPs" , "RevivePanel" , 1)) ;
		}

		if (setTEWA)
		{
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalCurrentEMPs","EMPPricePanel" , 1)) ;
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalWatchedAds" , "EMPPricePanel" , 1)) ;
			setTEWA = false ;
		}

		if (Purchaser.GetBuyEMPTrigger(GetType(),"50"))
		{
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalCurrentEMPs" , "EMPPricePanel" , 50)) ;
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalPurchasedEMPs" , "EMPPricePanel" , 50)) ;
			Purchaser.ResetBuyEMPTrigger(GetType(),"50");
		}
		
		if (Purchaser.GetBuyEMPTrigger(GetType(),"100"))
		{
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalCurrentEMPs" , "EMPPricePanel" , 100)) ;
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalPurchasedEMPs" , "EMPPricePanel" , 100)) ;
			Purchaser.ResetBuyEMPTrigger(GetType(),"100");
		}
		
		if (Purchaser.GetBuyEMPTrigger(GetType(),"250"))
		{
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalCurrentEMPs" , "EMPPricePanel" , 250)) ;
			StartCoroutine(JustFlipClient.GetInstance(GetType()).SetStatistic("TotalPurchasedEMPs" ,"EMPPricePanel" , 250)) ;
			Purchaser.ResetBuyEMPTrigger(GetType(),"250");
		}
	}

	public static void SetAdsTrigger(Type refType)
	{
		if (refType == typeof(ADStarter))
		{
			setTEWA = true ;
		}
	}
	
}
