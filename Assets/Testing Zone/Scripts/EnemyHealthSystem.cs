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

    void Start()
    {
        currentHealth = maxHealth;
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
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
        Lootbag lootbagComponent = GetComponent<Lootbag>(); // Obtener referencia al componente Lootbag
        if (lootbagComponent != null)
        {
            lootbagComponent.InstantiateLoot(transform.position); // Llamar al método InstantiateLoot con la posición actual del enemigo
        }
        else
        {
            Debug.LogError("No Lootbag component found on this GameObject or its parents.");
        }        
    }
}