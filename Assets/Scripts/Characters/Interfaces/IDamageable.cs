using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Characters.Interfaces
{
    public interface IDamageable
    {
        int MaxHealth { get; set; }

        //Из-за конфликтов имя класса и свойства добавил Value.
        int HealthValue { get; set; }
        
        bool IsAlive => HealthValue > 0;

        UnityEvent<int> HealthChanged { get; }

        UnityEvent Died { get; }

        void TakeDamage(int damage);

        void Heal(int heal);

        void Kill();
    }
}
