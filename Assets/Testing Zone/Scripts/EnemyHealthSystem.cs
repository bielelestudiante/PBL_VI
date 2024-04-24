using UnityEngine;
using System;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Color originalColor;
    public Color damageColor;
    public float damageFlashTime = 0.2f;

    private Material myMaterial;
    private Animator animator;

    // Evento para notificar cuando un enemigo es destruido
    public event Action OnEnemyDestroyed;

    void Start()
    {
        currentHealth = maxHealth;
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;

        animator = GetComponent<Animator>();

        // Obtener referencia al EnemySpawner usando FindObjectOfType
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
        {
            OnEnemyDestroyed += spawner.EnemyDestroyed;
        }
        else
        {
            Debug.LogError("EnemySpawner not found!");
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Infligiendo da�o al enemigo.");
        if (currentHealth <= 0)
        {
            Debug.Log("matando.");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");

        // Llamar al evento OnEnemyDestroyed
        OnEnemyDestroyed?.Invoke();

        // Activar la animaci�n de muerte en el Animator
        animator.SetBool("Die",true);
        animator.SetBool("IsChasing", false);
        animator.SetBool("IsPlayerClose", false);
        animator.SetBool("IsPatrolling", false);
        animator.SetBool("IsAttacking", false);

        // Destruir recursivamente el GameObject y todos sus hijos
        DestroyRecursive(gameObject);

        // C�digo original para instanciar Lootbag
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

    private void DestroyRecursive(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            DestroyRecursive(child.gameObject);
        }
        Destroy(obj);
    }
}
