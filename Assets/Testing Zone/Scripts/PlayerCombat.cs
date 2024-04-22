using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int damage = 20; // Da�o que inflige el jugador
    public float attackRange = 1.5f; // Rango de ataque del jugador
    public LayerMask enemyLayer; // Capa de los enemigos

    // M�todo para detectar y atacar a los enemigos
    public void Attack()
    {
        // Obtener la direcci�n en la que mira el personaje
        Vector3 attackDirection = transform.forward;

        // Detectar los enemigos en el rango de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // Aplicar da�o a los enemigos detectados
        foreach (Collider enemy in hitEnemies)
        {
            // Calcular la direcci�n al enemigo desde el jugador
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // Calcular el �ngulo entre la direcci�n de ataque y la direcci�n al enemigo
            float angle = Vector3.Angle(attackDirection, directionToEnemy);

            // Si el �ngulo es menor que 90 grados, el enemigo est� en frente del jugador y puede ser atacado
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
