using UnityEngine;

public class UIEvents : MonoBehaviour
{

	public Sprite normalImage ;
	public Sprite glowImage ;

	public float changeX ;
	public float changeY ;





	public void StabilizeValuesForPI()
	{
		transform.localPosition += Vector3.right * changeX + Vector3.up * changeY;
		GetComponent<SpriteRenderer>().sprite = glowImage ;
		GetComponent<SpriteRenderer>().color=Color.white;
	}

	public void StabilizeVeluesForPO()
	{
		transform.localPosition -= Vector3.right * changeX + Vector3.up * changeY ;
		GetComponent<SpriteRenderer>().sprite = normalImage ;
		GetComponent<SpriteRenderer>().color=Color.white-Color.black;
	}

	public void StabilizeValuesForFI()
	{
		transform.localPosition += Vector3.right * changeX + Vector3.up * (changeY-25f);
		GetComponent<SpriteRenderer>().sprite = glowImage ;
		GetComponent<SpriteRenderer>().color=Color.white;
	}



	public void StabilizeValuesForFO()
	{
		transform.localPosition -= Vector3.right * changeX + Vector3.up * (changeY-25f) ;
		GetComponent<SpriteRenderer>().sprite = normalImage ;
		GetComponent<SpriteRenderer>().color=Color.white-Color.black;
	}

	public void SwitchInteract()
	{
	    if (GetComponent<CircleCollider2D>().enabled)
		{
			GetComponent<CircleCollider2D>().enabled = false ;
		}
		else
		{
			GetComponent<CircleCollider2D>().enabled = true ;
		}
	}

	public void PlayGameLogoPauseOutAnim()
	{
		GameObject.Find("GameLogo(Clone)").GetComponent<Animator>().SetTrigger("PauseOut") ;
	}

	public void SetFalseYourself()
	{
		gameObject.SetActive(false);
	}
}
