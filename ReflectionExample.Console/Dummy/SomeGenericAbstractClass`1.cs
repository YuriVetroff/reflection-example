using ReflectionExample.Console.Dummy.GenericParams;

namespace ReflectionExample.Console.Dummy
{
    internal abstract class SomeGenericAbstractClass<T> : ISomeGenericInterface<T>
        where T : AbstractGenericParam
    {
        public virtual void SomeMethod(T input)
        {
            System.Console.WriteLine($"{GetType().Name}<{input.GetType().Name}>");
        }
    }
}
