using System.Collections;
using UnityEngine;

public class EffectEvents : MonoBehaviour
{

    public float delayTime;
    public bool destroy_TInactive_F;
    
    private void Start()
    {
        StartCoroutine(DestroyOrInactiveYourSelf());
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyOrInactiveYourSelf());
    }

    
    
    public IEnumerator DestroyOrInactiveYourSelf()
    {
        yield return new WaitForSeconds(delayTime);
        if (destroy_TInactive_F)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    
}
