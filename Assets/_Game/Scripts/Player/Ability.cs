using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public float cooldownTime;

    public abstract void Activate(GameObject player);
}
