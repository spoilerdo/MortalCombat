using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    private Vector3 DeafaultScale;

    public GameObject HealthBar;
    public GameObject Emblem;

    public Text HealthText;
    public Text ComboText;

	void Start () {
        DeafaultScale = new Vector3(1, 1, 1);
        HealthBar.transform.localScale = DeafaultScale;
    }
    public void HealthUpdate(float _Health, float _MaxHealth, int Combo)
    {
        Vector3 NewScale = new Vector3(_Health / _MaxHealth, 1, 1);
        HealthBar.transform.localScale = NewScale;
        HealthText.text = "Health: " + _Health;
        if(Combo > 0)
        {
            ComboText.text = "Combo X " + Combo;
        }
        else
        {
            ComboText.text = "";
        }
    }
}
