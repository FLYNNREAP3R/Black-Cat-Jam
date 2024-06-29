using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    #region
    //singleton
    public static GameSettings Instance { set; get; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //singleton
    #endregion

    // General Settings
    [Header("General")]
    [SerializeField] public int volume = 50;
    [SerializeField] public int NumberOfAssemblyLines = 2;


}
