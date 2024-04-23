using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public int damage = 20; // Daño que inflige el jugador
    public float attackRange = 1.5f; // Rango de ataque del jugador
    public LayerMask enemyLayer; // Capa de los enemigos

    private Animator anim; // Referencia al Animator
    private bool isAttacking = false; // Flag para controlar si el jugador está atacando
    public float attackCooldown = 1f; // Tiempo de espera antes de otro ataque

    private void Start()
    {
        anim = GetComponent<Animator>(); // Obtener referencia al Animator
    }

    // Método para detectar y atacar a los enemigos
    public void Attack()
    {
        // Si ya está atacando o está en cooldown, salir del método
        if (isAttacking)
            return;

        // Activar la animación de Ataque en el Animator
        anim.SetBool("Ataque", true);

        // Marcar que el jugador está atacando
        isAttacking = true;

        // Iniciar el cooldown del ataque
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        // Esperar el tiempo de cooldown
        yield return new WaitForSeconds(attackCooldown);

        // Reiniciar la bandera de ataque y desactivar la animación
        isAttacking = false;
        anim.SetBool("Ataque", false);
    }

    // Dibujar gizmos para visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
