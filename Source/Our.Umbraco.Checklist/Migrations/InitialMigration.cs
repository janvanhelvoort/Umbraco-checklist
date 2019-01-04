namespace Our.Umbraco.Checklist.Migrations
{
    using global::Umbraco.Core.Migrations;

    using Our.Umbraco.Checklist.Constants;

    public class InitialMigration : MigrationBase
    {
        public InitialMigration(IMigrationContext context) : base(context)
        {

        }

        public override void Migrate()
        {
            if (!this.TableExists(TableConstants.Tasks.TableName))
            {
                this.Create.Table(TableConstants.Tasks.TableName)
                    .WithColumn("Id").AsInt16().NotNullable()
                        .PrimaryKey("PK_tasks").Identity()
                    .WithColumn("ContentNodeId").AsInt16().NotNullable()
                    .WithColumn("Content").AsString().NotNullable()
                    .WithColumn("IsReady").AsBoolean().NotNullable()
                    .WithColumn("IsDeleted").AsBoolean().NotNullable()
                    .Do();
            }
        }
    }
}
