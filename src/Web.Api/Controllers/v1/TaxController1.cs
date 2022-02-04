using Core.Application.Contracts.Features.Client.Commands.Add;
using Core.Application.Contracts.Features.Client.Commands.Delete;
using Core.Application.Contracts.Features.Client.Commands.Register;
using Core.Application.Contracts.Features.Client.Commands.Update;
using Core.Application.Contracts.Features.Client.Queries.Get;
using Core.Application.Contracts.Features.Client.Queries.GetAllClients;
using Core.Application.Contracts.Features.Client.Queries.GetAllForDatatable;
using Core.Application.Contracts.Features.Service.Queries.GetAllForDatatable;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Web.Api.Controllers.v1
{
   /// <summary>
   /// controller for client
   /// </summary>
    [ApiVersion("1.0")]
    public class InvoiceController : BaseApiController
    {
       /// <summary>
       /// constructor of clientController
       /// </summary>
        public InvoiceController()
        {

        }

        /// <summary>
        /// create new client
        /// </summary>
        /// <param name="addClientCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateClientCommand addClientCommand)
        {
            var response = await Mediator.Send(addClientCommand);
            return Ok(response);
        }

        /// <summary>
        /// update a client
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateClientCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
        /// <summary>
        /// get all client
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IReadOnlyList<GetAllClientsQueryViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllClientsQuery query = new GetAllClientsQuery { };
            var clients = await Mediator.Send(query);
            return Ok(clients);
        }

        /// <summary>
        /// get all client for datatable
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllForDatatableClientQueryViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllForDatatable(GetAllForDatatableClientQuery query)
        {
            //GetAllForDatatableClientQuery query = new GetAllForDatatableClientQuery { };
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Registration for client
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register(RegisterClientCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }


        /// <summary>
        /// details of client
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetClientQueryViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int id)
        {
            GetClientQuery query = new GetClientQuery { Id = id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        /// <summary>
        /// Delete a client
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(DeleteClientCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}
