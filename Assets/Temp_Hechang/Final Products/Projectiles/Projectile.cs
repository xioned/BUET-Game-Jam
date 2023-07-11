using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum Projectile_Color
    {
        Red,
        Blue,
        Purple
    }

    public Projectile_Color color;

    public float damage;
    bool grabbed;

    public void ThrewAgain(float holdTime)
    {
        damage = damage * holdTime;
        Destroy(gameObject, 8f);
        transform.SetParent(null);
        grabbed = false;
    }

    public void StartingJourney(float _damage)
    {
        damage = _damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Holder"))
        {
            return;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Health>().DecreaseHealth((int)damage);
        }

        if(!grabbed)
            Destroy(gameObject);
    }

    public bool TryToGrab(Projectile_Color grabColor, Transform position)
    {
        Debug.Log("I am of color : " + color);
        Debug.Log("Trying to grab with : " + grabColor);
        Debug.Log("So, I got caught : " + (color == grabColor));

        if(color == grabColor)
        {
            grabbed = true;
            transform.SetParent(position);
            GetComponent<Rigidbody>().isKinematic = true;
            transform.localPosition = Vector3.zero;
            transform.localRotation = position.localRotation;
            return true;
        }
        else
        {
            return false;
        }
    }
}
