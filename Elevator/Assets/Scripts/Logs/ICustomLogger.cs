using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Logs
{
    public interface ICustomLogger
    {
        void Log(string message);
        void LogError(string message);
    }
}
