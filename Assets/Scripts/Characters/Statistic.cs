using Characters.States.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters
{
    [Serializable]
    public class Statistic
    {
        public StatisticType Type { get => _type; set => _type = value; }

        public float Value { get => _value; set => this._value = value; }

        [SerializeField]
        private StatisticType _type;

        [SerializeField]
        private float _value;

        [SerializeField]
        private bool _isNegative;

        public override string ToString()
        {
            var value = Value.ToString();

            if (value.StartsWith("-"))
            {
                value = value.Substring(1);
            }

            var color = _isNegative ? "red" : "green";
            var increaseOrDecrease = _isNegative ? "-" : "+";

            return $"<color={color}>{increaseOrDecrease} {value} {Type}</color>";
        }
    }
}
