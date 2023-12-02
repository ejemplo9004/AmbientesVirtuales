using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Parametros : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetArg();
    }
    // Helper function for getting the command line arguments
    private static void GetArg()
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-iniciar")
            {
                NetworkManager.Singleton.StartServer();
            }
            if (true)
            {
                Logger.Instance.LogInfo("parametros: "+ args[i]);
            }
        }
        Logger.Instance.LogInfo("parametros final");
    }
}
