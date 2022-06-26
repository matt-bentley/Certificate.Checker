namespace Certificate.Checker.Filters
{
    public class PathBaseStartupFilter : IStartupFilter
    {
        private readonly string _pathBase;

        public PathBaseStartupFilter(string pathBase)
        {
            _pathBase = pathBase;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UsePathBase(_pathBase);
                next(app);
            };
        }
    }
}
