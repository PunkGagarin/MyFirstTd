using System;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float speed = 70f;
    public int damage = 50;
    public float exposionRadius = 0f;
    public GameObject impactEffect;

    public void Seek(Transform _target) {
        target = _target;
    }

    // Update is called once per frame
    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    private void HitTarget() {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        if (exposionRadius > 0) {
            Explode();
        } else {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, exposionRadius);
        foreach (Collider collider in colliders) {
            if (collider.tag == "Enemy") {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemy) {
        //TODO: разобраться зачем тут компонент, когда можно напрямую вызывать enemy.TakeDamage у Transform enemy
        Enemy en = enemy.GetComponent<Enemy>();

        //Todo: тоже непонятная проверка, зачем? связано с компонентом
        if (en != null) {
            en.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exposionRadius);
    }
}
