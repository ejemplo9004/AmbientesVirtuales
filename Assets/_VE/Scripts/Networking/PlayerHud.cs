using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerHud : NetworkBehaviour
{
    [SerializeField]
    private NetworkVariable<NetworkString> playerNetworkName = new NetworkVariable<NetworkString>();

    private bool overlaySet = false;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            ulong idAjustado = OwnerClientId + 1;
            playerNetworkName.Value = $"Player {idAjustado}";
        }
    }

    public void SetOverlay()
    {   
        var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if(localPlayerOverlay != null) localPlayerOverlay.text = $"{playerNetworkName.Value}";
    }

    public void Update()
    {
        if (!overlaySet && !string.IsNullOrEmpty(playerNetworkName.Value))
        {
            SetOverlay();
            overlaySet = true;
        }
    }
}
