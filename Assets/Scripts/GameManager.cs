using MLAPI.NetworkedVar;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : SingletonNetworkedBehaviour<GameManager>
{
    private const float GAME_DURATION = 1000f;
    private const int FEATHERS_TO_COLLECT = 5;
    private const int MAX_HP = 5;

    private readonly NetworkedVarFloat _currentTimer = new NetworkedVarFloat(GAME_DURATION);
    private readonly NetworkedVarInt _feathersCollected = new NetworkedVarInt(0);
    private readonly NetworkedVarInt _currentHP = new NetworkedVarInt(MAX_HP);
    private bool _isGameOver = false;

    private void Start()
    {
        _currentTimer.OnValueChanged += (value, newValue) =>
        {
            UIManagement.Instance.UpdateTimerText(newValue);
            if (newValue > 0) return;
            _isGameOver = true;
            UIManagement.Instance.DisplayCatVictory();
        };
        _feathersCollected.OnValueChanged += (value, newValue) =>
        {
            UIManagement.Instance.UpdatePlumeText(newValue, FEATHERS_TO_COLLECT);
            if (newValue < FEATHERS_TO_COLLECT) return;
            _isGameOver = true;
            UIManagement.Instance.DisplayPigeonsVictory();
        };
        _currentHP.OnValueChanged += (value, newValue) =>
        {
            UIManagement.Instance.UpdateHpText(newValue, MAX_HP);
            if (newValue > 0) return;
            _isGameOver = true;
            UIManagement.Instance.DisplayCatVictory();
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) return;
        if (IsServer)
        {
            _currentTimer.Value -= Time.deltaTime;
        }
    }

    public override void NetworkStart()
    {
        UIManagement.Instance.UpdateHpText(_currentHP.Value, MAX_HP);
        UIManagement.Instance.UpdatePlumeText(_feathersCollected.Value, FEATHERS_TO_COLLECT);
        UIManagement.Instance.UpdateTimerText(_currentTimer.Value);
        
        if (IsServer)
        {
            FindObjectOfType<PlumesSpawner>().SpawnPlumes();
        }
    }

    public void LoseLife()
    {
        if (_isGameOver) return;
        _currentHP.Value = Mathf.Max(_currentHP.Value - 1, 0);
    }

    public void GainFeather()
    {
        if (_isGameOver) return;
        _feathersCollected.Value = Mathf.Min(_feathersCollected.Value + 1, FEATHERS_TO_COLLECT);
    }
}
