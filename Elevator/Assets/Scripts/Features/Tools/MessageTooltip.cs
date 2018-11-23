using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Tools
{
    public class MessageTooltip: MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void Show(string message)
        {
            _text.text = message;
            Destroy(gameObject,1f);
        }

    }
}
