using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters.Enemies
{
    [CreateAssetMenu(fileName = "New enemy data", menuName = "Enemy data", order = 51)]
    public class EnemyStatistics : ScriptableObject
    {
        public float Chance { get => _chance; }

        public int Experience { get => _experience; }

        [SerializeField]
        private float _chance;

        [SerializeField]
        private int _experience;
    }
}
