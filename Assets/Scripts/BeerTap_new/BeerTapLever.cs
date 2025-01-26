using System;
using TMPro;
using UnityEngine;

public class BeerTapLever : MonoBehaviour
{
    public bool isActivated { get; private set; }
    
    [SerializeField] private float _actiavtionAngleMin = -0.69f;
    [SerializeField] private float _actiavtionAngleMax = -0.65f;
    
    // for testing purpose
    [SerializeField] private TextMeshProUGUI _debugText;

    private void Update()
    {
        if (transform.localRotation.x >= _actiavtionAngleMin && transform.localRotation.x <= _actiavtionAngleMax)
        {
            isActivated = true;
            _debugText.text = "Activated";
        }
        else
        {
            isActivated = false;
            _debugText.text = "";
        }
    }
}
