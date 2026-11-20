using System;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance {get; private set;}
    public bool killedAllSlimes;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
