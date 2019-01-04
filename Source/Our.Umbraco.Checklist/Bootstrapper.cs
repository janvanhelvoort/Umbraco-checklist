namespace Our.Umbraco.Checklist
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using global::Umbraco.Core.Components;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Migrations;
    using global::Umbraco.Core.Migrations.Upgrade;
    using global::Umbraco.Core.Scoping;
    using global::Umbraco.Core.Services;
    using global::Umbraco.Web;
    using global::Umbraco.Web.UI.JavaScript;

    using Our.Umbraco.Checklist.Controllers.ApiControllers;
    using Our.Umbraco.Checklist.Mapping.Profile;
    using Our.Umbraco.Checklist.Migrations;

    public class BootStrapper : UmbracoComponentBase, IUmbracoUserComponent
    {
        public override void Compose(Composition composition)
        {
            base.Compose(composition);

            composition.Container.Register<TaskProfileProfile>();

            ServerVariablesParser.Parsing += this.ServerVariablesParserParsing;
        }

        public void Initialize(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            var upgrader = new Upgrader(new ChecklistMigrationPlan());
            upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);
        }

        private void ServerVariablesParserParsing(object sender, Dictionary<string, object> e)
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }

            if (!e.Keys.Contains("checklist"))
            {
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

                var urlDictionairy = new Dictionary<string, object>
                {
                    { "getTasks", urlHelper.GetUmbracoApiService<TasksApiController>("Get") },
                    { "saveTask", urlHelper.GetUmbracoApiService<TasksApiController>("Post") },
                    { "deleteTask", urlHelper.GetUmbracoApiService<TasksApiController>("Delete") }
               };

                e.Add("checklist", urlDictionairy);
            }
        }
    }
}