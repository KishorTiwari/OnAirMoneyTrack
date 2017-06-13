using Omack.Data.DAL;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Infrastructure.Repositories
{
    public class ItemRepository: GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(OmackContext context): base(context)
        {

        }
    }
    public interface IItemRepository: IGenericRepository<Item>
    {

    }
}
