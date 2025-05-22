using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
   public static UnitSelectionManager Instance{ get; private set; }
   public List<GameObject> AllUnitsList = new List<GameObject>();
   public List<GameObject> UnitsSeleted = new List<GameObject>();

   public LayerMask Clickable;
   public LayerMask Ground;
   public LayerMask Attackable;
   public GameObject GroundMarker;
   private Camera cam;
   public bool AttackCursorVisible;
   private void Awake()
   {
      // --- INICIALIZACIÓN DEL SINGLETON ---
      if (Instance == null)
      {
         Instance = this;
         Debug.Log("UnitSelectionManager.Instance asignado en Awake().");
         // Opcional: si quieres que el manager persista entre escenas
         // DontDestroyOnLoad(gameObject);
      }
      else if (Instance != this)
      {
         // Si ya existe otra instancia, destruye esta para mantener el Singleton.
         Debug.LogWarning("Ya existe una instancia de UnitSelectionManager. Destruyendo esta duplicada: " + gameObject.name);
         Destroy(gameObject);
         return; // Importante salir aquí para no ejecutar el resto de Awake/Start en el duplicado
      }

      // También es buena práctica inicializar la cámara aquí si otros scripts
      // podrían depender de ella indirectamente a través del manager en sus Start().
      cam = Camera.main;
      if (cam == null)
      {
         Debug.LogError("No se encontró una cámara principal (MainCamera) en la escena. Asegúrate de que exista y esté etiquetada como 'MainCamera'.", this);
      }

      if (GroundMarker == null)
      {
         Debug.LogWarning("GroundMarker no está asignado en el Inspector para UnitSelectionManager.", this);
      }
   }
   private void Start()
   {
      // Si cam ya se inicializó en Awake, esta línea no es estrictamente necesaria aquí,
      // pero no hace daño tenerla si Awake fallara por alguna razón no prevista (aunque no debería).
      if (cam == null)
      {
         cam = Camera.main;
         if (cam == null)
         {
            // Ya se logueó en Awake, pero por si acaso.
            Debug.LogError("Cámara principal aún no encontrada en Start().", this);
         }
      }

      if (GroundMarker != null && !GroundMarker.activeSelf) // Solo desactivar si no está ya desactivado
      {
         GroundMarker.SetActive(false);
      }
   }
   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         
         RaycastHit hit;
         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray, out hit, Mathf.Infinity, Clickable))
         {
            if (Input.GetKey(KeyCode.LeftShift))
            {
               MultiSelect(hit.collider.gameObject);
            }
            else
            {
               SelectByClicking(hit.collider.gameObject);
            }
            
         }
         else
         {
            DeselectAll();
         }
      }
      if (Input.GetMouseButtonDown(1)&& UnitsSeleted.Count > 0)
      {
         
         RaycastHit hit;
         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
         {
            GroundMarker.transform.position = hit.point;
            
            GroundMarker.SetActive(false);
            GroundMarker.SetActive(true);
         }
        
      }
      //atacar objetivo
      if (UnitsSeleted.Count > 0 && AtletsOneOffensiveUnit(UnitsSeleted))
      {
         
         RaycastHit hit;
         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray, out hit, Mathf.Infinity, Attackable))
         {
            Debug.Log("enemigo en el mouse");
            AttackCursorVisible = true;

            if (Input.GetMouseButtonDown(1))
            {
               Transform enemyTarget = hit.transform; 
               foreach (GameObject unit in UnitsSeleted)
               {
                  if (unit.GetComponent<AttackController>())
                  {
                     unit.GetComponent<AttackController>().targetToAttack = enemyTarget;
                  }
               }
            }
         }
         else
         {
            AttackCursorVisible = false;
         }
        
      }
      
      
   }

   private bool AtletsOneOffensiveUnit(List<GameObject> unitsSeleted)
   {
      foreach (GameObject unit in UnitsSeleted)
      {
         if (unit.GetComponent<AttackController>())
         {
            return true;
         }

       
      }  
      return false;
   }

   private void MultiSelect(GameObject unit)
   {
      if (UnitsSeleted.Contains(unit)==false)
      {
         UnitsSeleted.Add(unit);
         SelectUnit(unit, true);
       

      }
      else
      {
         SelectUnit(unit, false);
         UnitsSeleted.Remove(unit);
         
      }
         
   }

   public void DeselectAll()
   {
      foreach (var unit in UnitsSeleted)
      {
         SelectUnit(unit, false);

      }
      GroundMarker.SetActive(false); 
      UnitsSeleted.Clear();
   }
   
   internal void DragSelect(GameObject unit)
   {
      if (UnitsSeleted.Contains(unit)==false)
      {
         UnitsSeleted.Add(unit);
         SelectUnit(unit, true);
      }
   }

   // ReSharper disable Unity.PerformanceAnalysis
   private void SelectByClicking(GameObject unit)
   {
      DeselectAll();
      UnitsSeleted.Add(unit);
      SelectUnit(unit, true);
   }

   private void SelectUnit(GameObject unit, bool isSelected)
   {
      triggerSelectionIndicator(unit, isSelected);
      EnableUnitMovement(unit, isSelected);
   }

   private void EnableUnitMovement(GameObject unit, bool shouldMove)
   {
      if (unit == null) return;
      UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
      if (unitMovement != null)
      {
         unitMovement.enabled = shouldMove;
      }
      else
      {
         Debug.LogWarning("El objeto " + unit.name + " no tiene el componente UnitMovement.", unit);
      }
   }
   private void triggerSelectionIndicator(GameObject unit, bool isVisible)
   {
      if (unit == null) return;
      if (unit.transform.childCount > 0)
      {
         Transform selectionIndicator = unit.transform.GetChild(0);
         if (selectionIndicator != null)
         {
            selectionIndicator.gameObject.SetActive(isVisible);
         }
      }
      else
      {
         Debug.LogWarning("La unidad " + unit.name + " no tiene hijos para usar como indicador de selección.", unit);
      }
   }
  

  
}
