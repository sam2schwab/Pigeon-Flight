using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private InputField _ipField;
    private UnetTransport _transport;
    private Dropdown _dropdown;
    private Canvas _canvas;
    private Vector3? _spawnPos = null;
    private Quaternion? _spawnRot = null;
    private static readonly string[] Hashes = new []{ "Hunter","Hunted" };
    public bool AutoHost = false;
    
    // Start is called before the first frame update
    void Start()
    {
        NetworkingManager.Singleton.NetworkConfig.ConnectionApproval = true;
        _ipField = GetComponentInChildren<InputField>();
        _transport = NetworkingManager.Singleton.GetComponent<UnetTransport>();
        _dropdown = GetComponentInChildren<Dropdown>();
        _canvas = GetComponent<Canvas>();
        var spawn = GameObject.FindWithTag("Spawn")?.transform;
        if (spawn != null)
        {
            _spawnPos = spawn.position;
            _spawnRot = spawn.rotation;
        }

        _ipField.text = "127.0.0.1";
        
        Time.timeScale = 0;

        if (AutoHost)
        {
            _dropdown.value = 1;
            HostGame();
        }
    }

    public void HostGame()
    {
        NetworkingManager.Singleton.ConnectionApprovalCallback += SingletonOnConnectionApprovalCallback;
        NetworkingManager.Singleton.StartHost(_spawnPos, _spawnRot, true, SpawnManager.GetPrefabHashFromGenerator(Hashes[_dropdown.value]));
        StartGame();
    }

    private void SingletonOnConnectionApprovalCallback(byte[] connectionData, ulong clientId, NetworkingManager.ConnectionApprovedDelegate callback)
    {
        callback(true, SpawnManager.GetPrefabHashFromGenerator(Hashes[BitConverter.ToInt32(connectionData, 0)]), true, _spawnPos, _spawnRot);
    }

    public void JoinGame()
    {
        _transport.ConnectAddress = _ipField.text;
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = BitConverter.GetBytes(_dropdown.value);
        NetworkingManager.Singleton.StartClient();
        StartGame();
    }

    private void StartGame()
    {
        _canvas.enabled = false;
        Time.timeScale = 1;
    }
}
