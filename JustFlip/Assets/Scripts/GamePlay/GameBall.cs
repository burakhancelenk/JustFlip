using System.Collections;
using UnityEngine;
using UnityEngine.UI ;

public class GameBall : MonoBehaviour {

	private Vector3[] ballPositions=new Vector3[5];
	private float elapsedTime ;
	private byte cameraShakeMagnitudeMultiplier ;
	private GameObject instanceBallLandingEffect ;
	
	
	[HideInInspector] public static byte destination ;
	[HideInInspector] public static bool isSwitchingPosition ;
	[HideInInspector] public static byte currentPositionIndice ;

	public GameObject reviverShockScreen ;
	public GameObject ballLandingBlastSparkEffect ;
	public GameObject ballDestroyEffect ;
	public Sprite blueCircle_Glow ;
	public Sprite blueCircle ;
	public ParticleSystem ballCircleElectricityEffectPS;
	public ParticleSystem blueSparkEffectRight ;
	public ParticleSystem blueSparkEffectMiddle ;
	public ParticleSystem blueSparkEffectLeft ;
	public ParticleSystem lightningEffect1 ;
	public ParticleSystem lightningEffect2 ;
	public ParticleSystem lightningEffect3 ;
	public ParticleSystem lightningEffect4 ;
	public ParticleSystem lightningEffect5 ;
	
	void Start ()
	{
		isSwitchingPosition = false ;
		LineTouchInput.gameBallDestroyed = false ;
		destination = 2 ;
		currentPositionIndice = 2 ;
		ballPositions[0]=new Vector3(-50f,-54,0);
		ballPositions[1]=new Vector3(-24.69f,-54,0);
		ballPositions[2]=new Vector3(0,-54,0);
		ballPositions[3]=new Vector3(25.31f,-54,-0);
		ballPositions[4]=new Vector3(50.3f,-54,0);


		if (PausePanelEvents.buttonName == "Restart")
		{
			transform.localPosition = ballPositions[2] ;
		}

		reviverShockScreen = GameObject.Find("Canvas").transform.Find("ReviverShockScreen").gameObject ;
	}

	private void OnDisable()
	{
		isSwitchingPosition = false ;
		destination = 2 ;
		currentPositionIndice = 2 ;
		if (PausePanelEvents.buttonName == "Restart")
		{
			transform.localPosition = ballPositions[currentPositionIndice] ;
		}
		else
		{
			transform.localPosition = Vector3.zero ;
		}

		if (PausePanelEvents.buttonName != "Restart")
		{
			GetComponent<Animator>().enabled = true ;
		}
		else
		{
			GetComponent<Animator>().enabled = false ;
		}
		
	}


	public IEnumerator ChangePosition(byte targetPositionIndice)
	{
		
		if (currentPositionIndice != targetPositionIndice)
		{
			isSwitchingPosition = true ;
			elapsedTime = 0f ;
			
			GameManager.backLightningInstance.SetActive(false);
			GameManager.audioManager.PlaySoundFX("BallChangeEffect",0.25f*(Mathf.Abs(currentPositionIndice-targetPositionIndice)));
			AudioManager.railFrictionEffect.Stop();
			GameManager.SetCounterForDestroyerMode(this.GetType(),0);
			
			if (Obstacle.isDestroyerModeActive)
			{
				Obstacle.isDestroyerModeActive = false ;
			}
			
			blueSparkEffectLeft.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			blueSparkEffectMiddle.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			blueSparkEffectRight.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect1.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect2.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect3.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect4.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect5.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			transform.Find("BlueCircleImage").GetComponent<SpriteRenderer>().sprite = blueCircle_Glow ;
			transform.Find("BlueCircleImage").localPosition+=Vector3.right*(-0.15f)+Vector3.up*(-0.21f);
			StartCoroutine(Obstacle.lines.GetComponent<Lines>().SetToNormalFromSizzle(currentPositionIndice)) ;


			float firstPositionX = transform.localPosition.x ;


			float ScaleChangeRate =
				0.3f / (((transform.localPosition.x - ballPositions[targetPositionIndice].x) / 2f)
				         / ((ballPositions[targetPositionIndice].x - transform.localPosition.x) * Time.unscaledDeltaTime /
				            0.15f)) ;


			while (elapsedTime < 0.15f)
			{
				yield return null ;
				// this is the place where we change the scale of this gameobject and ballCircleElectricityEffect
				if (elapsedTime < 0.075f)
				{
					if (ballCircleElectricityEffectPS.emissionRate > 0)
					{
						ballCircleElectricityEffectPS.emissionRate -= 4 ;
					}
					
					transform.localScale -= ScaleChangeRate * (Vector3.right + Vector3.up) ;
					ballCircleElectricityEffectPS.transform.localScale -= ScaleChangeRate * (Vector3.right + Vector3.forward) ;
				}
				else
				{
					if (ballCircleElectricityEffectPS.emissionRate < 23)
					{
						ballCircleElectricityEffectPS.emissionRate += 4 ;
					}
					
					transform.localScale += ScaleChangeRate * (Vector3.right + Vector3.up) ;
					ballCircleElectricityEffectPS.transform.localScale += ScaleChangeRate * (Vector3.right + Vector3.forward) ;
				}

				transform.localPosition +=
					((ballPositions[targetPositionIndice].x - firstPositionX) * Time.unscaledDeltaTime / 0.15f) * Vector3.right ;
				elapsedTime += Time.unscaledDeltaTime ;
				
			}
			transform.Find("BlueCircleImage").GetComponent<SpriteRenderer>().sprite = blueCircle ;
			transform.Find("BlueCircleImage").localPosition += Vector3.right*(0.15f)+Vector3.up*(0.21f);
			cameraShakeMagnitudeMultiplier = (byte)Mathf.Abs(targetPositionIndice - currentPositionIndice) ;
			currentPositionIndice = targetPositionIndice ;
			ballCircleElectricityEffectPS.transform.localScale=Vector3.one;
			transform.localScale = Vector3.one ;
			transform.localPosition = ballPositions[targetPositionIndice] ;
			ballCircleElectricityEffectPS.emissionRate = 23 ;
			GameManager.SetCountDownIsActive(this.GetType(),false);
			
			isSwitchingPosition = false ;

			blueSparkEffectLeft.Play() ;
			blueSparkEffectMiddle.Play() ;
			blueSparkEffectRight.Play() ;
			lightningEffect1.Play() ;
			lightningEffect2.Play() ;
			lightningEffect3.Play() ;
			lightningEffect4.Play() ;
			lightningEffect5.Play() ;
			PlayLandingEffect();
			AudioManager.railFrictionEffect.Play();
			if (currentPositionIndice==destination)
			{
				GameManager.CreateLineEffect(currentPositionIndice , false) ;
				GameManager.CreateBackLightning(currentPositionIndice);
				Obstacle.lines.GetComponent<Lines>().TurnOffHighLightedLine(currentPositionIndice);
				StartCoroutine(Obstacle.lines.GetComponent<Lines>().CreateSizzleEffect(currentPositionIndice)) ;
				StartCoroutine(GameObject.Find("Camera").GetComponent<CameraShakeEffect>().ShakeCamera(1.25f*cameraShakeMagnitudeMultiplier,0.15f)) ;
				
			}
			else
			{
				GameObject.Find("Camera").GetComponent<Animator>().enabled = true ;
				GameObject.Find("Camera").GetComponent<Animator>().SetTrigger("ShakeCamera");
				StartCoroutine(DestroyYourSelf());
			}
			
		}
		
	}
	
	
	
	
	
	private void PlayLandingEffect()
	{
		
		if (instanceBallLandingEffect == null)
		{
			instanceBallLandingEffect = Instantiate(ballLandingBlastSparkEffect , transform.localPosition , Quaternion.identity) ;
		}
		else
		{
			instanceBallLandingEffect.SetActive(true);
			instanceBallLandingEffect.transform.localPosition = transform.localPosition ;
			instanceBallLandingEffect.GetComponent<ParticleSystem>().Play();
		}
		
	}


	private void SetPositionAndDisableAnimator()
	{
		
		GetComponent<Animator>().enabled = false ;
		transform.localPosition = Vector3.up*(-54f);
		

	}

	public IEnumerator DestroyYourSelf()
	{
		LineTouchInput.gameBallDestroyed = true ;
		GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 10 ;
		ScoreHandler.getEMPValues = true ;
		reviverShockScreen.SetActive(true);
		reviverShockScreen.transform.Find("ReviveButton").gameObject.SetActive(false);
		reviverShockScreen.transform.Find("NoThanksButton").gameObject.SetActive(false);
		Time.timeScale = 0 ;
		
		Text numberOfEMPs = reviverShockScreen.transform.Find("AmountOfShocks").GetComponent<Text>();
		bool notEnoughEMP = false ;
		numberOfEMPs.gameObject.SetActive(false);
		AudioManager.railFrictionEffect.Stop();
		
		while (!JustFlipClient.GetInstance(GetType()).GetEMPPulled() || string.IsNullOrEmpty(numberOfEMPs.text))
		{
			yield return null ;
		}
		
		numberOfEMPs.gameObject.SetActive(true);
		JustFlipClient.GetInstance(GetType()).ResetEMPPulled();
		
		if (int.Parse(numberOfEMPs.text) <= 0)
		{
			notEnoughEMP = true ;
		}
		
		reviverShockScreen.transform.Find("ReviveButton").gameObject.SetActive(true);
		reviverShockScreen.transform.Find("NoThanksButton").gameObject.SetActive(true);

		while (!reviverShockScreen.GetComponent<ReviverShockScreenTouchInput>().GetReviveButtonClicked() &&
		       !reviverShockScreen.GetComponent<ReviverShockScreenTouchInput>().GetNoThanksButtonClicked()
		       )
		{
			yield return null ;
		}
		
		GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 0 ;
		reviverShockScreen.SetActive(false);
		Time.timeScale = 1 ;

		if (reviverShockScreen.GetComponent<ReviverShockScreenTouchInput>().GetReviveButtonClicked() && !notEnoughEMP)
		{
			LineTouchInput.gameBallDestroyed = false ;
			ScoreHandler.setEMPValues = true ;
			GameManager.SetRemainTime(GetType(),GameManager.GetMaxRemainTime(GetType()));
			GameManager.ResetStopUIValues();
			
			GameManager.backLightningInstance.transform.localPosition=Vector3.zero;
			GameManager.backLightningInstance.SetActive(true);
			AudioManager.railFrictionEffect.Play();

			
			
			if (!isSwitchingPosition)
			{
				StartCoroutine(Obstacle.lines.GetComponent<Lines>().SetToNormalFromSizzle(currentPositionIndice)) ;
			}

			for (byte i = 0; i < 5; i++)
			{
				if (Obstacle.lines.transform.Find("Line" + (i + 1)).GetComponent<SpriteRenderer>().sprite ==
				    Obstacle.lines.GetComponent<Lines>().lineImage_HighGlow)
				{
					Obstacle.lines.GetComponent<Lines>().TurnOffHighLightedLine(i);
				} 
			}

			LineTouchInput.gameBallDestroyed = false ;
			currentPositionIndice = 2 ;
			destination = 2 ;
			gameObject.transform.localPosition = ballPositions[currentPositionIndice] ;
			isSwitchingPosition = false ;
			GameManager.SetCountDownIsActive(GetType(),false);
			GameManager.SetCounterForDestroyerMode(GetType(),0);
			Obstacle.isDestroyerModeActive = false ;
			Obstacle.destroyGameBall = false ;

			GameObject.Find("Camera").GetComponent<Animator>().enabled = false ;
			yield return new WaitForSeconds(0.2f);
			Obstacle.lines.GetComponent<Lines>().CreateSizzleEffectForRestart();

		}
		else if (reviverShockScreen.GetComponent<ReviverShockScreenTouchInput>().GetNoThanksButtonClicked() || notEnoughEMP)
		{
		    GameManager.SetStopUIValues(this.GetType(),true);
		    GameManager.audioManager.PlaySoundFX("BallDestroyEffect",1f);
		    AudioManager.railFrictionEffect.Stop();

		    GameObject.Find("PauseLogo(Clone)").GetComponent<BoxCollider2D>().enabled = false ;
		
		    Instantiate(ballDestroyEffect , transform.localPosition , Quaternion.identity) ;
		    gameObject.transform.localScale=Vector3.zero;
		    GameManager.backLightningInstance.SetActive(false);
			
		
			RestartButtonTouchInput.gameBallCurrentPos = currentPositionIndice ;
			RestartButtonTouchInput.gameBallDest = destination ;

			if (PausePanelEvents.pauseButton == null)
			{
				PausePanelEvents.pauseButton=GameObject.Find("PauseLogo(Clone)");
			}

			PausePanelEvents.pauseButton.GetComponent<BoxCollider2D>().enabled = false ;

			GameObject.Find("Canvas").transform.Find("HighestScore").GetComponent<Text>().text =
			PlayerPrefs.GetInt("HighestScore").ToString() ;
		
		// this is for close interact with the obstacles and lines
			Obstacle.lines.GetComponent<LinesEvents>().SwitchInteract();
			isSwitchingPosition = true ;
		
			blueSparkEffectLeft.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			blueSparkEffectMiddle.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			blueSparkEffectRight.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect1.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect2.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect3.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect4.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			lightningEffect5.Stop(false , ParticleSystemStopBehavior.StopEmitting) ;
			ballCircleElectricityEffectPS.Stop(false,ParticleSystemStopBehavior.StopEmitting);
			GameLogoEvents.isFinalPanel = true ;
			GameObject.Find("Canvas").transform.Find("FinalScore").GetComponent<Text>().text = ((int) GameManager.GetScore(this.GetType())).ToString() ;
			yield return new WaitForSeconds(2f);
			GameObject.Find("PauseLogo(Clone)").GetComponent<PauseButtonTouchInput>().CreatePausePanel();
			GameObject.Find("PauseLogo(Clone)").SetActive(false);
		
			Destroy(gameObject);
		}
        reviverShockScreen.GetComponent<ReviverShockScreenTouchInput>().ResetAllButtonClickedTriggers();
	}
	
}
