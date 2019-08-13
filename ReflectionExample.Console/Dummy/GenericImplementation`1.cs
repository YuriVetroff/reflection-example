using ReflectionExample.Console.Dummy.GenericParams;

namespace ReflectionExample.Console.Dummy
{
    internal class GenericImplementation<T> : SomeGenericAbstractClass<T>
        where T : AbstractGenericParam
    {
    }
}
