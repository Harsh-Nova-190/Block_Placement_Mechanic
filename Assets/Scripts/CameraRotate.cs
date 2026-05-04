using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;

    private float rotX = 0f;
    private float rotY = 0f;

    void Update()
    {
        float v = Input.GetAxis("Horizontal"); 
        float h = Input.GetAxis("Vertical");   

        Vector3 move = new Vector3(h, 0, v);

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * 100f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * 100f * Time.deltaTime;

            rotX += mouseX;
            rotY -= mouseY;
            rotY = Mathf.Clamp(rotY, -80f, 80f);

            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
        }
    }
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotX = angles.y;
        rotY = angles.x;
    }
}