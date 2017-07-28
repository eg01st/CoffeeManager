using System;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.Core.Util
{
    public static class ParameterTransmitter
    {
        private static readonly Dictionary<Guid, object> parameters = new Dictionary<Guid, object>();

        public static Guid PutParameter(object parameterValue)
        {
            var id = Guid.NewGuid();
            parameters.Add(id, parameterValue);

            return id;
        }

        public static bool TryGetParameter<T>(Guid parameterId, out T parameterValue, bool removeParameter = true)
            where T : class
        {
            if (parameters.ContainsKey(parameterId) == false)
            {
                parameterValue = default(T);
                return false;
            }

            parameterValue = (T)parameters[parameterId];

            if (removeParameter)
            {
                parameters.Remove(parameterId);
            }

            return true;
        }

        public static void ClearAllParameters()
        {
            parameters.Clear();
        }
    }
}
