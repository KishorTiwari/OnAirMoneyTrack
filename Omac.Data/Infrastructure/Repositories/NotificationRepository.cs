using Omack.Data.DAL;
using Omack.Data.Models;

namespace Omack.Data.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
	{
		public NotificationRepository(OmackContext context) : base(context)
		{

		}
	}


	public interface INotificationRepository : IGenericRepository<Notification>
	{

	}
}
