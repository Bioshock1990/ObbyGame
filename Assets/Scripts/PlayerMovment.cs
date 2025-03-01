using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class FirstPersonController : MonoBehaviour
{
    //ссылки
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GroundCheck _ground;

    // Настройки игрока
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveInputDeadZone;

    //Настройки прыжка
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce = 1f;

    // Обнаружение косаний
    private int rightFingerId;
    private float halfScreenWidth;

    // Камера
    private Vector2 lookInput;

    // Передвижение игрока
    private Vector3 moveInput;
    private Vector3 moveDir;

    //прыжок игрока и гравитация
    private float velocity;
    private float gravity = -9.81f;

    private void Start()
    {

        rightFingerId = -1;

        // расчитывается один раз
        halfScreenWidth = Screen.width / 2;

        // расчет погрешности
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            Jump();
        }

        //расчет гравитации
        velocity += gravity * gravityForce * Time.deltaTime;

        if (_ground.isGrounded && velocity < 0)
        {
            velocity = 0;
        }

        GetTouchInput();

        if (rightFingerId != -1)
        {
            // Начинаем отслеживать движение правого пальца если задетектили его
            //Debug.Log("Rotating");
            LookAround();
        }

        Move();
        transform.Translate(new Vector3(moveDir.x, velocity, moveDir.z) * Time.deltaTime);
    }


    private void GetTouchInput()
    {
        // Проходимся по всем касаниям
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Проверяем в каком состоянии находится косание
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Начинаем отслеживать правый палец если он только нажался
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == rightFingerId)
                    {
                        // прекращаем отслеживать правый палец
                        rightFingerId = -1;
                        //Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Получение данных для поворота
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Если палец неподвижен то устанавливаем lookInput равным нулю 
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

        // Поворот по вертикали
        /*cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);*/

        // Поворот по горизонтали
        transform.Rotate(transform.up, lookInput.x);
    }

    private void Move()
    {
        moveInput = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        //Debug.Log(moveInput);
        // Умножаем полученное направление на скорсть передвижения
        Vector3 movementDirection = moveInput.normalized * moveSpeed;
        // Передвигаем персонажа
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
