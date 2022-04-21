using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public enum ServerToClientId : ushort
{
    playerSpawned = 1,
}

public enum CliendToServerId : ushort
{
    name = 1,
}


public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
    public static NetworkManager Singelton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)}Instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    public Client Client { get; private set; }
    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    
    private void Awake()
    {
        Singelton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FaildToConnect;
        Client.Disconnected += DidConnect;

    }
    
    private void FixedUpdate()
    {
        Client.Tick();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
        
    }

    private void DidConnect(object sender, EventArgs e)
    {
        UIManager.Singelton.SendName();
    }

    private void FaildToConnect(object sender, EventArgs e)
    {
        UIManager.Singelton.BackToMain();
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        UIManager.Singelton.BackToMain();
    }

}
