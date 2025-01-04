using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "addItem")]
public class Item : ScriptableObject
{
    public string toolName;
    public Sprite sprite;
    public GameObject prefab;
}