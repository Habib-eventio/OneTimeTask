using System;

namespace CamcoTasks.Infrastructure.EnumHelper;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class CustomDisplayAttribute : Attribute
{
    public string DisplayText { get; }

    public CustomDisplayAttribute(string displayText)
    {
        DisplayText = displayText;
    }
}