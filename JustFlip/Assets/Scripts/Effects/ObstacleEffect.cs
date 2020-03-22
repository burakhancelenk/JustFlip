using UnityEngine;

public class ObstacleEffect : MonoBehaviour
{
	public float speed ;

	private Transform child1 ;
	private Transform child2 ;
	private Transform child3 ;

	private void OnDisable()
	{
		if (child1 == null || child2 == null || child3 == null)
		{
			child1 = gameObject.transform.Find("OrangeTriangle1") ;
			child2 = gameObject.transform.Find("OrangeTriangle2") ;
			child3 = gameObject.transform.Find("OrangeTriangle3") ;
		}
		

		child1.transform.localScale = Vector3.one ;
		child2.transform.localScale = Vector3.one ;
		child3.transform.localScale = Vector3.one ;
		if (child2.gameObject.activeSelf == true)
		{
			child2.gameObject.SetActive(false);
		}

		if(child3.gameObject.activeSelf==true)
		{
			child3.gameObject.SetActive(false);
		}
		child1.GetComponent<SpriteRenderer>().color=new Color(
			child1.GetComponent<SpriteRenderer>().color.r,child1.GetComponent<SpriteRenderer>().color.g,child1.GetComponent<SpriteRenderer>().color.b,
			1f);
		child2.GetComponent<SpriteRenderer>().color=new Color(
			child2.GetComponent<SpriteRenderer>().color.r,child2.GetComponent<SpriteRenderer>().color.g,child2.GetComponent<SpriteRenderer>().color.b,
			1f);
		child3.GetComponent<SpriteRenderer>().color=new Color(
			child3.GetComponent<SpriteRenderer>().color.r,child3.GetComponent<SpriteRenderer>().color.g,child3.GetComponent<SpriteRenderer>().color.b,
			1f);
	}


	void Start()
	{
		if (child1 == null || child2 == null || child3 == null)
		{
			child1 = gameObject.transform.Find("OrangeTriangle1") ;
			child2 = gameObject.transform.Find("OrangeTriangle2") ;
			child3 = gameObject.transform.Find("OrangeTriangle3") ;
		}
		
	}

	
    void Update () 
	{
		
		StartDestroyEffect();
		
	}

	private void StartDestroyEffect()
	{
		if (child1.localScale.sqrMagnitude <= (Vector3.one * 3f).sqrMagnitude)
		{
			child1.transform.localScale=new Vector3(child1.localScale.x+0.01f*speed*Time.deltaTime,child1.localScale.y+0.01f*speed*Time.deltaTime,child1.localScale.z);
			child1.GetComponent<SpriteRenderer>().color=new Color(
				child1.GetComponent<SpriteRenderer>().color.r,child1.GetComponent<SpriteRenderer>().color.g,child1.GetComponent<SpriteRenderer>().color.b,
				child1.GetComponent<SpriteRenderer>().color.a-0.005f*speed*Time.deltaTime);
		}

		if (child1.localScale.sqrMagnitude >= (Vector3.one * 2f).sqrMagnitude &&
		    child2.localScale.sqrMagnitude <= (Vector3.one * 3f).sqrMagnitude)
		{
			if (child2.gameObject.activeSelf == false)
			{
				child2.gameObject.SetActive(true);
			}
			child2.transform.localScale=new Vector3(child2.localScale.x+0.01f*speed*Time.deltaTime,child2.localScale.y+0.01f*speed*Time.deltaTime,child2.localScale.z);
			child2.GetComponent<SpriteRenderer>().color=new Color(
				child2.GetComponent<SpriteRenderer>().color.r,child2.GetComponent<SpriteRenderer>().color.g,child2.GetComponent<SpriteRenderer>().color.b,
				child2.GetComponent<SpriteRenderer>().color.a-0.005f*speed*Time.deltaTime);
		}

		if (child2.localScale.sqrMagnitude >= (Vector3.one * 2f).sqrMagnitude &&
		    child3.localScale.sqrMagnitude <= (Vector3.one * 3f).sqrMagnitude)
		{
			if (child3.gameObject.activeSelf == false)
			{
				child3.gameObject.SetActive(true);
			}
			child3.transform.localScale=new Vector3(child3.localScale.x+0.01f*speed*Time.deltaTime,child3.localScale.y+0.01f*speed*Time.deltaTime,child3.localScale.z);
			child3.GetComponent<SpriteRenderer>().color=new Color(
				child3.GetComponent<SpriteRenderer>().color.r,child3.GetComponent<SpriteRenderer>().color.g,child3.GetComponent<SpriteRenderer>().color.b,
				child3.GetComponent<SpriteRenderer>().color.a-0.005f*speed*Time.deltaTime);
			
		}
		
	    
		
		
		
	}
}
