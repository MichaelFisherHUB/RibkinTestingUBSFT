using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoReloadSliderManager : MonoBehaviour
{
    [SerializeField]
    private List<Slider> sliders = new List<Slider>();
    public void InitUI(List<AmmoBase> allUsersWeapon)
    {
        sliders.AddRange(GetComponentsInChildren<Slider>());
    }
}
