namespace SmartHome.Configuration.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly ConfiguringContext _context;

        protected BaseRepository(ConfiguringContext context)
        {
            _context = context;
        }
    }
}
