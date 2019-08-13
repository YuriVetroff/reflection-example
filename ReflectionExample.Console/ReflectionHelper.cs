using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionExample.Console
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> GetAllFinalChildren(
            Type parentType)
        {
            var allTypes = Assembly.GetAssembly(parentType)
                .GetTypes();

            var childrenTypes = allTypes
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && (t.IsSubclassOf(parentType)
                        || parentType.IsAssignableFrom(t)));

            return childrenTypes;
        }

        public static Type MakeGenericType(
            Type classType,
            params Type[] genericArguments) =>
                classType.MakeGenericType(genericArguments);

        public static void InvokeMethod(
            object instance,
            string methodName,
            params object[] parameters) =>
                instance.GetType()
                    .GetMethod(methodName)
                    .Invoke(instance, parameters);
    }
}
