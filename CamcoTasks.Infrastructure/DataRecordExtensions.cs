// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CamcoTasks.Infrastructure.Data;

public static class DataRecordExtensions
{
    private static readonly ConcurrentDictionary<Type, object> Materializers = new();

    public static List<T> Translate<T>(this DbDataReader reader) where T : new()
    {
        var results = new List<T>();
        var properties = typeof(T).GetProperties();

        while (reader.Read())
        {
            var item = new T();
            foreach (var property in properties)
            {
                try
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        var value = reader.GetValue(reader.GetOrdinal(property.Name));
                        var propertyType = property.PropertyType;
                        if (propertyType.IsGenericType &&
                            propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            // Handle nullable types
                            var underlyingType = Nullable.GetUnderlyingType(propertyType);
                            value = Convert.ChangeType(value, underlyingType);
                        }
                        else
                        {
                            // Handle non-nullable types
                            value = Convert.ChangeType(value, propertyType);
                        }

                        property.SetValue(item, value);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception to understand the issue
                    Console.WriteLine($"Error mapping property {property.Name}: {ex.Message}");
                }
            }

            results.Add(item);
        }

        return results;
    }

    public static IList<T> Translate<T>(this DbDataReader reader, Func<IDataRecord, T> objectMaterializer)
    {
        return Translate(reader, objectMaterializer, out _);
    }

    private static IList<T> Translate<T>(this DbDataReader reader, Func<IDataRecord, T> objectMaterializer,
        out bool hasNextResult)
    {
        var results = new List<T>();
        while (reader.Read())
        {
            var record = (IDataRecord)reader;
            var obj = objectMaterializer(record);
            results.Add(obj);
        }

        hasNextResult = reader.NextResult();

        return results;
    }

    public static bool Exists(this IDataRecord record, string propertyName)
    {
        return Enumerable.Range(0, record.FieldCount).Any(x => record.GetName(x) == propertyName);
    }
}