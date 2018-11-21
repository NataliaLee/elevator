using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Features.Windows
{
    [CreateAssetMenu(fileName= "WindowSettings", menuName="Create windows settings")]
    public class WindowsSettings: ScriptableObject
    {
        public List<WindowItem> windowItems;
    }
}
