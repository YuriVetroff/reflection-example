using ReflectionExample.Console.Dummy.GenericParams;

namespace ReflectionExample.Console.Dummy
{
    internal interface ISomeGenericInterface<T>
        where T: AbstractGenericParam
    {
        void SomeMethod(T input);
    }
}
