
using FluentMigrator;

namespace api.Migrattions;

[Migration(202312041917)]
public class AddCatTable : Migration
{
    public override void Up()
    {
        Create.Table("cat")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString();
    }

    public override void Down()
    {
        Delete.Table("cat");
    }
}
