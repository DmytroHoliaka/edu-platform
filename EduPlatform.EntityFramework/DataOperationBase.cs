namespace EduPlatform.EntityFramework
{
    public class DataOperationBase
    {
        protected readonly EduPlatformDbContextFactory _contextFactory;

        public DataOperationBase(EduPlatformDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
