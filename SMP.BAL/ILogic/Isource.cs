using SMP.BAL.DTO;
using SMP.Data.ModelForTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.ILogic
{
   public  interface Isource
    {


        DropDownListDTO Get_Geartype_source_datatypelist_for_dropdwn();


        int InsertKeyfield(KeyFieldDTO Model);

        List<KeyFieldDTO> ReturnKeyfield();



    }

}
