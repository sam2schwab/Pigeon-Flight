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
    private string HashGenerator => Hashes[_dropdown.value];
    private static readonly string[] Hashes = new []{ "Hunter","Hunted" };
    
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
    }

    public void HostGame()
    {
        NetworkingManager.Singleton.ConnectionApprovalCallback += SingletonOnConnectionApprovalCallback;
        NetworkingManager.Singleton.StartHost(_spawnPos, _spawnRot, true, SpawnManager.GetPrefabHashFromGenerator(HashGenerator));
        StartGame();
    }

    private void SingletonOnConnectionApprovalCallback(byte[] connectionData, ulong clientId, NetworkingManager.ConnectionApprovedDelegate callback)
    {
        callback(true, SpawnManager.GetPrefabHashFromGenerator(HashGenerator), true, _spawnPos, _spawnRot);
    }

    public void JoinGame()
    {
        _transport.ConnectAddress = _ipField.text;
        NetworkingManager.Singleton.StartClient();
        StartGame();
    }

    private void StartGame()
    {
        _canvas.enabled = false;
        Time.timeScale = 1;
    }
}
