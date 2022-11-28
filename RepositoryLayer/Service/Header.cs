using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using RepositoryLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryLayer.ViewModels;

namespace RepositoryLayer.Service
{
    public class Header
    {
        private readonly TBPContext context;
        public Header(TBPContext TBPContext)
        {
            context = TBPContext;
        }
        public List<HeaderSpr> GetAll(filterHeaderSPR filter)
        {
            var list = context.HeaderSpr.ToList();
            if (!String.IsNullOrEmpty(filter.Penyetujuan))
            {
                list = list.Where(c => c.TujuanSpr == filter.Penyetujuan).ToList();
            }
            if (filter.AdaDetil == "ada")
            {
                list = list.Where(c => c.AdaDetil == true).ToList();
            }
            else if (filter.AdaDetil == "tidak")
            {
                list = list.Where(c => c.AdaDetil == false).ToList();
            }
            if (filter.Status == "aktif")
            {
                list = list.Where(c => c.Status == true).ToList();
            }
            else if (filter.Status == "tidak")
            {
                list = list.Where(c => c.Status == false).ToList();
            }

            return list;
        }
        public HeaderSpr GetById(int id)
        {
            return context.HeaderSpr.FirstOrDefault(o => o.Id == id);
        }
        public string Remove(int id)
        {
            string result = string.Empty;
            var data = context.HeaderSpr.FirstOrDefault(o => o.Id == id);
            if (data != null)
            {
                context.HeaderSpr.Remove(data);
                result = "pass";
            }
            return result;
        }
        public string Save(HeaderSpr obj)
        {
            try
            {
                string result = string.Empty;
                //data = new HeaderSpr();
                string sqlCom = $"Exec spSaveHeaderSPR @Id = {obj.Id}, @TujuanSpr = '{obj.TujuanSpr}', @Status = {obj.Status}, @Proyek = {1283}, @Peminta = '{obj.Peminta}', @LokasiPeminta = '{obj.LokasiPeminta}', @NamaPenyetuju1 = '{obj.NamaPenyetuju1}', @NamaPenyetuju2 = '{obj.NamaPenyetuju2}'";
                context.Database.ExecuteSqlRaw(sqlCom);
                result = "pass";

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<string> GetDataPenyetujuan()
        {
            List<string> result = context.HeaderSpr.Select(o => o.TujuanSpr).ToList();
            return result;
        }
    }
}
