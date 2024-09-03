using Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventories.Items.Abilities
{
    public abstract class ActiveAbility : Ability
    {
        protected ActiveAbility(Character character, GameObject parent) : base(character, parent)
        {
        }
    }
}
