using DilmerGames.Core.Singletons;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public NetworkTransport unityTransport;

    [SerializeField]
    private string inpAddressIP;

    [SerializeField]
    private int inpPort;

    [SerializeField]
    private Button startServerButton;

    [SerializeField]
    private Button StartHostButton;

    [SerializeField]
    private Button startClientButton;

    [SerializeField]
    private TextMeshProUGUI playersInGameText;

    [SerializeField]
    private TMP_InputField joinCodeInput;

    [SerializeField]
    private Button executePhysicsButton;

    private bool hasServerStarted;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        playersInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInGame}";
    }

    private void Start()
    {
        //inicia el servidor
        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Logger.Instance.LogInfo("Server started...");
            }
            else
            {
                Logger.Instance.LogInfo("Server could not be started...");
            }

        });

        //Inicia el Host
        StartHostButton.onClick.AddListener(async() =>
        {
            //if (RelayManager.Instance.IsRelayEnabled)
            //    await RelayManager.Instance.SetupRelay();

            if (NetworkManager.Singleton.StartHost())
            {
                Logger.Instance.LogInfo("Host started...");
            }
            else
            {
                Logger.Instance.LogInfo("Host could not be started...");
            }
        });
        //Inicia el cliente
        startClientButton.onClick.AddListener(async() =>
        {
            //if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCodeInput.text))
            //    await RelayManager.Instance.JoinRelay(joinCodeInput.text);

            if (NetworkManager.Singleton.StartClient())
            {
                Logger.Instance.LogInfo("Client started...");
            }
            else
            {
                Logger.Instance.LogInfo("Client could not be started...");
            }
        });

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            Logger.Instance.LogInfo($"{id} just connected...");
        };

        NetworkManager.Singleton.OnServerStarted += () =>
        {
            hasServerStarted = true;
        };

        executePhysicsButton.onClick.AddListener(() =>
        {
            if (!hasServerStarted)
            {
                Logger.Instance.LogWarning("Server has not started...");
                return;
            }
            SpawnerControl.Instance.SpawnObjects(); //originalmente el metodo es privado en spawnerControl pero tuve que ponerlo public para llamarlo aca
        });
    }

    public void ChangeAddress(string ip)
    {
        inpAddressIP = ip;
        NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(inpAddressIP, (ushort)inpPort);
    }

    public void ChangePort(string port)
    {
        inpPort = int.Parse(port);
        NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(inpAddressIP, (ushort)inpPort);
    }
}
