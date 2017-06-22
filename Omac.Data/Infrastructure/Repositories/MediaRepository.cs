using Omack.Data.DAL;
using Omack.Data.Models;

namespace Omack.Data.Infrastructure.Repositories
{
	public class MediaRepository : GenericRepository<Media>, IMediaRepository
	{
		public MediaRepository(OmackContext context) : base(context)
		{

		}
	}


	public interface IMediaRepository : IGenericRepository<Media>
	{

	}
}
