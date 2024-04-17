using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;

    private Material myMaterial;
    private float flashTimer;

    void Start()
    {
        currentHealth = maxHealth;
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            myMaterial.color = damageColor;
        }
        else
        {
            myMaterial.color = originalColor;
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
        Lootbag lootbagComponent = GetComponent<Lootbag>(); // Obtener referencia al componente Lootbag
        if (lootbagComponent != null)
        {
            lootbagComponent.InstantiateLoot(transform.position); // Llamar al método InstantiateLoot con la posición actual del enemigo
        }
        else
        {
            Debug.LogError("No Lootbag component found on this GameObject or its parents.");
        }
        Destroy(gameObject);
    }
}