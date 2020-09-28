using UnityEngine;
[CreateAssetMenu]
public class TrashScriptableObject : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public float Value;
    public float MoveSpeed;
    public Rarity Rarity;
    public decimal Count = 0;
    public bool IsUnlocked = false;
    public GameObject CollectionObject;
    public string Description = "(description missing)";
}
