using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Features.Lift
{
    public class LiftButtonData
    {
        public int FloorNumber { get; private set; }

        public bool IsPushed { get; private set; }

        public LiftButtonData(int floorNumber)
        {
            FloorNumber = floorNumber;
            IsPushed = false;
        }

        public void OnClicked()
        {
            IsPushed = !IsPushed;
        }

        public void Reset()
        {
            IsPushed = false;
        }
    }
}
