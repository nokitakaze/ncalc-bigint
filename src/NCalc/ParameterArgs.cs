using System;

namespace NCalc
{
    public class ParameterArgs : EventArgs
    {
        private object _result;

        public object Result
        {
            get => _result;
            // ReSharper disable once UnusedMember.Global
            set
            {
                _result = value;
                HasResult = true;
            }
        }

        public bool HasResult { get; private protected set; }
    }
}