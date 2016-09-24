using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    [SerializeField]
    private float MaxHealth = 100;

    [SyncVar]
    public float CurrentHealth;

    private Healthbar HealthbarLeft;
    private GameObject HealthbarLeftObject;

    private Healthbar HealthbarRight;
    private GameObject HealthbarRightObject;

    private AttackBoxManager attackBoxManager;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        attackBoxManager = GetComponent<AttackBoxManager>();
    }
    void Start()
    {
        HealthbarLeftObject = GameObject.FindGameObjectWithTag("HealthbarLeft");
        HealthbarLeft = HealthbarLeftObject.GetComponent<Healthbar>();

        HealthbarRightObject = GameObject.FindGameObjectWithTag("HealthbarRight");
        HealthbarRight = HealthbarRightObject.GetComponent<Healthbar>();
    }
    public void TakeDamage(float _Damage)
    {
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            CurrentHealth -= _Damage;
            Debug.Log("Player: " + CurrentHealth);
        }
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            HealthbarLeft.HealthUpdate(CurrentHealth, MaxHealth, attackBoxManager.Combo);
        }
        else
        {
            HealthbarRight.HealthUpdate(CurrentHealth, MaxHealth, attackBoxManager.Combo);          
        }
    }
}
