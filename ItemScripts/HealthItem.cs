using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif

#if UNITY_EDITOR 
[CustomEditor(typeof(HealthItem))]
public class HealthItemEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var healthItem = target as HealthItem;

        healthItem.HasTriggeredAnimation = GUILayout.Toggle(healthItem.HasTriggeredAnimation, "Has trigger animation");

        if (healthItem.HasTriggeredAnimation)
            healthItem.AnimationDuration = EditorGUILayout.FloatField("Trigget animation duration", healthItem.AnimationDuration);

        healthItem.AmountOfRegen = EditorGUILayout.FloatField("Amount of regen", healthItem.AmountOfRegen);
    }
}
#endif

public class HealthItem : MonoBehaviour
{
    public float AmountOfRegen = 2;
    private float playerMaxHealth;
    private Player player;

    public bool HasTriggeredAnimation = false;
    public float AnimationDuration = 0;
    private Animator animator;

    private GameObject SpawnpointObject;

    private void Awake()
    {
        SpawnpointObject = ItemSpawnManager.RandomSpawnpointObject;
        ItemSpawnManager.AvailableSpawnpoints.Remove(SpawnpointObject.name);
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Player")
        {
            player = col.gameObject.GetComponent<Player>();
            playerMaxHealth = player.MaxHealth - AmountOfRegen;
            if(player.CurrentHealth < playerMaxHealth)
            {
                player.CurrentHealth = player.CurrentHealth + AmountOfRegen;
            }
            if(HasTriggeredAnimation == true)
            {
                StartCoroutine(AnimateObject());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator AnimateObject()
    {
        animator.SetBool("IsTouched", HasTriggeredAnimation);
        yield return new WaitForSeconds(AnimationDuration);
        ItemSpawnManager.AvailableSpawnpoints.Add(SpawnpointObject.name);
        Destroy(gameObject);
    }
}
