using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private InputField _ipField;
    private UnetTransport _transport;
    private Canvas _canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        _ipField = GetComponentInChildren<InputField>();
        _transport = NetworkingManager.Singleton.GetComponent<UnetTransport>();
        _canvas = GetComponent<Canvas>();

        _ipField.text = "127.0.0.1";
    }

    public void HostGame()
    {
        NetworkingManager.Singleton.StartHost();
        _canvas.enabled = false;
    }

    public void JoinGame()
    {
        _transport.ConnectAddress = _ipField.text;
        NetworkingManager.Singleton.StartClient();
        _canvas.enabled = false;
    }
}
