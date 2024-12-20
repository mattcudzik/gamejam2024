using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int Size;
    public ItemEnum Type;
    public Sprite Sprite;
    public bool Contraband = false;
    public string toStr()
    {
        return Type.ToString() + " " + Size;
    }
}
