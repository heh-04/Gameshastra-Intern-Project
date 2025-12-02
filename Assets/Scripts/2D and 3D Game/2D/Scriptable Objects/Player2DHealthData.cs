using UnityEngine;

[CreateAssetMenu(fileName = "Player2DHealthData", menuName = "Scriptable Objects/Player2DHealthData")]
public class Player2DHealthData : ScriptableObject
{
    [SerializeField]private int startingHealth = 5;
    public int StartingHealth => startingHealth;
}
