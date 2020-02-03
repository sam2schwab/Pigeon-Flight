using MLAPI;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T Instance { get; private set; }
    
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
        DontDestroyOnLoad(transform.gameObject);
    }
}
public class SingletonNetworkedBehaviour<T> : NetworkedBehaviour where T : SingletonNetworkedBehaviour<T>
{
    public static T Instance { get; private set; }
    
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
        DontDestroyOnLoad(transform.gameObject);
    }
}
