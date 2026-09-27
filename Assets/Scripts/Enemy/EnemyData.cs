using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyData",menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float fireRate;
    public int maxHealth;
    public float moveSpeed;
    public int damage;
    public float attackDistance;
}
