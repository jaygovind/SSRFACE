using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMPAPI.Model;

namespace SMPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly Ientity _Entity;
        private readonly Isource _source;

        private readonly Ientity _entity;
        public EntityController(
            Ientity Entity,
             Isource source,
            Ientity entity
            )
        {
            _Entity = Entity;
            _source = source;
            _entity = entity;
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("SaveNewEntity")]
        [Obsolete]
        public async Task<IActionResult> SaveNewEntity([FromBody]EntityDTO model)
        {

          var Returnid=  _Entity.SaveNewEntity(model);

            return Ok(Returnid);

        }



        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("NewReferencesave")]
        [Obsolete]
        public async Task<IActionResult> NewReferencesave([FromBody]ReferenceFieldListDTO model)
        {

           var Returnid = _Entity.SaveNewFieldList_referenceField(model);

            return Ok(Returnid);

        }



        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("CommentEntity")]
        [Obsolete]
        public async Task<IActionResult> CommentEntity([FromBody]CommentEntityParamDTO model)
        {

            var Returnid = _Entity.SaveCommentEntity(model);
            var Returndata= _Entity.getComments(model.Entityid_Int,model.ReferenceField_Int);
            return Ok(Returndata);

        }

        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetCommentEntity")]
        [Obsolete]
        public async Task<IActionResult> GetCommentEntity([FromBody]CommentEntityParamDTO model)
        {

            var Returndata = _Entity.getComments(model.Entityid_Int,model.ReferenceField_Int);

            return Ok(Returndata);

        }




        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetEntityDependeciesalllist")]
        [Obsolete]
        public async Task<IActionResult> GetEntityDependeciesalllist(int sourceid=0)
        {
            int Opcode = 0;
            if(sourceid==0)
            {
                Opcode = 2;
            }
            else
            {
                Opcode = 1;
            }
            var ReturnAllentityReferenceLinklist = _Entity.GetEntitiesDependencyBysourceid(Opcode, sourceid);

            return Ok(ReturnAllentityReferenceLinklist);

        }


        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetFieldListorReferencefileddata")]
        [Obsolete]
        public async Task<IActionResult> GetFieldListorReferencefileddata()
        {
            int Opcode = 3;

            var ReturnFieldList = _Entity.GetFieldListDataorReferenceFielddata(Opcode);

            return Ok(ReturnFieldList);

        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("EditEntityById")]
        [Obsolete]
        public async Task<IActionResult> EditEntityById([FromBody] ParametersCatch Model)
        {
            int Opcode = 1;

            var ReturnEntity = _Entity.EditEntitiesDependencyEntityId(Opcode,Convert.ToInt32(Model.Entityid));
            var Getall = _source.Get_Geartype_source_datatypelist_for_dropdwn();
            var ReturnFieldList = _entity.GetFieldListDataorReferenceFielddata(3);

            return Ok(new
            {
                ReturnEntity= ReturnEntity,
                getallGearsourcedatatype = Getall,
                ReturnFieldList= ReturnFieldList

            });
        }



        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("UpdateEntityById")]
        [Obsolete]
        public async Task<IActionResult> UpdateEntityById([FromBody] EntityDependenciesDTO Model)
        {


            var ReturnEntity = _Entity.UpdateEntities(Model);

            return Ok(ReturnEntity);
        }



        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("DeleteEntityById")]
        [Obsolete]
        public async Task<IActionResult> DeleteEntityById([FromBody] ParametersCatch Model)
        {
            try
            {

                var ReturnEntity = _Entity.DeleteEntity(Convert.ToInt32(Model.Entityid));

                var ReturnAllentityReferenceLinklist = _Entity.GetEntitiesDependencyBysourceid(2, 0);

                return Ok(ReturnAllentityReferenceLinklist);
            }
            catch(Exception ex)
            {

            }

            return Ok();
        }

    }
}