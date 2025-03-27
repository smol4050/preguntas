using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using models;
using System.IO;
using System;
using TMPro;

public class GameControllerPreguntas : MonoBehaviour
{
    private void Start()
    {
        Debug.LogWarning("Este script será eliminado. Reemplazar por:");
        Debug.LogWarning("- RoundManager\n- DataManager\n- UIManager");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}