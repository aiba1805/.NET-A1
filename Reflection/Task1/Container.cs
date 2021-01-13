using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {

        private List<Type> registeredTypes = new List<Type>();
        private List<(Type, Type)> registeredBaseTypes = new List<(Type, Type)>();

        public void AddAssembly(Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.CustomAttributes.Any(x => x.AttributeType == typeof(ImportConstructorAttribute)))
                    AddType(type);
                if (type.CustomAttributes.Any(x => x.AttributeType == typeof(ExportAttribute))){
                    var attr = (ExportAttribute)Attribute.GetCustomAttribute(type, typeof(ExportAttribute));
                    if (attr.Contract != null)
                        AddType(type, attr.Contract);
                    else
                        AddType(type);
                }
                var hasImportProperties = type.GetProperties()
                .Any(x => x.CustomAttributes
                .Any(y => y.AttributeType == typeof(ImportAttribute)));
                if(hasImportProperties)
                    AddType(type);
            };
        }

        public void AddType(Type type)
        {
            registeredTypes.Add(type);
        }

        public void AddType(Type type, Type baseType)
        {
            registeredBaseTypes.Add((type,baseType));
        }

        public T Get<T>()
        {
            if (!registeredTypes.Exists(x => x == typeof(T))
                && !registeredBaseTypes.Any(x=>x.Item2 == typeof(T))) throw new Exception();
            var type = typeof(T);
            if (registeredBaseTypes.Any(x => x.Item2 == typeof(T))) 
                type = registeredBaseTypes.Find(x => x.Item2 == typeof(T)).Item1;

            var importConstructor = type.CustomAttributes.Any(x=>x.AttributeType == typeof(ImportConstructorAttribute));
            if (importConstructor)
            {
                var constructor = type.GetConstructors().FirstOrDefault(x => x.GetParameters().Length > 0);
                var parameters = new List<object>();
                foreach (var parameter in constructor.GetParameters())
                {
                    var t = parameter.ParameterType;
                    parameters.Add(this.GetType().GetMethod("Get")
                        .MakeGenericMethod(t).Invoke(this, null));
                }
                return (T)constructor.Invoke(parameters.ToArray());
            }

            var importProperties = type.GetProperties()
                .Where(x=> x.CustomAttributes
                .Any(y => y.AttributeType == typeof(ImportAttribute)));
            var typeValue = (T)Activator.CreateInstance(type);
            foreach (var property in importProperties)
            {
                property.SetValue(typeValue, this.GetType().GetMethod("Get")
                    .MakeGenericMethod(property.PropertyType).Invoke(this, null));
            }
            return typeValue;
        }
    }
}