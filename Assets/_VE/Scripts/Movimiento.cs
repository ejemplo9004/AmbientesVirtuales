using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento
    public float jumpForce = 10f; // Fuerza de salto
    public Transform groundCheck; // Objeto para verificar si el jugador está en el suelo
    public LayerMask groundLayer; // Capa del suelo

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isGrounded;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Verificar si el jugador está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection = move.normalized * moveSpeed;

        // Salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = Mathf.Sqrt(2 * jumpForce * Mathf.Abs(Physics.gravity.y));
        }

        // Aplicar gravedad
        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        // Mover el jugador
        characterController.Move(moveDirection * Time.deltaTime);
    }
}