using CamcoTasks.Infrastructure.EnumHelper;
using System;
using System.Linq;

namespace CamcoTasks.Infrastructure.EnumHelper;

public static class EnumHelper
{
    public static string GetTextFromEnum<TEnum, TEnumId>(TEnumId enumId) where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        var underlyingEnumType = Enum.GetUnderlyingType(typeof(TEnum));

        var enumValues = Enum.GetValues(enumType);

        foreach (var enumValue in enumValues)
        {
            var underlyingEnumValue = Convert.ChangeType(enumValue, underlyingEnumType);
            var underlyingEnumId = Convert.ChangeType(enumId, underlyingEnumType);

            if (underlyingEnumValue.Equals(underlyingEnumId))
            {
                var fieldInfo = enumType.GetField(enumValue.ToString());

                if (fieldInfo.GetCustomAttributes(typeof(CustomDisplayAttribute), false)
                        .FirstOrDefault() is CustomDisplayAttribute displayAttribute)
                {
                    return displayAttribute.DisplayText;
                }
            }
        }

        return "Not Found"; // Or some default text when no matching ID is found
    }

    public static TEnumId GetEnumValueFromText<TEnum, TEnumId>(string displayText)
        where TEnum : Enum where TEnumId : struct
    {
        var enumType = typeof(TEnum);
        var enumNames = Enum.GetNames(enumType);

        foreach (var enumName in enumNames)
        {
            var fieldInfo = enumType.GetField(enumName);

            if (fieldInfo.GetCustomAttributes(typeof(CustomDisplayAttribute), false)
                    .FirstOrDefault() is CustomDisplayAttribute displayAttribute &&
                displayAttribute.DisplayText == displayText)
            {
                if (Enum.TryParse(enumType, enumName, out object enumValue))
                {
                    if (Enum.TryParse<TEnumId>(enumValue.ToString(), out var result))
                    {
                        return result;
                    }
                }
            }
        }

        return (TEnumId)Convert.ChangeType(0, typeof(TEnumId));
    }
}