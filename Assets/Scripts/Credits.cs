using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject _creditsMenu;
    private bool _isCreditsMenuActive = false;
    private bool _fadeOutFix;
    private void Update()
    {
        CheckIfCreditsMenuBoolIsTrue();
        if (_creditsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MenuDisable"))
        {
            _isCreditsMenuActive = false;
        }
    }
    public void ToggleCreditsMenu()
    {
        if (!_isCreditsMenuActive)
        {
            _fadeOutFix = false;
            _isCreditsMenuActive = true;
        }
        else
        {
            if (!_fadeOutFix)
            {
                _creditsMenu.GetComponent<Animator>().Play("MenuFadeOut");
                _fadeOutFix = true;
            }
        }
    }
    private void CheckIfCreditsMenuBoolIsTrue()
    {
        if (!_isCreditsMenuActive)
        {
            _creditsMenu.SetActive(false);
        }
        else
        {
            _creditsMenu.SetActive(true);
        }
    }
    public void CreditsEvent()
    {
        ToggleCreditsMenu();
    }
}
