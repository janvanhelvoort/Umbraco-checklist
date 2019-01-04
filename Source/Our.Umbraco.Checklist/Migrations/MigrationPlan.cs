namespace Our.Umbraco.Checklist.Migrations
{
    using global::Umbraco.Core.Migrations;

    public class ChecklistMigrationPlan : MigrationPlan
    {
        public ChecklistMigrationPlan() : base("Checklist")
        {
            From(string.Empty)
                .To<InitialMigration>("initial-migration");
        }
    }
}
