namespace PersonalFinanceManager.API.Infrastructure.OpenApi;

public class CustomSchemaNameGenerator : DefaultSchemaNameGenerator
{
    private const string Suffix = "Model";

    public override string Generate(Type type)
    {
        var schemaName = base.Generate(type);

        if (schemaName.EndsWith(Suffix) && type.GetCustomAttribute<RemoveModelSuffixAttribute>() != null)
        {
            return schemaName[..^Suffix.Length];
        }

        return schemaName;
    }
}
