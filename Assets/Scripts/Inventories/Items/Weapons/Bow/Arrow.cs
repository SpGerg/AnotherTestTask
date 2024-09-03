using Characters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventories.Items.Weapons.Bow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private int _damage;

        public void Initialize(int damage)
        {
            _damage = damage;
        }

        public void Update()
        {
            //Плохо, но не хочется усложнять
            transform.Translate(_speed * Time.deltaTime * transform.right);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                return;
            }

            damageable.TakeDamage(_damage);

            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<IDamageable>(out var damageable))
            {
                return;
            }

            damageable.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}
