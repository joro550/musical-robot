
using FluentMigrator;

namespace api.Migrattions;

[Migration(202312041917)]
public class AddCatTable : Migration
{
    public override void Up()
    {
        Create.Table("Cat")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsString();
    }

    public override void Down()
    {
        Delete.Table("Cat");
    }
}
