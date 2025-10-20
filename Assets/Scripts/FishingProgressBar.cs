using UnityEngine;

public class FishingProgressBar : MonoBehaviour
{   
       private Quaternion initialRotation;
    
    void Start()
    {
        initialRotation = transform.rotation;
    }
    
    void LateUpdate()
    {
        transform.rotation = initialRotation;
    }
}
