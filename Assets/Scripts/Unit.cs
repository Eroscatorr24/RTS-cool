// Unit.cs
using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
        
        if (UnitSelectionManager.Instance != null)
        {
            UnitSelectionManager.Instance.AllUnitsList.Add(gameObject);
        }
        else
        {
            Debug.LogError("UnitSelectionManager.Instance no está disponible en Start() para la unidad: " + gameObject.name);
        }
    }

    private void OnDestroy()
    {
       
        if (UnitSelectionManager.Instance != null)
        {
            
            if (UnitSelectionManager.Instance.AllUnitsList != null)
            {
                UnitSelectionManager.Instance.AllUnitsList.Remove(gameObject);
            }
            else
            {
                
                Debug.LogWarning("UnitSelectionManager.Instance.AllUnitsList es null en OnDestroy() para la unidad: " + gameObject.name);
            }
        }
        
    }
}