using System.Collections;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{


	private GameObject[] lightnings ;
	private byte orderOfLightningsIndice ;
	private byte[] orderOfLightnings ;
	
	

	
	void Awake()
	{
		orderOfLightningsIndice = 0 ;
		lightnings=new GameObject[3];
		orderOfLightnings=new byte[20];
		for (byte i = 0; i < orderOfLightnings.Length; i++)
		{
			orderOfLightnings[i] = (byte)(i % 3) ;
		}
		byte[] mixerArray = new byte[10];
		byte temp ;
		
		for (byte i = 0; i < mixerArray.Length; i++)
		{
			mixerArray[i] = (byte)Random.Range(0 , 19) ;
		}

		for (byte i = 0; i < mixerArray.Length-1; i++)
		{
			temp = orderOfLightnings[mixerArray[i]] ;
			orderOfLightnings[mixerArray[i]] = orderOfLightnings[mixerArray[i + 1]] ;
			orderOfLightnings[mixerArray[i + 1]] = temp ;
		}
		
		lightnings[0] = transform.Find("Lightning1").gameObject ;
		lightnings[1] = transform.Find("Lightning2").gameObject ;
		lightnings[2] = transform.Find("Lightning3").gameObject ;
	}

	private void OnEnable()
	{
		StartCoroutine(LightningEffectCoroutine()) ;
	}

	private void OnDisable()
	{
		for (byte i = 0; i < 3; i++)
		{
			lightnings[i].SetActive(false);
		}
		StopCoroutine(LightningEffectCoroutine()) ;
	}

	private IEnumerator LightningEffectCoroutine()
	{
	    byte temp ;
		
		while (true)
		{
			temp = GetRandomNumber();
			lightnings[temp].SetActive(true);
			yield return new WaitForSeconds(0.01f*Random.Range(4,10)); ;
			lightnings[temp].SetActive(false);
			yield return new WaitForSeconds(0.01f*Random.Range(1,15)); ;
			temp = GetRandomNumber() ;
			lightnings[temp].SetActive(true);
			yield return new WaitForSeconds(0.01f*Random.Range(4,10)); ;
			lightnings[temp].SetActive(false);
			yield return new WaitForSeconds(0.01f*Random.Range(1,15)); ;
			temp = GetRandomNumber() ;
			lightnings[temp].SetActive(true);
			yield return new WaitForSeconds(0.01f*Random.Range(4,10)); ;
			lightnings[temp].SetActive(false);
			yield return new WaitForSeconds(0.01f*Random.Range(1,15)); ;
		}
		
	}

	private byte GetRandomNumber()
	{
		
		if (orderOfLightningsIndice==19)
		{
			orderOfLightningsIndice = 0 ;
			return orderOfLightnings[19] ;
		}
		
		orderOfLightningsIndice++ ;
		return orderOfLightnings[orderOfLightningsIndice - 1] ;
		
		
	}
}
