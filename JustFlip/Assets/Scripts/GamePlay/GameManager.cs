using System ;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random ;

public class GameManager : MonoBehaviour
{

	public GameObject obstacleEffect ;
	public GameObject lineEffect ;
	public GameObject obstacle ;

	private GameBall gameBall ;
	private Text scoreText ;
	private Text remainTimeText ;
	
	private static GameObject[] obstacleEffectInstances ;
	private static GameObject lineEffectInstance ;
	private static GameObject[] obstacleInstances ;
	private static byte obstacleEffectCreateOrderIndice ;
	private static byte obstacleCreateOrderIndice ;
	private static byte randomSpawnPositionArrayIndice ;
	private static Vector3[] positionOfLines ;
	private static Vector3[] spawnPositionsOfObtacle ;
	private static byte[] randomSpawnPositionArray ;
	private static float obstacleSpeed ;
	private static float obstacleSpawnRate ;

	[HideInInspector] private static float score ;
	[HideInInspector] private static float remainTime ;
	[HideInInspector] private static float maxRemainTime ;
	[HideInInspector] private static bool countDownIsActive ;
	[HideInInspector] private static byte counterForDestroyerMode ;
	[HideInInspector] public static GameObject backLightningInstance ;
	[HideInInspector] private static bool stopUIValues ;
	[HideInInspector] public static AudioManager audioManager ;
	
	private bool isFirstCreation = true ;

	
	
	void Start ()
	{
		
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>() ;
		stopUIValues = false ;
		countDownIsActive = false ;
		score = 0f ;
		maxRemainTime = 3f ;
		remainTime = 3f ;
		obstacleSpeed = 50f ;
		obstacleSpawnRate = 1.5f;
		isFirstCreation = false ;
		Obstacle.isDestroyerModeActive = false ;
		Obstacle.destroyGameBall = false ;
		counterForDestroyerMode = 0 ;

		positionOfLines=new Vector3[5];
		positionOfLines[0] = new Vector3(-49.99f , -8.4f , 0f) ;
		positionOfLines[1] = new Vector3(-24.69f , -8.4f , 0f) ;
		positionOfLines[2] = new Vector3(0f , -8.4f , 0f) ;
		positionOfLines[3] = new Vector3(25.31f , -8.4f , 0f) ;
		positionOfLines[4] = new Vector3(50.31f , -8.4f , 0f) ;

		
		spawnPositionsOfObtacle=new Vector3[5];
		spawnPositionsOfObtacle[0] = new Vector3(-49.99f , 80.3f , 0f) ;
		spawnPositionsOfObtacle[1] = new Vector3(-24.69f , 80.3f , 0f) ;
		spawnPositionsOfObtacle[2] = new Vector3(0f , 80.3f , 0f) ;
		spawnPositionsOfObtacle[3] = new Vector3(25.31f , 80.3f , 0f) ;
		spawnPositionsOfObtacle[4] = new Vector3(50.31f , 80.3f , 0f) ;
		


		
		obstacleInstances=new GameObject[30];
        for (int i = 0; i < obstacleInstances.Length; i++)
        {
            obstacleInstances[i] = Instantiate(obstacle , Vector3.up*160 , Quaternion.identity) ;
		    obstacleInstances[i].SetActive(false);
        }
		
		
		FillRandomDestinationOfObtacles(obstacleInstances);
		DisorderDestinationOfObstacles(obstacleInstances);
		DisorderDestinationOfObstacles(obstacleInstances);
		DisorderDestinationOfObstacles(obstacleInstances);


		obstacleEffectInstances=new GameObject[8];
		for (int i = 0; i < obstacleEffectInstances.Length; i++)
		{
			obstacleEffectInstances[i] = Instantiate(obstacleEffect , Vector3.zero , Quaternion.identity) ;
			obstacleEffectInstances[i].SetActive(false);
		}
		
		

		lineEffectInstance = Instantiate(lineEffect , Vector3.zero , Quaternion.identity) ;
		lineEffectInstance.SetActive(false);


		gameBall = Obstacle.gameBall.GetComponent<GameBall>() ;
		scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>() ;
		remainTimeText = GameObject.Find("Canvas").transform.Find("RemainTime").GetComponent<Text>() ;
		

		randomSpawnPositionArray = CreateRandomNumbersArray() ;
		DisorderRandomNumberArray(randomSpawnPositionArray);
		DisorderRandomNumberArray(randomSpawnPositionArray);
		DisorderRandomNumberArray(randomSpawnPositionArray);

		obstacleEffectCreateOrderIndice = 0 ;
		obstacleCreateOrderIndice = 0 ;
		randomSpawnPositionArrayIndice = 0 ;
		
		StartCoroutine(CalculateSpawnProperties()) ;
		StartCoroutine(StartDisorderLoopForSpawn()) ;
		StartCoroutine(StartDisorderLoopForDestinations()) ;
		StartCoroutine(SpawnObtacles()) ;
		StartCoroutine(CalculateUIValuesAndWrite()) ;
		StartCoroutine(destroyGameBall()) ;
	}



	private void OnDisable()
	{
		backLightningInstance.transform.localPosition=Vector3.zero;
		backLightningInstance.SetActive(false);
		Obstacle.isDestroyerModeActive = false ;
		counterForDestroyerMode = 0 ;
		countDownIsActive = false ;
		stopUIValues = false ;
		score = 0f ;
		remainTime = 3f ;
		maxRemainTime = 3f ;
		obstacleSpeed = 50f ;
		obstacleSpawnRate = 1.5f;

		scoreText.text = "0" ;
		remainTimeText.text = "" ;
		
		for (int i = 0; i < obstacleInstances.Length; i++)
		{
			obstacleInstances[i].transform.localPosition =Vector3.up*160;
			obstacleInstances[i].SetActive(false);
		}
		
		FillRandomDestinationOfObtacles(obstacleInstances);
		DisorderDestinationOfObstacles(obstacleInstances);
		DisorderDestinationOfObstacles(obstacleInstances);
		DisorderDestinationOfObstacles(obstacleInstances);
		
		for (int i = 0; i < obstacleEffectInstances.Length; i++)
		{
			obstacleEffectInstances[i].SetActive(false);
		}
		
		lineEffectInstance.SetActive(false);
		
		randomSpawnPositionArray = CreateRandomNumbersArray() ;
		DisorderRandomNumberArray(randomSpawnPositionArray);
		DisorderRandomNumberArray(randomSpawnPositionArray);
		DisorderRandomNumberArray(randomSpawnPositionArray);

		obstacleEffectCreateOrderIndice = 0 ;
		obstacleCreateOrderIndice = 0 ;
		randomSpawnPositionArrayIndice = 0 ;
		
		
		StopCoroutine(CalculateSpawnProperties()) ;
		StopCoroutine(StartDisorderLoopForSpawn()) ;
		StopCoroutine(StartDisorderLoopForDestinations()) ;
		StopCoroutine(SpawnObtacles()) ;
		StopCoroutine(CalculateUIValuesAndWrite());
		StopCoroutine(destroyGameBall());
		
	}

	private void OnEnable()
	{
		
		if (!isFirstCreation)
		{
			gameBall = Obstacle.gameBall.GetComponent<GameBall>() ;
			StartCoroutine(CalculateUIValuesAndWrite()) ;
			StartCoroutine(CalculateSpawnProperties()) ;
			StartCoroutine(StartDisorderLoopForSpawn()) ;
			StartCoroutine(StartDisorderLoopForDestinations()) ;
			StartCoroutine(SpawnObtacles()) ;
			StartCoroutine(destroyGameBall()) ;
			backLightningInstance.SetActive(true) ;
		}
		
		
	}


	public static byte TakeNumberFromSpawnArray()
	{
		if (randomSpawnPositionArrayIndice >= randomSpawnPositionArray.Length - 1)
		{
			randomSpawnPositionArrayIndice = 0 ;
			return randomSpawnPositionArray[randomSpawnPositionArray.Length - 1] ;
			
		}
		else
		{
			randomSpawnPositionArrayIndice++ ;
			return randomSpawnPositionArray[randomSpawnPositionArrayIndice-1] ;
			
		}
	}

	public static GameObject CreateObstacleEffect(Vector3 position)
	{
		obstacleEffectInstances[obstacleEffectCreateOrderIndice].SetActive(true);
		obstacleEffectInstances[obstacleEffectCreateOrderIndice].transform.localPosition = position ;

		if (obstacleEffectCreateOrderIndice >= obstacleEffectInstances.Length - 1)
		{
			obstacleEffectCreateOrderIndice = 0 ;
			return obstacleEffectInstances[obstacleEffectInstances.Length-1] ;
		}
		else
		{
			obstacleEffectCreateOrderIndice++ ;
			return obstacleEffectInstances[obstacleEffectCreateOrderIndice-1] ;
		}
    
		
	}


	public static GameObject CreateLineEffect(byte indice, bool isReverse)
	{
		if (isReverse)
		{
			lineEffectInstance.GetComponent<LineEffect>().reverseBehaviour = true ;
			lineEffectInstance.GetComponent<EffectEvents>().delayTime = 0.2f ;
		}
		else
		{
			lineEffectInstance.GetComponent<LineEffect>().reverseBehaviour = false ;
			lineEffectInstance.GetComponent<EffectEvents>().delayTime = 1f ;
		}
		
		lineEffectInstance.SetActive(true);
		lineEffectInstance.transform.localPosition = positionOfLines[indice] + Vector3.up * -1.5f ;


		return lineEffectInstance ;
	}

	public static void CreateBackLightning(byte indice)
	{
		backLightningInstance.SetActive(true);
		switch (indice)
		{
		case 0 :
			backLightningInstance.transform.localPosition=Vector3.right*(-50f);
			break;
		case 1 :
			backLightningInstance.transform.localPosition = Vector3.right * (-24.7f) ;
			break;
		case 2:
			backLightningInstance.transform.localPosition = Vector3.zero;
			break;
		case 3:
			backLightningInstance.transform.localPosition=Vector3.right*(25.3f);
			break;
		case 4:
			backLightningInstance.transform.localPosition = Vector3.right * (50.3f) ;
			break;
		}
		
	}

	public IEnumerator CreateObstacle(byte indice)
	{
		obstacleInstances[obstacleCreateOrderIndice].transform.localPosition = spawnPositionsOfObtacle[indice] ;
		obstacleInstances[obstacleCreateOrderIndice].SetActive(true);

		while (obstacleInstances[obstacleCreateOrderIndice].transform.localScale.sqrMagnitude>3)
		{
			obstacleInstances[obstacleCreateOrderIndice].transform.localScale-=Vector3.right*(8f)*Time.deltaTime+Vector3.up*(8f)*Time.deltaTime;
			yield return null ;
		}
		
		
		obstacleInstances[obstacleCreateOrderIndice].transform.localScale=Vector3.one;
		obstacleInstances[obstacleCreateOrderIndice].transform.Find("ObstacleTriangleOrange")
				.GetComponent<SpriteRenderer>().sprite = obstacleInstances[obstacleCreateOrderIndice]
			.GetComponent<Obstacle>().triangleOrange_Glow ;
		obstacleInstances[obstacleCreateOrderIndice].GetComponent<Obstacle>().obstacleSpeed = obstacleSpeed;
		if (obstacleCreateOrderIndice >= obstacleInstances.Length - 1)
		{
			obstacleCreateOrderIndice = 0 ;
		}
		else
		{
			obstacleCreateOrderIndice++ ;
		}
	}

	private IEnumerator CalculateUIValuesAndWrite()
	{
		while (true)
		{

			if (!stopUIValues)
			{

				score += Time.deltaTime ;
				if (countDownIsActive)
				{
					remainTime -= Time.deltaTime ;
				}

				if (!scoreText.text.Equals(((int) score).ToString()))
				{
					scoreText.text = ((int) score).ToString() ;
				}

				if (!remainTimeText.text.Equals((remainTime).ToString("F1")) && countDownIsActive)
				{
					remainTimeText.text = (remainTime).ToString("F1") ;
				}
				else if (!countDownIsActive)
				{
					if (!remainTimeText.text.Equals(""))
					{
						remainTimeText.text = "" ;
					}
				}

				if (remainTime <= 0f)
				{
					remainTimeText.text = "0" ;
					if (gameBall.transform.localScale.sqrMagnitude >= 0.05f)
					{
						StartCoroutine(gameBall.DestroyYourSelf()) ;
						GameObject.Find("Camera").GetComponent<Animator>().enabled = true ;
						GameObject.Find("Camera").GetComponent<Animator>().SetTrigger("ShakeCamera") ;

						ReviverShockScreenTouchInput RSSTI = GameObject.Find("Canvas").transform.Find("ReviverShockScreen")
                        
							.GetComponent<ReviverShockScreenTouchInput>() ;
						
                        while (!RSSTI.GetUICheck())
                        {
                        	yield return null ;
                        }
                        
						
                        RSSTI.ResetUICheck();
                        if (RSSTI.GetNoThanksButtonClicked())
                        {   
                        	break;
                        }
					}
				}
			}

			yield return null ;
		}

	}

	private IEnumerator SpawnObtacles()
	{
		while (true)
		{
			StartCoroutine(CreateObstacle(TakeNumberFromSpawnArray()));
			yield return new WaitForSeconds(obstacleSpawnRate);
		}
	}

	private byte[] CreateRandomNumbersArray()
	{
		byte[] randomNumbers=new byte[30];
		for (byte i = 0; i < randomNumbers.Length; i++)
		{
			randomNumbers[i] = (byte)((i+1)%5) ;
		}

		return randomNumbers ;
	}

	private void DisorderRandomNumberArray(byte[] RNArray)
	{
		byte[] mixerArray=new byte[20];
		byte temp ;
		for (byte i = 0; i < mixerArray.Length; i++)
		{
			mixerArray[i] = (byte)Random.Range(0 , 29) ;
		}

		for (int i = 0; i < 19; i++)
		{
			temp = RNArray[mixerArray[i]] ;
			RNArray[mixerArray[i]] = RNArray[mixerArray[i + 1]] ;
			RNArray[mixerArray[i + 1]] = temp ;
		}
	}
	
	private IEnumerator StartDisorderLoopForSpawn()
	{
		while (true)
		{
			DisorderRandomNumberArray(randomSpawnPositionArray);
			yield return new WaitForSeconds(Random.Range(12 , 20)) ;
		}
	}

	private void FillRandomDestinationOfObtacles(GameObject[] obstacleInstancesParam)
	{
		byte[] tempDestinations=new byte[obstacleInstancesParam.Length];
		for (byte i = 0; i < tempDestinations.Length; i++)
		{
			tempDestinations[i] = (byte) ((30 - i) % 5) ;
		}
		
		for (byte i = 0; i < obstacleInstancesParam.Length; i++)
		{
			obstacleInstancesParam[i].GetComponent<Obstacle>().gameBallDestination = tempDestinations[i] ;
		}
	}

	private void DisorderDestinationOfObstacles(GameObject[] obstacleInstancesParam)
	{
		byte[] mixerArray=new byte[20];
		byte temp ;
		for (byte i = 0; i < mixerArray.Length; i++)
		{
			mixerArray[i] = (byte)Random.Range(0 , 29) ;
		}

		for (int i = 0; i < 19; i++)
		{
			temp = obstacleInstancesParam[mixerArray[i]].GetComponent<Obstacle>().gameBallDestination ;
			obstacleInstancesParam[mixerArray[i]].GetComponent<Obstacle>().gameBallDestination =obstacleInstancesParam[mixerArray[i + 1]]
				.GetComponent<Obstacle>().gameBallDestination ;
			obstacleInstancesParam[mixerArray[i + 1]].GetComponent<Obstacle>().gameBallDestination = temp ;
		}
	}
	
	private IEnumerator StartDisorderLoopForDestinations()
	{
		while (true)
		{
			DisorderDestinationOfObstacles(obstacleInstances);
			yield return new WaitForSeconds(Random.Range(12 , 20)) ;
		}
	}


	private IEnumerator CalculateSpawnProperties()
	{
		while (true)
		{
			if (obstacleSpeed < 106)
			{
				obstacleSpeed += 2f*Time.deltaTime ;
			}
			

			if (obstacleSpawnRate > 0.242)
			{
				obstacleSpawnRate -= 0.0629f * Time.deltaTime ;
			}

			if (maxRemainTime > 1.0f)
			{
				maxRemainTime -= 0.01f*Time.deltaTime ;
			}

			if (obstacleSpeed >= 106 && obstacleSpawnRate <= 0.242 && maxRemainTime<=1.0f)
			{
				obstacleSpeed = 106 ;
				obstacleSpawnRate = 0.24f ;
				maxRemainTime = 1 ;
				break;
			}
			
			yield return null ;
		}
		
	}

	private IEnumerator destroyGameBall()
	{
		while (true)
		{
			if (Obstacle.destroyGameBall)
			{
				StartCoroutine(gameBall.DestroyYourSelf()) ;
				GameObject.Find("Camera").GetComponent<Animator>().enabled = true ;
				GameObject.Find("Camera").GetComponent<Animator>().SetTrigger("ShakeCamera");
				Obstacle.destroyGameBall = false ;
				ReviverShockScreenTouchInput RSSTI = GameObject.Find("Canvas").transform.Find("ReviverShockScreen")
					.GetComponent<ReviverShockScreenTouchInput>() ;
				
				while (!RSSTI.GetDestoryGameBallCheck())
				{
					yield return null ;
				}
				
				RSSTI.ResetDestroyGameBallCheck();
				
				RSSTI.ResetDestroyGameBallCheck();
				if (RSSTI.GetNoThanksButtonClicked())
				{
					break;
				}
				
			}

			yield return null ;

		}
		
	}

	public static float GetScore(Type tp)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall) || tp==typeof(JustFlipClient) || tp==typeof(ScoreHandler))
		{
			return score ;
		}
		
		return 0f ;
		
		
	}
	public static float GetRemainTime(Type tp)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			return remainTime ;
		}
		
		return 0f ;
		
	}
	public static float GetMaxRemainTime(Type tp)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			return maxRemainTime ;
		}
		
		return 0f ;
	}

	public static bool GetCountDownIsActive(Type tp)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			return countDownIsActive ;
		}
		
		return true ;
	}
	
	public static byte GetCounterForDestroyerMode(Type tp)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			return counterForDestroyerMode ;
		}
		
		return 3 ;
		
	}

	public static bool GetStopUIValues(Type tp)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			return stopUIValues ;
		}
		
		return true ;
		
	}

	public static void SetScore(Type tp,float value)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			score = value ;
		}
		
	}
	
	public static void SetRemainTime(Type tp,float value)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			remainTime = value ;
		}
		
	}
	
	public static void SetMaxRemainTime(Type tp,float value)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			maxRemainTime = value ;
		}
		
	}
	
	public static void SetCountDownIsActive(Type tp,bool value)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			countDownIsActive = value ;
		}
		
	}

	public static void SetCounterForDestroyerMode(Type tp , byte value)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			counterForDestroyerMode = value ;
		}
		
	}

	public static void SetStopUIValues(Type tp , bool value)
	{
		if (tp==typeof(Obstacle) || tp==typeof(GameBall))
		{
			stopUIValues = value ;
		}
	}

	public static void ResetStopUIValues()
	{
		stopUIValues = false ;
	}
	

	
}
