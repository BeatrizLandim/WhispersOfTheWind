using UnityEngine;

public class AttackHitboxes : MonoBehaviour
{
    [Header("Hitboxes")]
    public BoxCollider2D hitbox1;
    public BoxCollider2D hitbox2;
    public BoxCollider2D hitbox3;

    private void Start()
    {
        hitbox1.enabled = false;
        hitbox2.enabled = false;
        hitbox3.enabled = false;
    }

    // Ataque 1
    public void AtivarHitbox1()
    {
        hitbox1.enabled = true;
    }

    public void DesativarHitbox1()
    {
        hitbox1.enabled = false;
    }

    // Ataque 2
    public void AtivarHitbox2()
    {
        hitbox2.enabled = true;
    }

    public void DesativarHitbox2()
    {
        hitbox2.enabled = false;
    }

    // Ataque 3
    public void AtivarHitbox3()
    {
        hitbox3.enabled = true;
    }

    public void DesativarHitbox3()
    {
        hitbox3.enabled = false;
    }
}