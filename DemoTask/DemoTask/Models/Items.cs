using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTask.Models
{
    public class DdlItems
    {
        public int Srno { get; set; }
        public string Itemname { get; set; }

    }
    public class Item
    {
        public int Itemcode { get; set; }
        public string Barcode { get; set; }
        public string Itemname { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string CreateDate { get; set; }
    }
    public class Res_Item
    {
        public int ec { get; set; }
        public string em { get; set; }
        public List<Item> Data { get; set; }

    }
    public class Res_PriceChange
    {
        public List<PriceChange> Data { get; set; }
    }
    public class PriceChange
    {
        public int Srno { get; set; }
        public int Itemcode { get; set; }
        public decimal Oldcost { get; set; }
        public string IncDec { get; set; }
        public string PriceType { get; set; }
        public string PriceUpdate { get; set; }
        public decimal Newcost { get; set; }
        public decimal Oldprice { get; set; }
        public decimal Newprice { get; set; }
    }
    public class Req_PriceChange
    {
        public int Srno { get; set; }
        public int Value { get; set; }
        public string IncDec { get; set; }
        public string PriceType { get; set; }
        public string PriceUpdate { get; set; }
    }
    public class Response
    {
        public int ec { get; set; }
        public string em { get; set; }

    }
    public class AddItem
    {
        public int Itemcode { get; set; }
        public string Barcode { get; set; }
        public string Itemname { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string CreateDate { get; set; }
    }
}
