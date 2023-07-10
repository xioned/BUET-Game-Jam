using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ParticleSystem largeParticle;
    public ParticleSystem smallParticle;

    public GameObject littleArrow;
    public GameObject largeArrow;

    float damage;

    bool shot = false;
    public void ArrowShot(float charge, float finalDamage, bool charged)
    {
        damage = finalDamage;

        shot = true;
        if(charged)
        {
            littleArrow.SetActive(false);
            largeArrow.SetActive(true);
            largeParticle.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (shot)
        {
            transform.SetParent(collision.transform);

            Destroy(GetComponent<Rigidbody>());

            largeParticle.Stop();
            smallParticle.Stop();

            if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<Health>().DecreaseHealth((int)damage);
            }

        }
    }
}
