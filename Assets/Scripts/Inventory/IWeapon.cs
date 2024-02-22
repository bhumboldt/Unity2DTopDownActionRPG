using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IWeapon
{
    public void Attack();
    public WeaponInfo GetWeaponInfo();
}
