using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.COMMON.Enums;
using SMP.Data.ModelForTables;
using SMP.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SMP.BAL.Logic
{
    public class SourceLogic : Isource
    {
        

      
        private readonly IRepository<KeyFieldDTO> _KeyFieldDTO;

        public SourceLogic(
            
            IRepository<KeyFieldDTO> KeyFieldDTO
            )
        {
           
            _KeyFieldDTO = KeyFieldDTO;
        }

        public DropDownListDTO Get_Geartype_source_datatypelist_for_dropdwn()
        {
            DropDownListDTO OBJ = new DropDownListDTO();

          

            return OBJ;
        }

       

        public int InsertKeyfield(KeyFieldDTO Model)
        {
            int returnid = 0;

         

           

           

           

            if(returnid>0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public List<KeyFieldDTO> ReturnKeyfield()
        {
           var KeyfieldList = new List<KeyFieldDTO>();
            try
            {
                string procName = SPROC_Names.Sp_Getkeyfieldlist.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@opcode", Value = 1, DbType = System.Data.DbType.String };

                KeyfieldList = _KeyFieldDTO.ExecuteWithJsonResult(procName, "KeyFieldsList", ParamsArray);
                return KeyfieldList;
            }
            catch(Exception ex)
            {

            }
            return KeyfieldList;
        }

       
    }
}
