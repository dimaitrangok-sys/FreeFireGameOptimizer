using UnityEngine;

/// <summary>
/// Game Booster - Tối ưu độ mượt game
/// Boost Level up to 90 - Giảm lag, tăng FPS, cảm giác chơi mượt
/// </summary>
public class GameBooster : MonoBehaviour
{
    private int currentBoostLevel = 0;
    private bool isBoostActive = false;
    private float boostStartTime;

    public void ActivateBoost(int level)
    {
        currentBoostLevel = Mathf.Clamp(level, 1, 90);
        isBoostActive = true;
        boostStartTime = Time.realtimeSinceStartup;

        ApplyBoostSettings();
    }

    private void ApplyBoostSettings()
    {
        // Boost Level 90 - Maximum optimization
        if (currentBoostLevel >= 80)
        {
            // Giảm shadow quality
            QualitySettings.shadows = ShadowQuality.Disable;
            
            // Giảm particle effect
            QualitySettings.particleRaycastBudget = 16;

            // Disable post-processing
            QualitySettings.antiAliasing = 0;

            // Optimize memory
            Resources.UnloadUnusedAssets();

            Debug.Log($"[GameBooster] 🚀 Level {currentBoostLevel} - Maximum Boost Applied!");
        }
        else if (currentBoostLevel >= 50)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            QualitySettings.particleRaycastBudget = 32;
            Debug.Log($"[GameBooster] 🔥 Level {currentBoostLevel} - High Boost Applied!");
        }
        else
        {
            QualitySettings.shadows = ShadowQuality.All;
            Debug.Log($"[GameBooster] ⚡ Level {currentBoostLevel} - Standard Boost Applied!");
        }

        // Set high FPS
        Application.targetFrameRate = 90;
        QualitySettings.vSyncCount = 0;

        // Optimize rendering
        QualitySettings.masterTextureLimit = 0;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
    }

    public void DeactivateBoost()
    {
        isBoostActive = false;
        currentBoostLevel = 0;
        Debug.Log("[GameBooster] Boost deactivated!");
    }

    public int GetCurrentBoostLevel()
    {
        return currentBoostLevel;
    }

    public bool IsBoostActive()
    {
        return isBoostActive;
    }

    public float GetBoostDuration()
    {
        if (isBoostActive)
        {
            return Time.realtimeSinceStartup - boostStartTime;
        }
        return 0f;
    }
}
