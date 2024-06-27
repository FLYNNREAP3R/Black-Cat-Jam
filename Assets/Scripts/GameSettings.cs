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

    // Assemblyline KeyCodes
    [Header("KeyCodes")]
    [SerializeField] public KeyCode PackageKeyCode_1 = KeyCode.Q;
    [SerializeField] public KeyCode YeetKeyCode_1 = KeyCode.W;

    [SerializeField] public KeyCode PackageKeyCode_2 = KeyCode.E;
    [SerializeField] public KeyCode YeetKeyCode_2 = KeyCode.R;

    [SerializeField] public KeyCode PackageKeyCode_3 = KeyCode.U;
    [SerializeField] public KeyCode YeetKeyCode_3 = KeyCode.I;

    [SerializeField] public KeyCode PackageKeyCode_4 = KeyCode.O;
    [SerializeField] public KeyCode YeetKeyCode_4 = KeyCode.P;

    [SerializeField] public int NumberOfAssemblyLines = 2;
}
