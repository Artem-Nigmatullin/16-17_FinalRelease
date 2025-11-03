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

        // --- Вывод в консоль Unity ---
        if (context != null)
            UnityEngine.Debug.Log($"<color=#00FFFF>{logMessage}</color>", context);
        else
            UnityEngine.Debug.Log($"<color=#00FFFF>{logMessage}</color>");

        // --- Сохранение в файл ---
        try
        {
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"DevLog: не удалось записать в файл: {e.Message}");
        }
    }

    // === Предупреждение ===
    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Warn(object message, UnityEngine.Object context = null)
    {
        string time =DateTime.Now.ToString("HH:mm:ss");
        if (context != null)
            UnityEngine.Debug.LogWarning($"<color=yellow>[{time}] [WARN]</color> {message}", context);
        else
            UnityEngine.Debug.LogWarning($"<color=yellow>[{time}] [WARN]</color> {message}");
    }

    // === Ошибка ===
    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Error(object message, UnityEngine.Object context = null)
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        if (context != null)
            UnityEngine.Debug.LogError($"<color=red>[{time}] [ERROR]</color> {message}", context);
        else
            UnityEngine.Debug.LogError($"<color=red>[{time}] [ERROR]</color> {message}");
    }
}

