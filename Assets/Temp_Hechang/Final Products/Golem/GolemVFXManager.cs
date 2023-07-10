using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemVFXManager : MonoBehaviour
{

    [Header("Projectlie")]
    Projectile.Projectile_Color chargeColor;

    public ParticleSystem blueCharge;
    public ParticleSystem redCharge;
    public ParticleSystem purpleCharge;

    [Header("Slam")]
    public ParticleSystem bamHat;
    public ParticleSystem danHat;

    public void PowerMarmuChargeKor()
    {
        switch(chargeColor)
        {
            case Projectile.Projectile_Color.Red: redCharge.Play(); break;
            case Projectile.Projectile_Color.Blue: blueCharge.Play(); break;
            case Projectile.Projectile_Color.Purple: purpleCharge.Play(); break;
        }
    }

   public void ChargeColor(Projectile.Projectile_Color color)
    {
        chargeColor = color;
    }

    public void DanHatMatiteGutaDe()
    {
        danHat.Play();
    }

    public void BamHatMatiteGutaDe()
    {
        bamHat.Play();
    }
}
