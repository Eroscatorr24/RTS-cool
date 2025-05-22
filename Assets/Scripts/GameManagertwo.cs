using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxUnits = 5;
    private int currentUnits = 0;

    public TextMeshProUGUI unitCounterText;

    void Awake()
    {
        if (Instance == null) Instance = this;

        currentUnits = 1; 
        UpdateUI();
    }

    public bool CanSpawn()
    {
        return currentUnits < maxUnits;
    }

    public void AddUnit()
    {
        currentUnits++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        unitCounterText.text = $"Unidades: {currentUnits}/{maxUnits}";
    }
}


