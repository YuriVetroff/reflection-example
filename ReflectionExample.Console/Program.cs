using ReflectionExample.Console.Dummy;
using ReflectionExample.Console.Dummy.GenericParams;
using System;
using static System.Console;

namespace ReflectionExample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunExample();
            ReadLine();
        }

        /// <summary>
        /// 
        /// Есть базовый абстрактный класс AbstractGenericParam.
        /// У него есть конкретных наследника: GenericParamA, GenericParamB, GenericParamC.
        /// 
        /// Эти наследники используются в качестве generic-параметров
        /// для реализации интерфейса ISomeGenericInterface<T>
        /// и абстрактного класса SomeGenericAbstractClass<T>, вот так:
        ///     class ImplementationForParamA : SomeGenericAbstractClass<GenericParamA>
        ///     class ImplementationForParamB : SomeGenericAbstractClass<GenericParamB>
        /// и так далее.
        /// 
        /// Также в модели присутствует generic-класс GenericImplementation<T>,
        /// который наследуется от SomeGenericAbstractClass<T>
        /// и может функционировать с любым из наследников AbstractGenericParam.
        /// 
        /// </summary>
        static void RunExample()
        {
            FindImplementationsOfGenericType();
            BuildConcreteGenericType();
        }

        /// <summary>
        /// 
        /// В этом методе производится поиск всех конкретных (не абстрактных) наследников AbstractGenericParam.
        /// Затем ищутся конкретные реализации generic-интерфейса ISomeGenericInterface<T> по результатам действия выше.
        /// 
        /// Для этого строится generic-тип с помощью метода MakeGenericType,
        /// который подставляет для базового типа (в данном случае typeof(ISomeGenericInterface<>))
        /// заданные generic-параметры (один из наследников AbstractGenericParam).
        /// 
        /// После для каждого найденного типа создаётся объект и вызывается метод SomeMethod.
        /// 
        /// </summary>
        static void FindImplementationsOfGenericType()
        {
            WriteLine(nameof(FindImplementationsOfGenericType));
            WriteLine("===============================");

            var allGenericParams = ReflectionHelper.GetAllFinalChildren(
                typeof(AbstractGenericParam));

            foreach (var genericParam in allGenericParams)
            {
                WriteLine($"Generic parameter: {genericParam.Name.ToString()}");

                var genericType = ReflectionHelper.MakeGenericType(
                    typeof(ISomeGenericInterface<>),
                    genericParam);

                var implementationsOfGenericType = ReflectionHelper
                    .GetAllFinalChildren(genericType);

                foreach (var impl in implementationsOfGenericType)
                {
                    ReflectionHelper.InvokeMethod(
                        Activator.CreateInstance(impl),
                        "SomeMethod",
                        Activator.CreateInstance(genericParam));
                }

                WriteLine();
            }
        }

        /// <summary>
        /// В отличие от FindImplementationsOfGenericType, здесь не ищутся конкретные реализации generic-интерфейса,
        /// а строится реализация обобщённого generic-класса, который может работать с любым из generic-параметров.
        /// В данном случае это класс GenericImplementation<T>.
        /// 
        /// Начало метода аналогично предыдущему: ищутся все не абстрактные наследники AbstractGenericParam.
        /// Затем для каждого из них создаётся generic-тип на основе typeof(GenericImplementation<>).
        /// 
        /// После для него создаётся объект и вызывается метод SomeMethod.
        /// </summary>
        static void BuildConcreteGenericType()
        {
            WriteLine(nameof(BuildConcreteGenericType));
            WriteLine("===============================");

            var allGenericParams = ReflectionHelper.GetAllFinalChildren(
                typeof(AbstractGenericParam));

            foreach (var genericParam in allGenericParams)
            {
                WriteLine($"Generic parameter: {genericParam.Name.ToString()}");

                var genericType = ReflectionHelper.MakeGenericType(
                    typeof(GenericImplementation<>),
                    genericParam);

                ReflectionHelper.InvokeMethod(
                    Activator.CreateInstance(genericType),
                    "SomeMethod",
                    Activator.CreateInstance(genericParam));

                WriteLine();
            }
        }
    }
}
