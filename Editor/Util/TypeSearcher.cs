using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;

public static class TypeSearcher
{
    readonly static IEnumerable<Type> AllTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t => t.IsPublic);

    readonly static Dictionary<string, Type> BuiltInTypes = GetBuiltInTypes();

    // This returns like "int" => System.Int32
    static Dictionary<string, Type> GetBuiltInTypes()
    {
        var builtIntypes = new Dictionary<string, Type>();

        // Resolve reference to mscorlib.
        // int is an arbitrarily chosen type in mscorlib
        var mscorlib = Assembly.GetAssembly(typeof(int));

        using var provider = new CSharpCodeProvider();

        foreach (var type in mscorlib.DefinedTypes)
        {
            if (string.Equals(type.Namespace, "System"))
            {
                var typeRef = new CodeTypeReference(type);
                var csTypeName = provider.GetTypeOutput(typeRef);

                // Ignore qualified types.
                if (csTypeName.IndexOf('.') == -1)
                {
                    builtIntypes[csTypeName] = type;
                }
            }
        }

        return builtIntypes;
    }

    public static Type[] SearchForType(string typeName)
    {
        // If the typeName contains a '.' then search with namespace, else just search by name
        bool includeNamespaces = typeName.Contains('.');
        bool caseSensitive = typeName.Any(char.IsUpper);

        // Include built in types if the search doesn't include namespaces and is case insensitive
        if (!includeNamespaces && !caseSensitive)
        {
            if (BuiltInTypes.ContainsKey(typeName))
            {
                return new [] { BuiltInTypes[typeName] };
            }
        }

        var stringComparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        bool matchesTypeName(Type t) =>
            typeName.Equals(includeNamespaces ? t.FullName : t.Name, stringComparison);

        return AllTypes
            .Where(matchesTypeName)
            .ToArray();
    }

    public static bool IsBuiltInType(Type type) => BuiltInTypes.ContainsValue(type);
}
