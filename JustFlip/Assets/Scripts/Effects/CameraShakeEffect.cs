using System.Collections;
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
	


	
	public IEnumerator ShakeCamera(float magnitude,float duration)
	{
		float elapsedTime = 0f ;
		Vector3 originalPosition = transform.localPosition ;
		float randomX , randomY ;

		while (elapsedTime<duration)
		{
			randomX = Random.Range(-1f , 1f) ;
			randomY = Random.Range(-1f , 1f) ;
			
			transform.localPosition=new Vector3(randomX*magnitude,randomY*magnitude,transform.localPosition.z);

			elapsedTime += Time.deltaTime ;
			yield return null ; // this means it will wait until next frame

		}
		transform.localPosition = originalPosition ;
	}

	

    
		
	
}
