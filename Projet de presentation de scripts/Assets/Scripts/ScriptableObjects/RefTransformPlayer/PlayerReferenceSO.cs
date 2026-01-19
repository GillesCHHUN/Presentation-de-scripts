using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "TransformSO")]
public class PlayerReferenceSO : ScriptableObject
{
    [HideInInspector]
    public Transform TransformSO;
    [HideInInspector]
    public bool IsPlayerDeadSO;
    [HideInInspector]
    public Collider ColliderSO;

}