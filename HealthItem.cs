using UnityEngine;
using System.Collections;

public class HealthItem : MonoBehaviour
{
    public int AmountOfRegen = 2;
    private Player player;

    public bool HasTriggeredAnimation = false;
    private Animation TriggerAnimation;
    public string TriggerAnimationName;
    private Animator animator;

    private float WaitBeforeDestroy;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Player")
        {
            player = col.gameObject.GetComponent<Player>();
            player.CurrentHealth = player.CurrentHealth + AmountOfRegen;
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
        WaitBeforeDestroy = TriggerAnimation[TriggerAnimationName].time;
        yield return new WaitForSeconds(WaitBeforeDestroy);
        Destroy(gameObject);
    }
}
