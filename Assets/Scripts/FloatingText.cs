using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Transform mainCamera;




    void Start()
    {
        mainCamera = Camera.main.transform;
  
        
    }

    void Update()
    {
        this.transform.LookAt(mainCamera); //look at the camera
    
    }
}
