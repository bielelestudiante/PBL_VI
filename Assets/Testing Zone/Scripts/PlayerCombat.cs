using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int damage = 20; // Daño que inflige el jugador
    public float attackRange = 1.5f; // Rango de ataque del jugador
    public LayerMask enemyLayer; // Capa de los enemigos

    private Animator anim; // Referencia al Animator

    private void Start()
    {
        anim = GetComponent<Animator>(); // Obtener referencia al Animator
    }

    // Método para detectar y atacar a los enemigos
    public void Attack()
    {
        // Activar la animación de Ataque en el Animator
        anim.SetBool("Ataque", true);

        // Obtener la dirección en la que mira el personaje
        Vector3 attackDirection = transform.forward;

        anim.SetBool("Ataque", false);

        // Detectar los enemigos en el rango de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // Aplicar daño a los enemigos detectados
        foreach (Collider enemy in hitEnemies)
        {
            // Calcular la dirección al enemigo desde el jugador
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // Calcular el ángulo entre la dirección de ataque y la dirección al enemigo
            float angle = Vector3.Angle(attackDirection, directionToEnemy);

            // Si el ángulo es menor que 90 grados, el enemigo está en frente del jugador y puede ser atacado
            if (angle < 90f)
            {
                EnemyHealthSystem enemyHealth = enemy.GetComponent<EnemyHealthSystem>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }

    // Dibujar gizmos para visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
