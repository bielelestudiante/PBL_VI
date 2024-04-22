using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;

    private Material myMaterial;
    private float flashTimer;

    // Referencia al EnemySpawner
    private EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;

        // Obtener referencia al EnemySpawner
        spawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            myMaterial.color = damageColor;
        }
        else
        {
            myMaterial.color = originalColor;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        flashTimer = damageFlashTime;
    }

    private void Die()
    {
        Destroy(gameObject);

        // Notificar al EnemySpawner que un enemigo ha sido destruido
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }

        // Código original para instanciar Lootbag
        Lootbag lootbagComponent;
        if (TryGetComponent(out lootbagComponent))
        {
            lootbagComponent.InstantiateLoot(transform.position);
        }
        else
        {
            Debug.LogError("No Lootbag component found on this GameObject or its parents.");
        }
    }

    // Método opcional para ser llamado manualmente si el EnemySpawner necesita saber
    public void NotifyEnemyDestroyed()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }
    }
}
