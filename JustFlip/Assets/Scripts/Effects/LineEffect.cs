using UnityEngine;

public class LineEffect : MonoBehaviour {

	public float speed ;
	private Vector3 lastScale ;
	public bool reverseBehaviour ;
	private Vector3 ScaleAmount ;
	private Color AlphaAmount ;

	private void OnEnable()
	{
		if (reverseBehaviour == false)
		{
			transform.localScale = Vector3.one ;
			GetComponent<SpriteRenderer>().color = new Color(
				GetComponent<SpriteRenderer>().color.r , GetComponent<SpriteRenderer>().color.g ,
				GetComponent<SpriteRenderer>().color.b ,
				1f) ;
		}
		else
		{
			transform.localScale = lastScale ;
			GetComponent<SpriteRenderer>().color = new Color(
				GetComponent<SpriteRenderer>().color.r , GetComponent<SpriteRenderer>().color.g ,
				GetComponent<SpriteRenderer>().color.b ,
				0f) ;

		}
	}


	void Start () 
	{
		ScaleAmount= new Vector3(0.023f,0.001076f,0f);
		AlphaAmount=new Color(0f,0f,0f,0.028f);
		
		lastScale=new Vector3(1.89f,1.04f,1f);
		
		if (reverseBehaviour == false)
		{
			transform.localScale=Vector3.one;
		}
		else
		{
			transform.localScale=lastScale;
		}
	
			
		
	}
	
	
	void Update () {
		if (reverseBehaviour == false)
		{
			StartDestroyEffect();
		}
		else
		{
			StartReverseDestroyEffect();
		}
		
	}

	private void StartDestroyEffect()
	{
		if (transform.localScale.sqrMagnitude <= lastScale.sqrMagnitude)
		{
			transform.localScale += ScaleAmount * speed*Time.deltaTime ;
			GetComponent<SpriteRenderer>().color -= AlphaAmount * speed*Time.deltaTime ;
		}
	}
	private void StartReverseDestroyEffect()
	{
		if (transform.localScale.sqrMagnitude >= Vector3.one.sqrMagnitude)
		{
			transform.localScale -= ScaleAmount * speed*Time.deltaTime ;
			GetComponent<SpriteRenderer>().color += AlphaAmount * speed*Time.deltaTime ;
		}

		if (transform.localScale.x < 0.9999f)
		{
			transform.localScale=Vector3.one;
		}
		
	}
}
