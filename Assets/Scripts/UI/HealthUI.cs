using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    private Slider _slider;
    private void Start()
    {
        _slider = GetComponent<Slider>();
    }
    public void SetValue(float health)
    {
        _slider.value = health;
    }
}
