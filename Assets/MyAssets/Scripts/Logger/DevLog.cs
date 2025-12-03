using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public static class DevLog
{
    private readonly static string logPath = Application.persistentDataPath + "/game_log.txt";
    // === Лог общего назначения ===
    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]

    public static void Log(object message, UnityEngine.Object context = null)
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        string logMessage = $"[{time}] [INFO] {message}";
        string color = $"<color=#00FFFF>[{logMessage}] </color>";

        if (context != null)
            UnityEngine.Debug.Log($"{color},{context}");
        else
            UnityEngine.Debug.Log(color);

        try
        {
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"DevLog: не удалось записать в файл: {e.Message}");
        }
    }

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Warn(object message, UnityEngine.Object context = null)
    {
        string time =DateTime.Now.ToString("HH:mm:ss");
        string logMessage = $"[{time}] [WARN] {message}";
        string color = $"<color=yellow>[{logMessage}] [WARN]</color>";

        if (context != null)
            UnityEngine.Debug.LogWarning($"{color} {context}");
        else
            UnityEngine.Debug.LogWarning(color);

        try
        {
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"DevWARN: не удалось записать в файл: {e.Message}");
        }
    }

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Error(object message, UnityEngine.Object context = null)
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        string logMessage = $"[{time}] [ERROR] {message}";
        string color = $"<color=#FF0000>[{logMessage}] [ERROR]</color>";

        if (context != null)
            UnityEngine.Debug.LogError($"{color} {context}");
        else
            UnityEngine.Debug.LogError(color);

        try
        {
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"DevError: не удалось записать в файл: {e.Message}");
        }
    }
}

