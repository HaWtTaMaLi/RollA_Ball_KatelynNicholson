using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Player")]
    public Transform playerBody;
    private Vector3 offset;

    void Start()
    {
        //text look at camera
        offset = transform.position - playerBody.transform.position;
    }

    void LateUpdate()
    {

        //text look at camera
        transform.position = playerBody.position + offset;

        transform.LookAt(playerBody);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        offset = Quaternion.AngleAxis(mouseX * 3f, Vector3.up) * offset.normalized * offset.magnitude;
    }
}
