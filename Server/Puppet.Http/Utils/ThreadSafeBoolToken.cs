using System.Threading;

namespace Puppet.Http.Utils
{
    public class ThreadSafeBoolToken
    {
        /// <summary> Current state
        /// </summary>
        private int _isWorking = FALSE;
        
        /// <summary> Use constaints to treadsafe changing states
        /// </summary>
        private const int TRUE = 1;
        private const int FALSE = 0;
        
        public bool Value => _isWorking == TRUE ? true : false;

        public void ChangeValue(bool value)
        {
            Interlocked.Exchange(ref _isWorking, value ? TRUE : FALSE);
        }
    }
}