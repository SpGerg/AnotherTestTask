using Characters;
using UnityEngine;

namespace Inventories.Items.Abilities.Items
{
    public class HealPoison : ActiveAbility
    {
        public HealPoison(Character character, GameObject parent) : base(character, parent)
        {
        }

        //Прокачку способностей через ветку прокачки можно реализовать черел словарь где ключ строка с названием
        //Значения, а значения само значения. Но т.к у нас нету деревьев, сделаем пока так.
        private const int Heal = 25;

        public override void Execute()
        {
            Character.Health.Heal(Heal);

            if (!Parent.TryGetComponent<Item>(out var item))
            {
                return;
            }

            Character.Inventory.RemoveItem(item.ItemData.Type);
        }
    }
}
