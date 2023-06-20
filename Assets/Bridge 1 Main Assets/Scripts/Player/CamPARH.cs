using UnityEngine;

public class CamPARH : MonoBehaviour
{
    [Header("Height Handler")]
    [SerializeField] float extendsDown;

    [Header("Camera and Movment")]
    [SerializeField] float sensitivity = 5;
    [SerializeField] float walkSpeed = 10;
    [SerializeField] float sprintSpeed = 15;

    bool b_lock = true;

    void Update()
    {
        fun_CursorConfinement();

        if(b_lock)
        {
            fun_heightHandle();
            fun_RotateCamera();
            fun_MoveCamera();
        }
    }

    float accel;
    bool r_Down = false;
    RaycastHit h_Down;

    void fun_heightHandle()
    {
        r_Down = Physics.Raycast(transform.position, Vector3.down, out h_Down, extendsDown);

        if (r_Down)
        {
            transform.position = h_Down.point + Vector3.up * extendsDown;
            accel = 0;
        }
        else
        {
            accel += Time.deltaTime * 0.25f;
            transform.position += Vector3.down * accel;
        }
    }

    void fun_CursorConfinement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            b_lock = !b_lock;

        if (b_lock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!b_lock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0) && b_lock == false)
            b_lock = true;
    }

    float rot;
    void fun_RotateCamera()
    {
        rot += Input.GetAxisRaw("Mouse X") * sensitivity;

        transform.localRotation = Quaternion.Euler(0.0f, rot, 0.0f);
    }

    void fun_MoveCamera()
    {
        float velocitae;

        if (Input.GetKey(KeyCode.LeftShift))
            velocitae = sprintSpeed;
        else
            velocitae = walkSpeed;

        GetComponent<Rigidbody>().AddRelativeForce(vec3_moveDir().normalized * velocitae, ForceMode.Force);
    }

    Vector3 vec3_moveDir()
    {
        int oW = 0;
        int oS = 0;
        int oA = 0;
        int oD = 0;

        if (Input.GetKey(KeyCode.W)) oW = 1;
        if (Input.GetKey(KeyCode.S)) oS = 1;
        if (Input.GetKey(KeyCode.A)) oA = 1;
        if (Input.GetKey(KeyCode.D)) oD = 1;

        return new Vector3(oD - oA, 0.0f, oW - oS);
    }
}
