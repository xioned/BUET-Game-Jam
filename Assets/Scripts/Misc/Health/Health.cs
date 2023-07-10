using UnityEngine;

[RequireComponent(typeof(HealthUpdateEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    public int defaultHealthAmount;
    private int currentHealthAmount;
    HealthUpdateEvent healthUpdateEvent;
    private void Awake()
    {
        healthUpdateEvent = GetComponent<HealthUpdateEvent>();
    }

    private void Start()
    {
        currentHealthAmount = defaultHealthAmount;
    }
    public void SetAnimeHealth(int amount)
    {
        defaultHealthAmount = amount;
        currentHealthAmount = amount;
    }

    public void DecreaseHealth(int amount)
    {
        int newHealthAfterDamage = currentHealthAmount - amount;
        currentHealthAmount = newHealthAfterDamage;
        if (currentHealthAmount <= 0)
        {
            currentHealthAmount = 0;
        }
        healthUpdateEvent.CallHealthUpdateEvent(currentHealthAmount,defaultHealthAmount);
    }

    public void IncreaseHealth(int amount)
    {
        int newHealthAfterDamage = currentHealthAmount + amount;
        currentHealthAmount = newHealthAfterDamage;
        
        if (currentHealthAmount > defaultHealthAmount)
        {
            currentHealthAmount = defaultHealthAmount;
        }
        healthUpdateEvent.CallHealthUpdateEvent(currentHealthAmount,defaultHealthAmount);
    }
}
