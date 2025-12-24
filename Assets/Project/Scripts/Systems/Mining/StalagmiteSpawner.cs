using UnityEngine;

public class StalagmiteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject stalagmitePrefab;
    [SerializeField] private BoxCollider2D worldBounds;
    [SerializeField] private int minSpawn = 3;
    [SerializeField] private int maxSpawn = 8;

    private void OnEnable()
    {
        GameEvents.RunReset += Spawn;
    }

    private void OnDisable()
    {
        GameEvents.RunReset -= Spawn;
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        // Eski dikitleri temizle
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int count = Random.Range(minSpawn, maxSpawn + 1);

        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = GetRandomPointInBounds();
            Instantiate(stalagmitePrefab, randomPos, Quaternion.identity, transform);
        }
    }

    private Vector2 GetRandomPointInBounds()
    {
        Bounds b = worldBounds.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        return new Vector2(x, y);
    }
}
