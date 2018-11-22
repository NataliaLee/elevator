using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Windows.Buttons
{
    public class TextButton:HighlightedButton
    {
        [SerializeField]
        private Text _buttonText;

        public void SetText(string text)
        {
            _buttonText.text = text;
        }
    }
}
