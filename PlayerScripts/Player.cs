using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField]
    public float MaxHealth = 100;

    [SyncVar]
    public float CurrentHealth;

    private Healthbar HealthbarLeft;
    private GameObject HealthbarLeftObject;

    private Healthbar HealthbarRight;
    private GameObject HealthbarRightObject;

    private AttackBoxManager attackBoxManager;
    private MatchController matchController;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        attackBoxManager = GetComponent<AttackBoxManager>();
        matchController = FindObjectOfType<MatchController>();
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
        if(CurrentHealth >= 0)
        {
            CurrentHealth -= _Damage;
            PlayerController.IsDamaged = true;
            Debug.Log("Player: " + CurrentHealth);
        }
    }
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            //Destroy(gameObject);
            StartCoroutine(matchController.WinningController(attackBoxManager.EnemyName));
            PlayerController.IsDead = true;
        }
        if (isLocalPlayer)
        {
            HealthbarLeft.HealthUpdate(CurrentHealth, MaxHealth, attackBoxManager.Combo);
            HealthbarLeft.PlayerName(gameObject.name);
        }
        else
        {
            HealthbarRight.HealthUpdate(CurrentHealth, MaxHealth, attackBoxManager.Combo);          
        }
    }
}
