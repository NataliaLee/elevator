using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Windows.Buttons
{
    public class HighlightedButton: MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _highlightImage;

        private Action _onClick;

        public void SetOnClickAction(Action onClick)
        {
            _onClick = onClick;
        }

        public void SetHighlighted(bool hightLighted)
        {
            _highlightImage.gameObject.SetActive(hightLighted);
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke();
        }
    }
}
