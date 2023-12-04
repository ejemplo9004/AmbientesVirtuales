using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float rotationSpeed = 1.5f;

    [SerializeField]
    private Vector2 defaultInitialPositionOnPlane = new Vector2(-4, 4);

    [SerializeField]
    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();

    [SerializeField]
    public NetworkVariable<int> plataforma = new NetworkVariable<int>(
        value: 0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );

    private CharacterController characterController;
    //private Animator animator;
    //public GameObject camara;

    private Vector3 oldInputPosition = Vector3.zero;
    private Vector3 oldInputRotation = Vector3.zero;

    public bool esPropio;

    public InputActionProperty accionMovimiento;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
    }
 //   [ServerRpc(RequireOwnership = false)]
 //   void ConfigurarPlataformaServerRpc()
	//{
 //   }
    private void Start()
    {
        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y), 0,
                   Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y));
            plataforma.Value = (int)GraficsConfig.configuracionDefault.plataformaObjetivo;
            //ConfigurarPlataformaServerRpc();
            //camara.SetActive(true);
        }
        //else
        //{
        //    camara.SetActive(false);
        //}
        accionMovimiento.action.Enable();
        esPropio = IsClient && IsOwner;
    }

    private void Update()
    {

        esPropio = IsClient && IsOwner;
        //esPropio = IsClient && IsOwner;
        if (IsClient && IsOwner)
        {
            ClientInput();


            if (Input.GetKeyDown(KeyCode.A))
            {
                plataforma.Value = 1;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                plataforma.Value = 3;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                plataforma.Value = 5;
            }


        }
        ClientMoveAndRotate();
        ClientVisuals();
    }

    private void ClientInput()
    {
        //player position and rotation input
        Vector3 inputRotation = Vector3.up * accionMovimiento.action.ReadValue<Vector2>().x;

        Vector3 direction = transform.TransformDirection(Vector3.forward);
        float forwardInput = accionMovimiento.action.ReadValue<Vector2>().y;
        Vector3 inputPosition = direction * forwardInput;

        if (oldInputPosition != inputPosition || oldInputRotation != inputRotation)
        {
            oldInputPosition = inputPosition;
            oldInputRotation = inputRotation;
            UpdateClientPositionServerRpc(inputPosition* walkSpeed, inputRotation*rotationSpeed);
        }

        //player state changes
        
        if (forwardInput > 0)
        {
            UpdatePlayerStateServerRpc(PlayerState.Walk);
        }
        else if (forwardInput < 0)
        {
            UpdatePlayerStateServerRpc(PlayerState.ReverseWalk);
        }
        else
        {
            UpdatePlayerStateServerRpc(PlayerState.Idle);
        }
    }

    private void ClientVisuals()
    {
        if (networkPlayerState.Value == PlayerState.Walk)
        {
            //animator.SetFloat("Walk", 1);
        }else if (networkPlayerState.Value == PlayerState.ReverseWalk)
        {
            //animator.SetFloat("Walk", -1);
        }
        else
        {
            //animator.SetFloat("Walk", 0);
        }
    }

    private void ClientMoveAndRotate()
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

    private void UpdateClient()
    {
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(Vector3 newPosition, Vector3 newRotationDirection)
    {
        networkPositionDirection.Value = newPosition;
        networkRotationDirection.Value = newRotationDirection;
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState newState)
    {
        networkPlayerState.Value = newState;
    }

	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();
        plataforma.OnValueChanged += (oldVal, newVal) =>
        {
            print("Valor anterior de plataforma: " + oldVal.ToString() + " - nuevo: " + newVal.ToString());
        };
	}
}
