using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class LoginJustEffect : MonoBehaviour
{

	public Sprite justImage ;
	public Sprite justImage_Glow ;

	private Image img ;
	
	
	void Start ()
	{
		img = GetComponent<Image>() ;
		StartCoroutine(JustEffect()) ;
	}


	private IEnumerator JustEffect()
	{
		while (true)
		{
			transform.localScale = Vector3.one;
			img.sprite = justImage ;
			yield return new WaitForSeconds(Random.Range(0f,1.5f));
			transform.localScale = Vector3.right*1.18f+Vector3.up*1.9f+Vector3.forward;
			img.sprite = justImage_Glow ;
			yield return new WaitForSeconds(Random.Range(0f,0.3f)) ;
		}
	}
	
	
}
