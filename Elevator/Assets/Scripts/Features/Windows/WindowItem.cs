using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Features.Windows
{
    [CreateAssetMenu(fileName="WindowItem", menuName="Create WindowItem")]
    public class WindowItem: ScriptableObject
    {
        public WindowType windowType;
        public GameObject windowPrefab;
    }
}
