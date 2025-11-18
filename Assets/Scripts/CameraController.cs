using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Player Reference
    [Header("Player")]
    public Transform playerBody;

    //text look at camera
    [Header("Camera Look at settings")]
    public Transform orientation;
    private Vector3 offset;
    public Transform mainCamera;


    void Start()
    {
        //text look at camera
        offset = transform.position - playerBody.transform.position;
        mainCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        //text look at camera
        transform.position = playerBody.transform.position + offset;
    }

    void Update()
    {

        //text look at camera
        this.transform.LookAt(mainCamera); //look at the camera
    }
}
