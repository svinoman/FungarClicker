using UnityEngine;
using UnityEngine.UI;

public class HPBars : MonoBehaviour
{
    private Slider _hpBar;
    private GribusAI _gribusAI;
    private void Awake()
    {
        _hpBar = GetComponent<Slider>();
        _gribusAI = transform.parent.parent.GetComponent<GribusAI>();
        _hpBar.maxValue = _gribusAI.hp;
    }
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back);
        _hpBar.value = _gribusAI.hp;
    }
}
