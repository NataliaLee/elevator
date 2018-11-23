using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Features.Floors
{
    public class FloorData
    {
        public int FloorNumber { get; private set; }

        public bool UpIsPushed { get; private set; }

        public bool DownIsPushed { get; private set; }

        public FloorData(int floorNumber)
        {
            FloorNumber = floorNumber;
            UpIsPushed = false;
            DownIsPushed = false;
        }

        public void OnUpClicked()
        {
            UpIsPushed = !UpIsPushed;
        }

        public void OnDownClicked()
        {
            DownIsPushed = !DownIsPushed;
        }

        public void Reset()
        {
            UpIsPushed = false;
            DownIsPushed = false;
        }
    }
}
