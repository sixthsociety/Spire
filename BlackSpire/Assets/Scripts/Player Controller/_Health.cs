using System;
using UnityEngine;

public class _Health : MonoBehaviour {

    [SerializeField]
    public int maxHealth = 100;
    [Range (0, 100)]
    public int currentHealth;
    public Canvas healthBarCanvas;


    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthBarCanvas = GetComponentInChildren<Canvas>();
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void Update()
    {
        ViewHealth();
    }

    void ViewHealth()
    {
        if (currentHealth == 100)
        {
            healthBarCanvas.enabled = false;
        }
        else healthBarCanvas.enabled = true;
        //Include section to reduce the slider on the healthbar as the character takes damage.
    }

}
