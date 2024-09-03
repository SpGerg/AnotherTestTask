using Inventories.Items.Abilities;
using Inventories.Items.Abilities.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventories.Items
{
    public class HealPoison : Item
    {
        public override ActiveAbility Ability { get; protected set; }

        public void OnEnable()
        {
            StartCoroutine(LateAwake());
        }

        private IEnumerator LateAwake()
        {
            yield return new WaitForEndOfFrame();

            Ability = new Abilities.Items.HealPoison(Character, gameObject);
        }
    }
}
