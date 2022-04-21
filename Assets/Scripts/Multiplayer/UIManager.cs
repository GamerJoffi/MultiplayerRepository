using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;
    public static UIManager Singelton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)}Instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    [Header("Connect")]
    [SerializeField] private GameObject connectUI;
    [SerializeField] InputField usernameField;

    private void Awake()
    {
        Singelton = this;
    }

    public void ConnectClicked()
    {
        usernameField.interactable = false;
        connectUI.SetActive(false);

        NetworkManager.Singelton.Connect();
    }

    public void BackToMain()
    {
        usernameField.interactable = true;
        connectUI.SetActive(false);
    }

    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)CliendToServerId.name);
    }
}
