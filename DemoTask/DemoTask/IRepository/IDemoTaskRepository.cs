using DemoTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTask.IRepository
{
    public interface IDemoTaskRepository
    {
        Response AddItems(AddItem addItem);
        Res_Item GetAllItems();
        Res_PriceChange PriceChanges(Req_PriceChange req_Price);
        List<DdlItems> GetDdlData();
    }
}
