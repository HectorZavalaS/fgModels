//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fgModels.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class siixsem_sidesModel_td
    {
        public int se_id_detail { get; set; }
        public int se_id_model { get; set; }
        public int se_id_side { get; set; }
        public string se_part_num_tr { get; set; }
        public string se_int_part_num { get; set; }
        public string se_cust_part_num { get; set; }
        public string se_oracle_par_num { get; set; }
        public string se_assembly_name { get; set; }
    
        public virtual siixsem_models_t siixsem_models_t { get; set; }
        public virtual siixsem_sides_t siixsem_sides_t { get; set; }
    }
}