using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class Detail
    {
        private readonly TBPContext context;
        public Detail(TBPContext TBPContext)
        {
            context = TBPContext;
        }
        public DetilSpr GetById(int id)
        {
            return context.DetilSpr.FirstOrDefault(o => o.Id == id);
        }
        public DetilSpr GetByIdRef(int idRef)
        {
            return context.DetilSpr.FirstOrDefault(o => o.IdRef == idRef);
        }
        public string Remove(int id)
        {
            string result = string.Empty;
            var data = context.DetilSpr.FirstOrDefault(o => o.Id == id);
            if (data != null)
            {
                context.DetilSpr.Remove(data);
                result = "pass";
            }
            return result;
        }
        public string AddNew(DetilSpr obj)
        {
            try
            {
                string result = string.Empty;

                var newDetail = new DetilSpr();
                newDetail.IdRef = obj.IdRef;
                newDetail.Material = obj.Material;
                newDetail.Unit = obj.Unit;
                newDetail.Volume = obj.Volume;
                newDetail.StatusDisetujui = obj.StatusDisetujui;
                newDetail.TanggalRencanaDiterima = obj.TanggalRencanaDiterima;

                var updatedHeader = context.HeaderSpr.FirstOrDefault(c => c.Id == obj.IdRef);
                updatedHeader.AdaDetil = true;

                context.DetilSpr.Add(newDetail);
                context.SaveChanges();

                result = "pass";

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update(DetilSpr obj)
        {
            try
            {
                var existedData = context.DetilSpr.FirstOrDefault(c => c.Id == obj.Id);

                existedData.StatusDisetujui = obj.StatusDisetujui;
                existedData.Unit = obj.Unit;
                existedData.Volume = obj.Volume;
                existedData.Material = obj.Material;
                existedData.TanggalRencanaDiterima = obj.TanggalRencanaDiterima;

                context.SaveChanges();

                string result  = "pass";
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
