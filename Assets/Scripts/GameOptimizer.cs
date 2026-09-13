using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Free Fire Game Optimizer - Tối ưu hóa mạnh cho FPS Mobile Game
/// Features: Response Optimization, Sensitivity Control, Game Boost, Smoothness
/// </summary>
public class GameOptimizer : MonoBehaviour
{
    [System.Serializable]
    public class OptimizationSettings
    {
        public int targetFPS = 90;
        public bool enableVSync = false;
        public QualityLevel qualityLevel = QualityLevel.High;
        public float sensitivityX = 1.5f;
        public float sensitivityY = 1.5f;
        public bool enableGameBoost = true;
        public int boostLevel = 90;
    }

    public enum QualityLevel
    {
        Low,
        Medium,
        High,
        Ultra
    }

    private static GameOptimizer instance;
    public static GameOptimizer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameOptimizer>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameOptimizer");
                    instance = obj.AddComponent<GameOptimizer>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private OptimizationSettings settings = new OptimizationSettings();
    private ResponseOptimizer responseOptimizer;
    private SensitivityControl sensitivityControl;
    private GameBooster gameBooster;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeOptimizers();
    }

    private void InitializeOptimizers()
    {
        responseOptimizer = gameObject.AddComponent<ResponseOptimizer>();
        sensitivityControl = gameObject.AddComponent<SensitivityControl>();
        gameBooster = gameObject.AddComponent<GameBooster>();

        ApplyOptimizations();
    }

    public void ApplyOptimizations()
    {
        // Áp dụng cài đặt FPS
        Application.targetFrameRate = settings.targetFPS;
        QualitySettings.vSyncCount = settings.enableVSync ? 1 : 0;

        // Áp dụng chất lượng đồ họa
        ApplyQualitySettings();

        // Tối ưu phản hồi
        if (responseOptimizer != null)
        {
            responseOptimizer.OptimizeResponse();
        }

        // Áp dụng độ nhạy
        if (sensitivityControl != null)
        {
            sensitivityControl.SetSensitivity(settings.sensitivityX, settings.sensitivityY);
        }

        // Bật Game Boost
        if (settings.enableGameBoost && gameBooster != null)
        {
            gameBooster.ActivateBoost(settings.boostLevel);
        }

        Debug.Log($"[GameOptimizer] Optimizations Applied - FPS: {settings.targetFPS}, Quality: {settings.qualityLevel}, Boost Level: {settings.boostLevel}");
    }

    private void ApplyQualitySettings()
    {
        switch (settings.qualityLevel)
        {
            case QualityLevel.Low:
                QualitySettings.SetQualityLevel(0);
                break;
            case QualityLevel.Medium:
                QualitySettings.SetQualityLevel(2);
                break;
            case QualityLevel.High:
                QualitySettings.SetQualityLevel(4);
                break;
            case QualityLevel.Ultra:
                QualitySettings.SetQualityLevel(5);
                break;
        }
    }

    public void SetTargetFPS(int fps)
    {
        settings.targetFPS = Mathf.Clamp(fps, 30, 120);
        Application.targetFrameRate = settings.targetFPS;
    }

    public void SetSensitivity(float x, float y)
    {
        settings.sensitivityX = Mathf.Clamp(x, 0.1f, 5f);
        settings.sensitivityY = Mathf.Clamp(y, 0.1f, 5f);
        if (sensitivityControl != null)
        {
            sensitivityControl.SetSensitivity(settings.sensitivityX, settings.sensitivityY);
        }
    }

    public void SetQualityLevel(QualityLevel level)
    {
        settings.qualityLevel = level;
        ApplyQualitySettings();
    }

    public void EnableGameBoost(int level)
    {
        settings.enableGameBoost = true;
        settings.boostLevel = Mathf.Clamp(level, 1, 90);
        if (gameBooster != null)
        {
            gameBooster.ActivateBoost(settings.boostLevel);
        }
    }

    public void DisableGameBoost()
    {
        settings.enableGameBoost = false;
        if (gameBooster != null)
        {
            gameBooster.DeactivateBoost();
        }
    }

    public OptimizationSettings GetCurrentSettings()
    {
        return settings;
    }

    public float GetCurrentFPS()
    {
        return 1f / Time.deltaTime;
    }
}
