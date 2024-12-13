using DemoTask.IRepository;
using DemoTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTask.Repository
{
    public class DemoTaskRepository : IDemoTaskRepository
    {
        clsDB dbNew = new clsDB(Config.ConStr);

        public Res_Item GetAllItems()
        {
            try
            {
                Res_Item res = new Res_Item();
                List<Item> lstop = new List<Item>();
                DataSet RetDs = new DataSet();
                RetDs = dbNew.ExecuteDataSet("SP_GetAllItems");
                if (RetDs.Tables.Count > 0 && RetDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < RetDs.Tables[0].Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Itemcode = Convert.ToInt32(RetDs.Tables[0].Rows[i]["Itemcode"]);
                        item.Itemname = Convert.ToString(RetDs.Tables[0].Rows[i]["Itemname"]);
                        item.Barcode = Convert.ToString(RetDs.Tables[0].Rows[i]["Barcode"]);
                        item.Cost = Convert.ToInt32(RetDs.Tables[0].Rows[i]["Cost"]);
                        item.Price = Convert.ToInt32(RetDs.Tables[0].Rows[i]["Price"]);
                        lstop.Add(item);
                    }
                    res.Data = lstop;
                    res.ec = 0;
                    res.em = "success";
                }
                else
                {
                    res.ec = -301;
                    res.em = "Data not found";
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
        public Response AddItems(AddItem addItem)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("Itemcode", addItem.Itemcode);
                p[1] = new SqlParameter("Itemname", addItem.Itemname);
                p[2] = new SqlParameter("Barcode", addItem.Barcode);
                p[3] = new SqlParameter("Cost", addItem.Cost);
                p[4] = new SqlParameter("Price", addItem.Price);
                p[5] = new SqlParameter("CreateDate", addItem.CreateDate);
                DataSet RetDs = new DataSet();
                RetDs = dbNew.ExecuteDataSet("SP_OnSaveItems", p);
                if (RetDs.Tables.Count > 0 && RetDs.Tables[0].Rows.Count > 0)
                {
                    res.ec = Convert.ToInt16(RetDs.Tables[0].Rows[0]["RetVal"]);
                    res.em = Convert.ToString(RetDs.Tables[0].Rows[0]["Msg"]);
                }
                else
                {
                    res.ec = -301;
                    res.em = "Data not found";
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }


        public List<DdlItems> GetDdlData()
        {
            try
            {
                SqlParameter[] p;
                DataTable dt;
                List<DdlItems> list = new List<DdlItems>();
                dt = dbNew.ExecuteDataTable("SP_DdlItems");

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DdlItems ddlItems = new DdlItems();
                        ddlItems.Srno = Convert.ToInt32(dt.Rows[i]["Srno"]);
                        ddlItems.Itemname = Convert.ToString(dt.Rows[i]["Itemname"]);
                        list.Add(ddlItems);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


        }
        public Res_PriceChange PriceChanges(Req_PriceChange req_Price)
        {
            Res_PriceChange res_Price = new Res_PriceChange();
            List<PriceChange> Pricelist = new List<PriceChange>();
            try
            {
                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("Srno", req_Price.Srno);
                p[1] = new SqlParameter("Value", req_Price.Value);
                p[2] = new SqlParameter("IncDec", req_Price.IncDec);
                p[3] = new SqlParameter("PriceType", req_Price.PriceType);
                p[4] = new SqlParameter("PriceUpdate", req_Price.PriceUpdate);
                DataSet RetDs = new DataSet();
                RetDs = dbNew.ExecuteDataSet("SP_OnPriceChange", p);
                if (RetDs.Tables.Count > 0 && RetDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < RetDs.Tables[0].Rows.Count; i++)
                    {
                        PriceChange pc = new PriceChange();
                        pc.Srno = Convert.ToInt32(RetDs.Tables[0].Rows[0]["Srno"]);
                        pc.Itemcode = Convert.ToInt32(RetDs.Tables[0].Rows[0]["Itemcode"]);
                        pc.Oldcost = Convert.ToDecimal(RetDs.Tables[0].Rows[0]["Oldcost"]);
                        pc.IncDec = Convert.ToString(RetDs.Tables[0].Rows[0]["Increase_Decrease"]);
                        pc.PriceType = Convert.ToString(RetDs.Tables[0].Rows[0]["PriceType"]);
                        pc.PriceUpdate = Convert.ToString(RetDs.Tables[0].Rows[0]["PriceUpdate"]);
                        pc.Newcost = Convert.ToDecimal(RetDs.Tables[0].Rows[0]["Newcost"]);
                        pc.Oldprice = Convert.ToDecimal(RetDs.Tables[0].Rows[0]["Oldprice"]);
                        pc.Newprice = Convert.ToDecimal(RetDs.Tables[0].Rows[0]["Newprice"]);
                        Pricelist.Add(pc);
                    }
                    res_Price.Data = Pricelist;
                }
                else
                {
                }
                return res_Price;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }



    }
}
