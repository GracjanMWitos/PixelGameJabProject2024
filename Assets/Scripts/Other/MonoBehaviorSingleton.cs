using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviorSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T currentInstance;
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return FindObjectOfType<T>();
#endif
            if (currentInstance == null)
                Debug.LogError($"Trying to get instance of {typeof(T)}, but it's not set");
            return currentInstance;
        }
    }

    protected virtual void Awake()
    {
        if (currentInstance != null)
        {
            Debug.LogError(
                $"Current instance of {typeof(T)} is not empty in OnAwake, it's {currentInstance.gameObject.name}");
            Destroy(gameObject);
            return;
        }

        currentInstance = this as T;
    }

    protected virtual void OnDestroy()
    {
        if (currentInstance == null)
        {
            Debug.LogError($"Current instance of {typeof(T)} is empty in OnDestroy");
            return;
        }

        currentInstance = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitSubsystem()
    {
        currentInstance = null;
    }
}