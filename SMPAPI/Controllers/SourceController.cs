using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMP.BAL.DTO;
using SMPAPI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;
using SMPAPI.Settings;
using Microsoft.Extensions.Options;
using SMP.BAL.ILogic;
using SMP.Data.ModelForTables;
using SMPAPI.Model;

namespace SMPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _environment;
        private readonly FileUploading _FileUploading;

        private readonly Isource _source;

        private readonly Ientity _entity;



        [Obsolete]
        public SourceController(
            IHostingEnvironment environment,
            IOptions<FileUploading> FileUploading,
            Isource source,
            Ientity entity
            )
        {
            _environment = environment;
            _FileUploading = FileUploading.Value;
            _source = source;
            _entity = entity;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("SourceSave")]
        [Obsolete]
        public async Task<IActionResult> SourceSave([FromForm]SourceDTO model)
        {
            try
            {
                var filename = "";
                var filePathForfile = "";

                if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
                {
                    _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }

                var uploads = Path.Combine(_environment.WebRootPath, _FileUploading.SourceUploadFilepath);

                filePathForfile = uploads;
                if (model.fileSource.Length > 0)
                {
                    filename = model.fileSource.FileName;
                    var filePath = Path.Combine(uploads, model.fileSource.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.fileSource.CopyToAsync(fileStream);
                    }
                }



                return Ok();
            }

            catch(Exception ex)
            {
                throw;
            }


                return null;
        }


        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetlistforgearTypeanddatatype")]
        public IActionResult GetlistforgearTypeanddatatype()
        {

            var Getall = _source.Get_Geartype_source_datatypelist_for_dropdwn();


            var ReturnFieldList = _entity.GetFieldListDataorReferenceFielddata(3);

            return Ok(new
            {
                GetallGearsourcedatatype= Getall,
                Fieldlist= ReturnFieldList
            }
            );

        }




        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("NewkeyfieldInsert")]
        [Obsolete]
        public async Task<IActionResult> NewkeyfieldInsert([FromBody]KeyFieldDTO model)
        {

         var Returnid= _source.InsertKeyfield(model);
         var list= _source.ReturnKeyfield();
            return Ok(Returnid);
        }


        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetonlyKeyfieldvalue")]
        [Obsolete]
        public async Task<IActionResult> GetonlyKeyfieldvalue()
        {


            var list = _source.ReturnKeyfield();
            return Ok(list);
        }



        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetKeyFieldList")]
        [Obsolete]
        public async Task<IActionResult> GetKeyFieldList()
        {
            var listKeyField = _source.ReturnKeyfield();

            var Entitylist = 0;
            var Getall = _source.Get_Geartype_source_datatypelist_for_dropdwn();

            return Ok(new
            {
                KeyFieldlist= listKeyField,
                Getsource_gear_datatype= Getall,
                entitylist= Entitylist

            });

        }

        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GetonlyKeyfieldById")]
        [Obsolete]
        public async Task<IActionResult> GetonlyKeyfieldById(ParametersCatch Modal)
        {
            // var listKeyField = _source.ReturnKeyfieldByid(Convert.ToInt32(Modal.KeyFieldId));

            var listKeyField = 0;
            return Ok(new
            {
                 listKeyField

            });

        }


    }

    }