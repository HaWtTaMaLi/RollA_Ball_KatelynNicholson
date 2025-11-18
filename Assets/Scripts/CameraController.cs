using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player")]
    //look with mouse
    public Transform playerBody;

    [Header("Camera Look at settings")]
    //text look at camera
    public Transform orientation;
    private Vector3 offset;
    public Transform mainCamera;

    //look with mosue
    public float xRotation = 0f;
    public float yRotation = 0f;

    public float mouseSensitivity = 100f;

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
        /*//NO ERRORS BUT DOESNT MOVE 
        //look with mouse
        float mouseX = Input.GetAxis("mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 30);
        orientation.Rotate(Vector3.up * mouseX);


        //text look at camera
        this.transform.LookAt(mainCamera); //look at the camera */
    }
}
