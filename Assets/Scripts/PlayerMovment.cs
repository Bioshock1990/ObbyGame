using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class FirstPersonController : MonoBehaviour
{
    //������
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GroundCheck _ground;

    // ��������� ������
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveInputDeadZone;

    //��������� ������
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce = 1f;

    // ����������� �������
    private int rightFingerId;
    private float halfScreenWidth;

    // ������
    private Vector2 lookInput;

    // ������������ ������
    private Vector3 moveInput;
    private Vector3 moveDir;

    //������ ������ � ����������
    private float velocity;
    private float gravity = -9.81f;

    private void Start()
    {

        rightFingerId = -1;

        // ������������� ���� ���
        halfScreenWidth = Screen.width / 2;

        // ������ �����������
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            Jump();
        }

        //������ ����������
        velocity += gravity * gravityForce * Time.deltaTime;

        if (_ground.isGrounded && velocity < 0)
        {
            velocity = 0;
        }

        GetTouchInput();

        if (rightFingerId != -1)
        {
            // �������� ����������� �������� ������� ������ ���� ����������� ���
            //Debug.Log("Rotating");
            LookAround();
        }

        Move();
        transform.Translate(new Vector3(moveDir.x, velocity, moveDir.z) * Time.deltaTime);
    }


    private void GetTouchInput()
    {
        // ���������� �� ���� ��������
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // ��������� � ����� ��������� ��������� �������
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // �������� ����������� ������ ����� ���� �� ������ �������
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == rightFingerId)
                    {
                        // ���������� ����������� ������ �����
                        rightFingerId = -1;
                        //Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // ��������� ������ ��� ��������
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }

                    break;
                case TouchPhase.Stationary:
                    // ���� ����� ���������� �� ������������� lookInput ������ ���� 
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    private void LookAround()
    {

        // ������� �� ���������
        /*cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);*/

        // ������� �� �����������
        transform.Rotate(transform.up, lookInput.x);
    }

    private void Move()
    {
        moveInput = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        //Debug.Log(moveInput);
        // �������� ���������� ����������� �� ������� ������������
        Vector3 movementDirection = moveInput.normalized * moveSpeed;
        // ����������� ���������
        moveDir = transform.TransformDirection(movementDirection);
        //Debug.Log(movementDirection);
        //characterController.Move(movementDirection);

        //characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.z);
    }

    public void Jump()
    {
        if (_ground.isGrounded)
        {
            velocity = jumpForce;
        }

    }
}
