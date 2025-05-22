using UnityEngine;

public class StructureClickHandler : MonoBehaviour
{
    public GameObject spawnButton;
    public Material defaultMaterial;
    public Material selectedMaterial;

    private Renderer rend;
    private bool isSelected = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = defaultMaterial;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clic izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Select();
                }
                else
                {
                    Deselect();
                }
            }
            else
            {
                Deselect();
            }
        }
    }

    void Select()
    {
        isSelected = true;
        rend.material = selectedMaterial;
        spawnButton.SetActive(true);
    }

    void Deselect()
    {
        if (isSelected)
        {
            rend.material = defaultMaterial;
            spawnButton.SetActive(false);
            isSelected = false;
        }
    }
}
