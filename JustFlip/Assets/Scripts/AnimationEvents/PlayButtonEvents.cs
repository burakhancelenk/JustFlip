using System.Collections;
using UnityEngine;

public class PlayButtonEvents : MonoBehaviour
{

	public GameObject loopingSparkEffect ;
	public GameObject triangleBlastSparkEffect ;
	public GameObject soundLogo ;
	public GameObject facebookLogo ;
	public GameObject twitterLogo ;
	public GameObject instagramLogo ;
	public GameObject scoreBoardLogo ;
	public GameObject empLogo ;
	public GameObject gameBall ;
	public GameObject lines ;
	public GameObject gameManager ;
	

	[HideInInspector] public bool isThisFirstPlayButton ;
	
    public void CallShakeCamera()
	{
		StartCoroutine(GameObject.Find("Camera").GetComponent<CameraShakeEffect>().ShakeCamera(5f,0.2f)) ;   
	}
	
	public IEnumerator CreateSparkAndPlayAnimation(string sparkType)
	{
		
		if (sparkType=="circleOrange")
		{
			GameObject temp = Instantiate(loopingSparkEffect , Vector3.zero , Quaternion.identity) ;
			temp.GetComponent<Animator>().enabled = false ;
			
			Vector3 tempVectorX = new Vector3(1,0,0);
			Vector3 tempVectorY = new Vector3(0,1,0);
			float tempAngle = 90f ;
			while (tempAngle>(-270))
			{
				
				temp.transform.localPosition =  24.3f*(Mathf.Cos(tempAngle*(Mathf.PI/180f)) * tempVectorX+Mathf.Sin(tempAngle*(Mathf.PI/180f))*tempVectorY) ;
				tempAngle -= 360f*Time.deltaTime ;
				yield return null ;
			}
			temp.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting) ;
		}
		
		else if (sparkType=="circleBlast")
		{
			Instantiate(triangleBlastSparkEffect , Vector3.zero , Quaternion.identity) ;
		}
		
	}

	public void CreateOtherButtons()
	{
		if (isThisFirstPlayButton)
		{
			Instantiate(soundLogo , soundLogo.transform.localPosition , Quaternion.identity) ;
			Instantiate(facebookLogo , facebookLogo.transform.localPosition , Quaternion.identity) ;
			Instantiate(twitterLogo , twitterLogo.transform.localPosition , Quaternion.identity) ;
			Instantiate(instagramLogo , instagramLogo.transform.localPosition , Quaternion.identity) ;
			Instantiate(scoreBoardLogo , scoreBoardLogo.transform.localPosition , Quaternion.identity) ;
			Instantiate(empLogo , empLogo.transform.localPosition , Quaternion.identity) ;
		}
		else
		{
			GameLogoEvents.facebookLogoInstance.SetActive(true);
			GameLogoEvents.twitterLogoInstance.SetActive(true);
			GameLogoEvents.instagramLogoInstance.SetActive(true);
			GameLogoEvents.soundLogoInstance.SetActive(true);
			GameLogoEvents.scoreBoardLogoInstance.SetActive(true);
			GameLogoEvents.empLogoInstance.SetActive(true);
		}
		
	}

	public void SwitchInteract()
	{
		if (gameObject.GetComponent<CircleCollider2D>().enabled == false)
		{
			gameObject.GetComponent<CircleCollider2D>().enabled = true ;
		}
		else
		{
			gameObject.GetComponent<CircleCollider2D>().enabled = false ;
		}
		
	}

	public void CreateGameBall()
	{
		
		if (isThisFirstPlayButton)
		{
			Obstacle.gameBall = Instantiate(gameBall , Vector3.zero , Quaternion.identity) ;
			LineTouchInput.gameBall = Obstacle.gameBall ;
		}
		else
		{
			Obstacle.gameBall.SetActive(true);
		}
		
		GameObject temp = Instantiate(triangleBlastSparkEffect , Vector3.zero , Quaternion.identity) ;
		temp.transform.localScale=(Vector3.right+Vector3.up)*0.8f;
		
	}


	public void CreateGameManager()
	{
		if (isThisFirstPlayButton)
		{
			Instantiate(gameManager , Vector3.zero , Quaternion.identity) ;
		}
		else
		{
			GameLogoEvents.gameManager.SetActive(true);
		}
		
	}

	public void CreateLines()
	{
		if (isThisFirstPlayButton)
		{
			Instantiate(lines , lines.transform.localPosition , Quaternion.identity)
				.GetComponent<Animator>().SetTrigger("StartGame") ;
		}
		else
		{
			Obstacle.lines.SetActive(true);
			Obstacle.lines.GetComponent<Animator>().enabled = true ;
			Obstacle.lines.GetComponent<Animator>().SetTrigger("StartGame");
		}
		
	}

	public void SetTrueIsFirstPlayButtton()
	{
		isThisFirstPlayButton = true ;
	}


	public void DestroyYourSelf()
	{
		Destroy(gameObject);
	}

	public void PlayMainMenuMusic()
	{
		AudioManager.mainMenuMusicActive = true ;
	}

	public void PlayGamePlayMusic()
	{
		AudioManager.gamePlayMusicActive = true ;
	}

	public void StopMusic()
	{
		AudioManager.stopMusicActive = true ;
	}
	
	public void PlayFx(string fxName)
	{
		if (GameManager.audioManager==null)
		{
			GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>() ;
			GameManager.audioManager.PlaySoundFX(fxName , 1f) ;

		}
		else
		{
			GameManager.audioManager.PlaySoundFX(fxName,1f) ;
		}
		
	}

	public void PlayRailFrictionEffect()
	{
		AudioManager.railFrictionEffect.Play();
	}

}
