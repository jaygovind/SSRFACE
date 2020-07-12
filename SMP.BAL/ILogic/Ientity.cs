using SMP.BAL.DTO;
using SMP.Data.ModelForTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.BAL.ILogic
{
  public  interface Ientity
    {
        IEnumerable<GetCommentEntityDTO> getComments(int Entityid, int ReferenceFieldlistId);
        int SaveCommentEntity(CommentEntityParamDTO Model);
        int DeleteReference(int ReferenceId);
        int DeleteEntity(int EntityId);
        int UpdateReferenceField(ReferenceFieldListDTO Model);
        int SaveNewEntity(EntityDTO Model);
        int UpdateEntities(EntityDependenciesDTO Model);
       

      int  SaveNewFieldList_referenceField(ReferenceFieldListDTO Model);
        IEnumerable<EntityDependenciesDTO> GetEntitiesDependencyBysourceid(int opcode = 0, int sourceid = 0);

        IEnumerable<ReferenceFieldListDTO> GetFieldListDataorReferenceFielddata(int opcode = 0);

        IEnumerable<EntityDependenciesDTO> EditEntitiesDependencyEntityId(int opcode = 0, int Entityid = 0);

     


    }
}
