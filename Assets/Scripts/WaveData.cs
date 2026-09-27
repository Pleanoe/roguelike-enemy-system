using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WaveData", menuName = "Waves/Wave Data")]
public class WaveData : ScriptableObject
{
    public List<EnemyGroup> enemies;
}
