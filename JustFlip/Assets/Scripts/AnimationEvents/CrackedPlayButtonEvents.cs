using UnityEngine;

public class CrackedPlayButtonEvents : MonoBehaviour
{

    public GameObject playButton ;
    public GameObject crackedPlayButtonBlastSparkEffect ;
    
    public void CreatePlayButton()
    {
        Instantiate(playButton , Vector3.zero , Quaternion.identity) ;  
    }

    public void CreateBlastSparkEffect()
    {
        Instantiate(crackedPlayButtonBlastSparkEffect , Vector3.zero , Quaternion.identity) ;        
    }

    public void DestroyYourSelf()
    {
        Destroy(gameObject);
    }

    public void CallShakeCamera()
    {
        StartCoroutine(GameObject.Find("Camera").GetComponent<CameraShakeEffect>().ShakeCamera(5f,0.2f)) ;   
    }
    
    public void PlayFx()
    {
       GetComponent<AudioSource>().Play();
    }
   
    
}
