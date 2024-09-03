using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories
{
    public class InfoPanel : MonoBehaviour
    {
        public string Name
        {
            get
            {
                return _name.text;
            }
            set
            {
                _name.text = value;
            }
        }
        public string Description
        {
            get
            {
                return _description.text;
            }
            set
            {
                _description.text = value;
            }
        }

        public string Statistics
        {
            get
            {
                return _statistics.text;
            }
            set
            {
                _statistics.text = value;
            }
        }

        [SerializeField]
        private TextMeshProUGUI _name;

        [SerializeField]
        private TextMeshProUGUI _description;

        [SerializeField]
        private TextMeshProUGUI _statistics;
    }
}
