namespace Our.Umbraco.Checklist.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    using AutoMapper;

    using global::Umbraco.Core.Composing;

    using Our.Umbraco.Checklist.Constants;
    using Our.Umbraco.Checklist.Models;
    using Our.Umbraco.Checklist.Models.Repositories;

    /// <summary>
    /// The task server
    /// </summary>
    internal class TaskService
    {
        /// <summary>
        /// The current instance.
        /// </summary>
        private static TaskService instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="TaskService"/> class from being created.
        /// </summary>
        private TaskService()
        {
            instance = this;
        }

        /// <summary>
        /// Gets the current instance
        /// </summary>
        public static TaskService Instance => instance ?? new TaskService();

        // Tasks
        private IEnumerable<Task> Tasks
        {
            get
            {
                return (IEnumerable<Task>)Current.ApplicationCache.RuntimeCache.GetCacheItem(RuntimeCacheConstants.RuntimeCacheKeyPrefix, () =>
                {
                    return Mapper.Map<IEnumerable<Task>>(TaskRepository.Instance.GetBy(task => !task.IsDeleted));
                }, TimeSpan.FromMinutes(RuntimeCacheConstants.DefaultExpiration), true);
            }
        }

        //Get by content node Id
        public IEnumerable<Task> GetByContentNodeId(int contentNodeId)
        {
            return this.Tasks.Where(task => task.ContentNodeId.Equals(contentNodeId));
        }

        // Get by Id
        public Task GetById(int id)
        {
            return this.Tasks.SingleOrDefault(task => task.Id.Equals(id));
        }

        // save task
        public Task Save(Task task)
        {
            try
            {
                return Mapper.Map<Task>(
                    TaskRepository.Instance.Save(
                        Mapper.Map<Models.Pocos.Task>(task)));
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}
