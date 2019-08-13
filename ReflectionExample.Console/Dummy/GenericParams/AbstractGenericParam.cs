namespace ReflectionExample.Console.Dummy.GenericParams
{
    internal abstract class AbstractGenericParam
    {
        public virtual void AnotherMethod()
        {
            System.Console.WriteLine(GetType());
        }
    }
}
