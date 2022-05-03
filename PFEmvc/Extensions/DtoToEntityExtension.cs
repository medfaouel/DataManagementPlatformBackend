using PFEmvc.dto;
using PFEmvc.Models;

namespace PFEmvc.Extensions
{
    public static class DtoToEntityExtension
    {
        public static Data ToEntity(this createData data)
        {
            return new Data
            {
                Context = data.Context,
                Fors_Material_Group = data.Fors_Material_Group,
                LEONI_Part = data.LEONI_Part,
                LEONI_Part_Classification = data.LEONI_Part_Classification,
                Month = data.Month,
                Part_Request = data.Part_Request,
                Supplier = data.Supplier
            };
        }
        

    }
}
