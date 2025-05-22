using UnityEngine;
using UnityEngine.InputSystem;
public class ControlCamara : MonoBehaviour
{
    public float velocidadmMovimiento = 10;
    public float velocidadRotacion = 10;
   
    private InputAction movimiento; 
    private InputAction rotacion;

    private Transform yaw;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        movimiento = InputSystem.actions.FindAction("Movimiento");
        rotacion = InputSystem.actions.FindAction("Rotacion");
       
        yaw = transform.Find("Yaw");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.anyKey)
        {
            
            Vector2 vectorMovimiento = movimiento.ReadValue<Vector2>();
            float cambioRotacion = rotacion.ReadValue<float>();
            
            yaw.Rotate(0, cambioRotacion, 0);
           
            Vector3 movimientoRotado = yaw.rotation * new Vector3(vectorMovimiento.x, 0, vectorMovimiento.y);
            
            
            transform.Translate(velocidadmMovimiento * Time.deltaTime * movimientoRotado); 
           
           
           
        }
    }
}
