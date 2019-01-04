namespace Our.Umbraco.Checklist.CacheRefresher
{
    using System;

    using global::Umbraco.Core.Cache;
    using global::Umbraco.Core.Composing;

    using Our.Umbraco.Checklist.Constants;

    public class ChecklistCacheRefresher : JsonCacheRefresherBase<ChecklistCacheRefresher>
    {
        public ChecklistCacheRefresher(CacheHelper cacheHelper) : base(cacheHelper)
        {

        }

        protected override ChecklistCacheRefresher This
        {
            get { return this; }
        }

        public override Guid RefresherUniqueId
        {
            get { return CacheRefresherConstants.RuleCacheRefreshGuid; }
        }

        public override string Name
        {
            get { return "Checklist cache refresher"; }
        }

        public override void RefreshAll()
        {
            this.CacheHelper.RuntimeCache.ClearCacheByKeySearch(RuntimeCacheConstants.RuntimeCacheKeyPrefix);

            base.RefreshAll();
        }

        public static void ClearCache()
        {
            Current.CacheRefreshers[CacheRefresherConstants.RuleCacheRefreshGuid].RefreshAll();
        }
    }
}
