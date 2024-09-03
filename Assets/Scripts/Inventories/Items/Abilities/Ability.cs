using Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventories.Items.Abilities
{
    public abstract class Ability
    {
        public Ability(Character character, GameObject parent)
        {
            Character = character;
            Parent = parent;
        }

        public Character Character { get; }

        public GameObject Parent { get; }

        public virtual Cooldown Cooldown { get; } = new Cooldown();

        public void ExecuteWithCooldown()
        {
            if (!Cooldown.IsReady)
            {
                return;
            }

            Execute();
            Cooldown.ForceUse();
        }

        public abstract void Execute();
    }
}
