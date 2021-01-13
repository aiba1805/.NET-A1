using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Task2
{
    public static class ObjectExtensions
    {

		public static void SetReadOnlyProperty(this object obj, string propertyName, object newValue)
        {
            var type = obj.GetType();
            var property = obj.GetType().GetProperty(propertyName);
            var backingField = type
              .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
              .FirstOrDefault(field =>
                field.Attributes.HasFlag(FieldAttributes.Private) &&
                field.Attributes.HasFlag(FieldAttributes.InitOnly) &&
                field.CustomAttributes.Any(attr => attr.AttributeType == typeof(CompilerGeneratedAttribute)) &&
                (field.DeclaringType == property.DeclaringType) &&
                field.FieldType.IsAssignableFrom(property.PropertyType) &&
                field.Name.StartsWith("<" + property.Name + ">"));
            if(backingField == null)
            {
                type = type.BaseType ?? null;
                backingField = type
              .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
              .FirstOrDefault(field =>
                field.Attributes.HasFlag(FieldAttributes.Private) &&
                field.Attributes.HasFlag(FieldAttributes.InitOnly) &&
                field.CustomAttributes.Any(attr => attr.AttributeType == typeof(CompilerGeneratedAttribute)) &&
                (field.DeclaringType == property.DeclaringType) &&
                field.FieldType.IsAssignableFrom(property.PropertyType) &&
                field.Name.StartsWith("<" + property.Name + ">"));
            }
            backingField.SetValue(obj, newValue);
        }

        public static void SetReadOnlyField(this object obj, string fieldName, object newValue)
        {
            var field = obj.GetType().GetField(fieldName);
            var method = new DynamicMethod(
            name: "SetField",
            returnType: null,
            parameterTypes: new[] { obj.GetType(), typeof(string) },
            restrictedSkipVisibility: true
        );
            var gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field);
            gen.Emit(OpCodes.Ret);
            method.Invoke(null, new object[] { obj, newValue });
        }
    }
}
