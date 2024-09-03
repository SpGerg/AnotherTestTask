using Characters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventories.Items.Weapons.Bow
{
    public class Bow : Weapon
    {
        private GameObject _arrow;

        private Vector3 FirePosition => Character.transform.position + new Vector3(3, 0);

        public void Awake()
        {
            _arrow = Resources.Load("Weapons/Bow/Arrow") as GameObject;
        }

        public override void Attack(IDamageable target)
        {
            if (!IsCanAttack)
            {
                return;
            }

            var projectile = Instantiate(_arrow, FirePosition, Quaternion.identity);
            projectile.GetComponent<Arrow>().Initialize(Character.Statistics.AttackDamage);

            Destroy(projectile.gameObject, 5f);

            StartCoroutine(AttackReloadCoroutine());
        }
    }
}
