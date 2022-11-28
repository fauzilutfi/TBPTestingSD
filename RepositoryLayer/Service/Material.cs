using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class MaterialService
    {
        private readonly TBPContext context;
        public MaterialService(TBPContext TBPContext)
        {
            context = TBPContext;
        }
        public List<Material> GetAllByIdType(bool tipe)
        {
            bool? Tipe = (bool?)tipe;
            var result = context.Material.Where(o => o.TipeMaterial == Tipe).ToList();
            return result;
        }
        public Material GetById(int id)
        {
            var result = context.Material.FirstOrDefault(o => o.Id == id);
            return result;
        }
    }
}
