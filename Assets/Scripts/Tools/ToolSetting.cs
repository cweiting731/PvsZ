using UnityEngine;

[CreateAssetMenu(fileName = "New tool", menuName = "addTool")]
public class Tool : ScriptableObject
{
    public string toolName;
    public Sprite sprite;
    public GameObject prefab;
}