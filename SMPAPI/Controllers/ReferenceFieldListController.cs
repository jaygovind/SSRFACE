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
    public class ReferenceFieldListController : ControllerBase
    {
        private readonly Ientity _Entity;
        private readonly Isource _source;

        public ReferenceFieldListController(
            Ientity Entity,
             Isource source
            )
        {
            _Entity = Entity;
            _source = source;
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("EditReferencefieldListById")]

        public async Task<IActionResult> EditReferencefieldListById([FromBody] ParametersCatch Model)
        {
            int Opcode = 1;

            //var ReturnReferenceField = _Entity.EditReferenceFieldListbyid(Opcode, Convert.ToInt32(Model.ReferenceFieldid));


            return Ok();
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("UpdatedReferenceFieldList")]
        [Obsolete]

        public async Task<IActionResult> UpdatedReferenceFieldList([FromBody] ReferenceFieldListDTO Model)
        {
            int Opcode = 1;

            var ReturnReferenceField = _Entity.UpdateReferenceField( Model);


            return Ok(ReturnReferenceField);
        }



        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("DeleteReferencefieldListById")]

        public async Task<IActionResult> DeleteReferencefieldListById([FromBody] ParametersCatch Model)
        {
            try
            {

                var ReturnReferenceField = _Entity.DeleteReference(Convert.ToInt32(Model.ReferenceFieldid_int));
               if(Model.Entityid_Int!=null)
                {
                    var ReturnEntity = _Entity.EditEntitiesDependencyEntityId(1, Convert.ToInt32(Model.Entityid_Int));

                    return Ok(ReturnEntity);
                }
                else
                {
                    var ReturnFieldList = _Entity.GetFieldListDataorReferenceFielddata(3);
                    return Ok(ReturnFieldList);
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }



    }
}