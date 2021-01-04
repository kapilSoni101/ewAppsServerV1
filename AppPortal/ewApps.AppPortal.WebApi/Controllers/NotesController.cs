using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController:ControllerBase {

        INotesDS _notesDS;

        public NotesController(INotesDS notesDS) {
            _notesDS = notesDS;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ResponseModelDTO> AddNotes([FromBody]NotesAddDTO notesAddDTO) {
           Notes notes= await _notesDS.AddNotesAsync(notesAddDTO);
            return new ResponseModelDTO {
                Id = notes.ID,
                IsSuccess = true,
                Message="Notes added succesfully"
            };
        }

    }
}