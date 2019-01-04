namespace Our.Umbraco.Checklist.Models.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using global::Umbraco.Core.Composing;
    using global::Umbraco.Core.Persistence;

    using Our.Umbraco.Checklist.Constants;
    using Our.Umbraco.Checklist.Models.Pocos;

    internal class TaskRepository
    {
        public static readonly TaskRepository Instance = new TaskRepository();        

        public IEnumerable<Task> GetBy(Expression<Func<Task, bool>> predicate)
        {
            using (var scope = Current.ScopeProvider.CreateScope())
            {
                var query = scope.SqlContext.Sql().Select("*").From(TableConstants.Tasks.TableName).Where(predicate);

                return scope.Database.Fetch<Task>(query);
            }
        }

        public Task Save(Task task)
        {
            if (task != null)
            {
                using (var scope = Current.ScopeProvider.CreateScope())
                {
                    if (task.Id > 0)
                    {
                        scope.Database.Update(task);
                    }
                    else
                    {
                        scope.Database.Save(task);
                    }

                    scope.Complete();
                }
            }

            return task;
        }
    }
}
