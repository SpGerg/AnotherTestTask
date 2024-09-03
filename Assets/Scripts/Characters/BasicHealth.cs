using Characters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Characters
{
    public class BasicHealth : MonoBehaviour, IDamageable
    {
        public int MaxHealth { get; set; }

        public int HealthValue
        {
            get
            {
                return _health;
            }
            set
            {
                var oldValue = _health;

                _health = value;

                _health = Mathf.Clamp(_health, 1, MaxHealth);

                HealthChanged.Invoke(oldValue);
            }
        }

        public bool IsAlive => HealthValue > 0;

        [SerializeField]
        private Character _character;

        private CharacterStatistics _statistics => _character.Statistics;

        public UnityEvent<int> HealthChanged { get; } = new();

        public UnityEvent Died { get; } = new();

        private Font _font;

        private Canvas _canvas;

        [SerializeField]
        private int _health;

        public void Initialize()
        {
            MaxHealth = _statistics.MaxHealth;
            HealthValue = MaxHealth;

            _font = Resources.Load<Font>("Fonts/FFFFORWA");
            _canvas = FindAnyObjectByType<Canvas>();
        }

        public void OnDestroy()
        {
            Kill();
        }

        public void Kill()
        {
            Died.Invoke();
            Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            if (_health <= 0)
            {
                return;
            }

            damage -= _statistics.Armor;

            if (_health - damage <= 0)
            {
                Kill();

                _health = 0;

                return;
            }

            HealthValue -= damage;
            HintEffect(damage, Color.red);
        }

        public void Heal(int heal)
        {
            HealthValue += heal;

            HintEffect(heal, Color.green);
        }

        private void HintEffect(int damage, Color color)
        {
            var gameObject = new GameObject("Damage hit");
            gameObject.transform.SetParent(_canvas.transform, false);
            gameObject.transform.position = _character.transform.position;

            gameObject.AddComponent<DamageHint>();

            var text = gameObject.AddComponent<Text>();
            text.text = damage.ToString();
            text.font = _font;
            text.color = color;

            Destroy(gameObject, 3);
        }
    }
}
