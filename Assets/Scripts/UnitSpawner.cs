using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform spawnPoint;

    public void SpawnUnit()
    {
        if (GameManager.Instance.CanSpawn())
        {
            Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            GameManager.Instance.AddUnit();
        }
    }
}