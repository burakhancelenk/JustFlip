using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	public Sprite triangleOrange ;
	public Sprite triangleOrange_Glow ;
	private float distanceToGameBall ;
	private GameObject tempObstacleEffect ;
	
	[HideInInspector] public float obstacleSpeed ;
	[HideInInspector] public static GameObject gameBall ;
	[HideInInspector] public static GameObject lines ;
	[HideInInspector] public byte gameBallDestination ;
	[HideInInspector] public static bool isDestroyerModeActive ;
	[HideInInspector] public static bool destroyGameBall ;

	private static float distanceTolerance = 12f ;
	private static Vector3 positionOutOfCamera = Vector3.up*160 ;
	
	

	private void OnEnable()
	{
		transform.localScale += Vector3.right+Vector3.up;
		StartCoroutine(CheckCollision()) ;
		StartCoroutine(LeftArea()) ;
		StartCoroutine(DestroyerModeController()) ;
		StartCoroutine(MoveObstacle()) ;

		
	}

	private void OnDisable()
	{
		StopCoroutine(MoveObstacle());
		StopCoroutine(CheckCollision());
		StopCoroutine(LeftArea());
		StopCoroutine(DestroyerModeController());
		obstacleSpeed = 0 ;
		transform.Find("ObstacleTriangleOrange").GetComponent<SpriteRenderer>().sprite = triangleOrange ;
		transform.localScale = Vector3.one ;
		transform.Find("ObstacleBehindEffect").GetComponent<ParticleSystem>().emissionRate = 50 ;
	}



	private void Start ()
	{
		
		if (lines == null)
		{
	     	lines = GameObject.Find("Lines(Clone)");
		}

		

	}


	private IEnumerator CheckCollision()
	{
		
		  while (true)
		  {
			  if (gameBall != null)
			  {

				  if (!GameBall.isSwitchingPosition)
				  {

					  distanceToGameBall = (gameBall.transform.localPosition - transform.localPosition).magnitude ;

					  if (distanceToGameBall <= distanceTolerance)
					  {
						  if (!PlayerPrefs.HasKey("Tutorial"))
						  {
							  if (gameBallDestination == GameBall.currentPositionIndice)
							  {
								  Tutorial.sameLineHighlighted = true ;
							  }
							  else
							  {
								  Tutorial.differentLineHighlighted = true ;
							  }
						  }
						  
						  GameManager.audioManager.PlaySoundFX("ObstacleDestroyEffect",0.5f);

						  GameManager.CreateObstacleEffect(transform.localPosition) ;
						  if (gameBallDestination != GameBall.currentPositionIndice)
						  {
							  if (GameManager.GetCounterForDestroyerMode(this.GetType()) < 2)
							  {
								  GameManager.SetCounterForDestroyerMode(this.GetType(),(byte)(GameManager.GetCounterForDestroyerMode(this.GetType())+1));
								  GameManager.CreateLineEffect(gameBallDestination , true) ;
								  lines.GetComponent<Lines>().HighlightLine(gameBallDestination) ;
								  GameManager.SetCountDownIsActive(this.GetType(),true);
								  GameBall.destination = gameBallDestination ;
							  }
							  else if (GameManager.GetCounterForDestroyerMode(this.GetType()) == 2)
							  {
								  GameManager.SetCounterForDestroyerMode(this.GetType(),(byte)(GameManager.GetCounterForDestroyerMode(this.GetType())+1));
								  isDestroyerModeActive = true ;
								  GameManager.audioManager.PlaySoundFX("DestroyModeStartEffect",1f);
								  GameManager.CreateLineEffect(gameBallDestination , true) ;
								  lines.GetComponent<Lines>().HighlightLine(gameBallDestination) ;
								  GameManager.SetCountDownIsActive(this.GetType(),true);
								  GameBall.destination = gameBallDestination ;
							  }
							  else
							  {
								  destroyGameBall = true ;
								  GameManager.SetCountDownIsActive(this.GetType(),false);
							  }
						  }
						  else
						  {
							  if (GameBall.destination != GameBall.currentPositionIndice)
							  {
								  lines.GetComponent<Lines>().TurnOffHighLightedLine(GameBall.destination) ;
								  GameManager.SetCountDownIsActive(this.GetType(),false);
							  }

							  if (GameManager.GetCounterForDestroyerMode(this.GetType()) < 2)
							  {
								  GameManager.SetCounterForDestroyerMode(this.GetType(),(byte)(GameManager.GetCounterForDestroyerMode(this.GetType())+1));
							  }
							  else if (GameManager.GetCounterForDestroyerMode(this.GetType()) == 2)
							  {

							  }
							  else
							  {
								  destroyGameBall = true ;
							  }

							  GameBall.destination = gameBallDestination ;
						  }

						  transform.localPosition = positionOutOfCamera ;
						  GameManager.SetRemainTime(this.GetType(),GameManager.GetMaxRemainTime(this.GetType()));
						  gameObject.SetActive(false) ;
					  }
					  
				  }
				  
			  }

			  yield return null ;
		  }
	}

	
	public IEnumerator MoveObstacle()
	{
		while (true)
		{
			
			transform.localPosition += Vector3.down * obstacleSpeed*Time.smoothDeltaTime ;
			yield return null ;
			
		}
		
	}


	private IEnumerator LeftArea()
	{
		while (transform.localPosition.y>(-100))
		{
			yield return null;
		}
		while (transform.localScale.sqrMagnitude>0f && transform.localScale.x>0)
		{
			transform.localScale-=Vector3.right*(7f)*Time.deltaTime+Vector3.up*(7f)*Time.deltaTime;
			if (GetComponent<ParticleSystem>().emissionRate > 0)
			{
				GetComponent<ParticleSystem>().emissionRate -= 50*Time.deltaTime ;
			}

			yield return null ;

		}
		
		gameObject.SetActive(false);
	}

	public IEnumerator DestroyerModeController()
	{
		SpriteRenderer temp = transform.Find("ObstacleTriangleOrange").GetComponent<SpriteRenderer>() ;

		
			while (true)
			{
				if (isDestroyerModeActive)
				{
					if (temp.color == Color.red)
					{
						temp.color=Color.black;
					}
					else
					{
						temp.color=Color.red;
					}
					
					if (!PlayerPrefs.HasKey("Tutorial") && !PlayerPrefs.HasKey("destroyModeTutorial"))
					{
						Tutorial.destroyerModeTutorial = true ;
						PlayerPrefs.SetInt("destroyModeTutorial",0);
					}
					
					yield return new WaitForSeconds(0.1f);
					
				}
				else
				{
					if (temp.color != Color.white)
					{
						temp.color = Color.white ;
					}
					yield return null ;
				}
				
				
			}

	}
	
}
