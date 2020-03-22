using System.Collections;
using UnityEngine;

public class Lines : MonoBehaviour
{

    public Sprite lineImage_Glow ;
    public Sprite lineImage_HighGlow ;
    
    private Color colorAlphaMultiplier = new Color(0f , 0f , 0f , 1f) ;
    private Color fullTransparentColor = new Color(1f , 1f , 1f , 0f) ;


    public void HighlightLine(byte indice)
    {
        if (transform.Find("Line"+(GameBall.destination+1)).GetComponent<SpriteRenderer>().sprite==lineImage_HighGlow)
        {
            TurnOffHighLightedLine(GameBall.destination);
        }
        transform.Find("Line" + (indice+1)).GetComponent<SpriteRenderer>().sprite = lineImage_HighGlow ;
        transform.Find("Line" + (indice+1)).localPosition+=Vector3.right*(-0.18f);

        if (Obstacle.isDestroyerModeActive)
        {
            StartCoroutine(BlinkLineForDestroyMode(indice)) ;
        }
    }

    public IEnumerator BlinkLineForDestroyMode(byte indice)
    {
        SpriteRenderer tempSR = transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>() ;
        bool highlight = false ;
        bool turnOff = true ;
        while (Obstacle.isDestroyerModeActive)
        {
            if (turnOff)
            {
            
                tempSR.color -= Color.black*Time.deltaTime*12;

                if (tempSR.color.a <= 0)
                {
                    tempSR.color = Color.white-Color.black;
                    turnOff = false ;
                    highlight = true ;
                }
                
            }
            else if (highlight)
            {
                
                tempSR.color += Color.black*Time.deltaTime*12;

                if (tempSR.color.a >= 1)
                {
                    turnOff = true ;
                    highlight = false ;
                    tempSR.color = Color.white;
                }
                
            }

            yield return null ;
        }

        tempSR.color = Color.white ;
    }


    public void TurnOffHighLightedLine(byte indice)
    {
        transform.Find("Line" + (indice+1)).GetComponent<SpriteRenderer>().sprite = lineImage_Glow ;
        transform.Find("Line"+(indice+1)).localPosition+=Vector3.right*(0.18f);
        
        if (Obstacle.isDestroyerModeActive)
        {
            transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color = Color.white ;
            StopCoroutine(BlinkLineForDestroyMode(indice));
        }
    }

    public IEnumerator CreateSizzleEffect(byte indice)
    {
        while (transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color.a>0.27f)
        {
            transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color -= colorAlphaMultiplier * (3f)*Time.deltaTime;
            yield return null ;
        }

        transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color =
            fullTransparentColor + colorAlphaMultiplier*(0.27f) ;
        while (transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color.a<0.55f)
        {
            transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color += colorAlphaMultiplier * (3f)*Time.deltaTime;
            yield return null ;
        }
        
        transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color =
            fullTransparentColor + colorAlphaMultiplier*(0.55f) ;
    }

    public void CreateSizzleEffectForRestart()
    {
        StartCoroutine(CreateSizzleEffect(2)) ;
    }

    public IEnumerator SetToNormalFromSizzle(byte indice)
    {
        while (transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color.a<1f)
        {
            transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color += colorAlphaMultiplier * (2f)*Time.deltaTime;
            yield return null ;
        }
        transform.Find("Line" + (indice + 1)).GetComponent<SpriteRenderer>().color = Color.white;
    }




}
