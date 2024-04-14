using UnityEngine;


public enum proType
{
    rock,firball,arrow
}
public class Projectile : MonoBehaviour
{
    [SerializeField] private proType projectileType;
    [SerializeField] private int attackDamage;
    [SerializeField] private float projectileSpeed;


    public int AttackDamage { get { return attackDamage; } }

    public proType ProjectileType { get { return projectileType; } }

    public float ProjectileSpeed { get {  return projectileSpeed; } }
}
