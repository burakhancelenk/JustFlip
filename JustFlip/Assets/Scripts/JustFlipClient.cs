using System ;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI ;

public class JustFlipClient : ScriptableObject
{

	private int EMPsValue ;
	private int AdsValue ;
	private int PEMPsValue ;
	private int SEMPsValue ;

	public static int EMP50Price ;
	public static int EMP100Price ;
	public static int EMP250Price ;

	public static bool EMPPricesPulled = false;


	private bool EMPsValuePulled = false;
	private bool AdsValuePulled = false;
	private bool PEMPsValuePulled = false;
	private bool SEMPsValuePulled  = false;


	private Text AmountOfShocks ;
	private Text AmountOfShocksForPricePanel ;
	private Text HighestScore ;


	private static JustFlipClient JFC = ScriptableObject.CreateInstance<JustFlipClient>() ;
	private static JustFlipClient jfc ;
	private static List<PlayerLeaderboardEntry> leaderboard ;
	public static string nicknameChangeLog = String.Empty ;
	


	public static JustFlipClient GetInstance(Type tp)
	{
		if (tp == typeof(LeaderboardInitializer) ||
		    tp == typeof(LoginWindowView) ||
		    tp == typeof(GameBall) ||
		    tp == typeof(ScoreHandler) ||
		    tp == typeof(EMPPanelPriceHandler) ||
		    tp == typeof(Purchaser))
		{
			return JFC ;
		}

		return jfc ;
	}

	public void SetReferencesForJustFlipClient(Text AOS, Text HS)
	{
		if (this.Equals(JFC))
		{
			AmountOfShocks = AOS ;
			HighestScore = HS ;
		}
	}

	public void SetReferenceOfAOSForPricePanel(Text AOS)
	{
		if (this.Equals(JFC))
		{
			AmountOfShocksForPricePanel = AOS ;
		}
	}
	
	
	
	public void TrySubmitScore(int score)
	{
		if (this.Equals(JFC))
		{
			UpdatePlayerStatisticsRequest req = new UpdatePlayerStatisticsRequest();
			req.Statistics = new List<StatisticUpdate>();
			req.Statistics.Add(new StatisticUpdate { StatisticName = "LEADERBOARD", Version = null, Value = score });

			PlayFabClientAPI.UpdatePlayerStatistics(req , null , error =>
			{
				TrySubmitScore(score);
			}) ;
		}
		
	}

	public void GetPlayerDisplayName()
	{
		if (this.Equals(JFC))
		{
			GetAccountInfoRequest req = new GetAccountInfoRequest();
			PlayFabClientAPI.GetAccountInfo(req, result =>
			{
				LeaderboardInitializer.myDisplayName = result.AccountInfo.TitleInfo.DisplayName ;
			},
			error =>
			{
				GetPlayerDisplayName();
               			});
		}
	}


	public void ChangeDisplayName(String displayName)
	{
		if (this.Equals(JFC))
		{
			string currentDisplayName = "NoDisplayName" ;
		
			GetAccountInfoRequest requ = new GetAccountInfoRequest();
			PlayFabClientAPI.GetAccountInfo(requ,
				result =>
				{
					currentDisplayName = result.AccountInfo.TitleInfo.DisplayName ;
				}
				, error =>
				{
					if (error.Error == PlayFabErrorCode.UnableToConnectToDatabase)
					{
						if (Tutorial.languageEnglish)
						{
							nicknameChangeLog = "Please check your internet connection." ;
						}
						else
						{
							nicknameChangeLog = "Lütfen internet bağlantınızı kontrol edin." ;
						}
						
					}
				}
			);
		
			UpdateUserTitleDisplayNameRequest req = new UpdateUserTitleDisplayNameRequest();
			req.DisplayName = displayName ;
			PlayFabClientAPI.UpdateUserTitleDisplayName(req,
				result =>
				{
				
					if (result.DisplayName == currentDisplayName)
					{
						if (Tutorial.languageEnglish)
						{
							nicknameChangeLog = "Nickname assignment failed.";
						}
						else
						{
							nicknameChangeLog = "Nickname atama işlemi başarısız oldu.";
						}
						
					}

					if(currentDisplayName == "NoDisplayName" )
					{
						if (Tutorial.languageEnglish)
						{
							nicknameChangeLog = "Nickname assignment failed.";
						}
						else
						{
							nicknameChangeLog = "Nickname atama işlemi başarısız oldu.";
						}
					}

					if (result.DisplayName != currentDisplayName)
					{
						if (Tutorial.languageEnglish)
						{
							nicknameChangeLog = "Nickname assigned succesfully.";
						}
						else
						{
							nicknameChangeLog = "Nickname atandı.";
						}
					}

				},
				error =>
				{
					if (error.Error == PlayFabErrorCode.NameNotAvailable)
					{
						if (Tutorial.languageEnglish)
						{
							nicknameChangeLog = "This nickname using by another person." ;
						}
						else
						{
							nicknameChangeLog = "Bu nickname başkası tarafından kullanılıyor." ;
						}
				
					}

					if (error.Error == PlayFabErrorCode.UnableToConnectToDatabase)
					{
						if (Tutorial.languageEnglish)
						{
							nicknameChangeLog = "Please check your internet connection." ;
						}
						else
						{
							nicknameChangeLog = "Lütfen internet bağlantınızı kontrol edin." ;
						}
						
					}
				
				}
			);


		}
		

	}
	
	public void TryGetLeaderBoard()
	{
		if (this.Equals(JFC))
		{
			PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest { StatisticName = "LEADERBOARD", StartPosition = 0, MaxResultsCount = 100 },
				(GetLeaderboardResult r) =>
				{
					leaderboard = r.Leaderboard;
					LeaderboardInitializer.leaderboardIsLoaded = true ;

				},
				null);    
		}
		            
	}

	public List<PlayerLeaderboardEntry> GetLeaderBoard()
	{
		if (this.Equals(JFC))
		{
			return leaderboard ;
		}
		
		return new List<PlayerLeaderboardEntry>();
		
	}

	public void GetYourLeaderboardScore()
	{
		// this method calls by PausePanelOpening
		if (this.Equals(JFC))
		{
			GetPlayerStatisticsRequest req = new GetPlayerStatisticsRequest();
			PlayFabClientAPI.GetPlayerStatistics(req, result =>
				{
					for (byte i = 0; i < result.Statistics.Count; i++)
					{
						if (result.Statistics[i].StatisticName == "LEADERBOARD")
						{
							HighestScore.text = result.Statistics[i].Value.ToString() ;
						}
					}
					
				},
				error =>
				{
					GetYourLeaderboardScore();
				});
		}
	}

	
	
	public void GetStatistic(string statisticName,string forWhere)
	{
		if (this.Equals(JFC))
		{
			GetPlayerStatisticsRequest req = new GetPlayerStatisticsRequest();
			PlayFabClientAPI.GetPlayerStatistics(req, result =>
				{
					for (byte i = 0; i < result.Statistics.Count; i++)
					{
						if (result.Statistics[i].StatisticName == statisticName)
						{
							if (statisticName == "TotalCurrentEMPs")
							{
								if (forWhere == "RevivePanel")
								{
									AmountOfShocks.text = result.Statistics[i].Value.ToString() ;
								}
								else if(forWhere == "EMPPricePanel")
								{
									AmountOfShocksForPricePanel.text = result.Statistics[i].Value.ToString() ;
								}
								
								EMPsValue = result.Statistics[i].Value ;
								EMPsValuePulled = true ;
							}
							else if (statisticName == "TotalWatchedAds")
							{
								AdsValue = result.Statistics[i].Value ;
								AdsValuePulled = true ;
							}
							else if (statisticName == "TotalPurchasedEMPs")
							{
								PEMPsValue = result.Statistics[i].Value ;
								PEMPsValuePulled = true ;
							}
							else if (statisticName == "TotalSpentEMPs")
							{
								SEMPsValue = result.Statistics[i].Value ;
								SEMPsValuePulled = true ;
							}
							
						}
					}


				},
				error =>
				{
					GetStatistic(statisticName,forWhere);
				});
		}
	}

	
	public IEnumerator SetStatistic(string statisticName, string forWhere, int changeAmount)
	{
			if (this.Equals(JFC))
			{
				GetStatistic(statisticName,forWhere);

				switch (statisticName)
				{
				case "TotalCurrentEMPs":
					while (!EMPsValuePulled)
					{
						yield return null ;
					}

					EMPsValuePulled = false ;
					break;
				
				case "TotalWatchedAds":
					while (!AdsValuePulled)
					{
						yield return null ;
					}

					AdsValuePulled = false ;
					break ;
				
				case "TotalPurchasedEMPs":
					while (!PEMPsValuePulled)
					{
						yield return null ;
					}

					PEMPsValuePulled = false ;
					break;
				
				case "TotalSpentEMPs":
					while (!SEMPsValuePulled)
					{
						yield return null ;
					}

					SEMPsValuePulled = false ;
					break;	
				}
				
				UpdatePlayerStatisticsRequest req = new UpdatePlayerStatisticsRequest();
				req.Statistics = new List<StatisticUpdate>();

				switch (statisticName)
				{
					case "TotalCurrentEMPs":
						if (changeAmount < 0)
						{
							if (EMPsValue > 0)
							{
								req.Statistics.Add(new StatisticUpdate { StatisticName = statisticName, Version = null, Value = EMPsValue + changeAmount });
							}
						}
						else
						{
							req.Statistics.Add(new StatisticUpdate { StatisticName = statisticName, Version = null, Value = EMPsValue + changeAmount });
						}
							
						break;
					
					case "TotalWatchedAds":
						req.Statistics.Add(new StatisticUpdate { StatisticName = statisticName, Version = null, Value = AdsValue + changeAmount });
						break;
					
					case "TotalPurchasedEMPs":
						req.Statistics.Add(new StatisticUpdate { StatisticName = statisticName, Version = null, Value = PEMPsValue + changeAmount });
						break;
					
					case "TotalSpentEMPs":
						req.Statistics.Add(new StatisticUpdate { StatisticName = statisticName, Version = null, Value = SEMPsValue + changeAmount });
						break;	
				}

				PlayFabClientAPI.UpdatePlayerStatistics(req ,
					result =>
					{
						
					} ,
					error => { }) ;
				
			}

		yield return null ;
	}

	public void SetAllStatisticsForRegister()
	{
		UpdatePlayerStatisticsRequest req = new UpdatePlayerStatisticsRequest();
		req.Statistics = new List<StatisticUpdate>();
		req.Statistics.Add(new StatisticUpdate { StatisticName = "TotalCurrentEMPs", Version = null, Value = 0 });
		req.Statistics.Add(new StatisticUpdate { StatisticName = "TotalWatchedAds", Version = null, Value = 0 });
		req.Statistics.Add(new StatisticUpdate { StatisticName = "TotalPurchasedEMPs", Version = null, Value = 0 });
		req.Statistics.Add(new StatisticUpdate { StatisticName = "TotalSpentEMPs", Version = null, Value = 0 });
		
		PlayFabClientAPI.UpdatePlayerStatistics(req ,
			result =>
			{ } ,
			error =>
			{
				SetAllStatisticsForRegister();
			}) ;
	}

	public void GetTitleDataForPrices()
	{
		if (this.Equals(JFC))
		{
			GetTitleDataRequest req = new GetTitleDataRequest();
			PlayFabClientAPI.GetTitleData(req, result =>
			{
				EMP50Price = int.Parse(result.Data["EMP50"]) ;	
				EMP100Price = int.Parse(result.Data["EMP100"]) ;
				EMP250Price = int.Parse(result.Data["EMP250"]) ;
				EMPPricesPulled = true ;		
			},null);

		}	
	}
	

	public bool GetEMPPulled()
	{
		if (this.Equals(JFC))
		{
			return EMPsValuePulled ;
		}

		return false ;
	}

	public void ResetEMPPulled()
	{
		if (this.Equals(JFC))
		{
			EMPsValuePulled = false ;
		}
	}

}
