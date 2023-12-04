using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    public enum PlayerState
    {
        Idle,Walk,ReverseWalk
    }
    [SerializeField]
    private float walkSpeed = 3.5f;

    [SerializeField]
    private float runSpeedOffset = 2.0f;

    [SerializeField]
    private float rotationSpeed = 3.5f;

    [SerializeField]
    private Vector2 defaultInitialPositionOnPlane = new Vector2(-4, 4);

    [SerializeField]
    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();

    private CharacterController characterController;
    private Animator animator;

    //client caches positions
    private Vector3 oldForwardBackPosition;
    private Vector3 oldLeftRightPosition; // client caches positions
    private Vector3 oldInputPosition = Vector3.zero;
    private Vector3 oldInputRotation = Vector3.zero;
    private PlayerState oldPlayerState = PlayerState.Idle;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y), 0,
                   Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y));
        }
        
    }

    private void Update()
    {
        if (IsClient && IsOwner)
        {
            ClientInput();
        }
        clientMoveAndRotate();
        clientVisuals();
    }

    private void ClientInput()
    {
        //player position and rotation input
        Vector3 inputRotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 inputPosition = direction * forwardInput;

        if (oldInputPosition != inputPosition || oldInputRotation != inputRotation)
        {
            oldInputPosition = inputPosition;
            oldInputRotation = inputRotation;
            updateClientPositionServerRpc(inputPosition* walkSpeed, inputRotation*rotationSpeed);
        }

        //player state changes
        if (forwardInput > 0)
        {
            updatePlayerStateServerRpc(PlayerState.Walk);
        }
        else if (forwardInput < 0)
        {
            updatePlayerStateServerRpc(PlayerState.ReverseWalk);
        }
        else
        {
            updatePlayerStateServerRpc(PlayerState.Idle);
        }
    }

    private void clientVisuals()
    {
        if (networkPlayerState.Value == PlayerState.Walk)
        {
            animator.SetFloat("walk", 1);
        }else if (networkPlayerState.Value == PlayerState.ReverseWalk)
        {
            animator.SetFloat("walk", -1);
        }
        else
        {
            animator.SetFloat("walk", 0);
        }
    }

    private void clientMoveAndRotate()
    {
        if (networkPositionDirection.Value != Vector3.zero)
        {
            characterController.SimpleMove(networkPositionDirection.Value);
        }
        if (networkRotationDirection.Value != Vector3.zero)
        {
            transform.Rotate(networkRotationDirection.Value);
        }
    }
    /*
    private void updateServer()
    {
        transform.position = new Vector3(transform.position.x + leftRightPosition.Value, 
            transform.position.y, transform.position.z + forwardBackPosition.Value);
    }*/

    private void updateClient()
    {
       
    }
    [ServerRpc]
    public void updateClientPositionServerRpc(Vector3 newPosition, Vector3 newRotationDirection)
    {
        networkPositionDirection.Value = newRotationDirection;
        networkRotationDirection.Value = newRotationDirection;
    }

    [ServerRpc]
    public void updatePlayerStateServerRpc(PlayerState newState)
    {
        networkPlayerState.Value = newState;
    }
}
