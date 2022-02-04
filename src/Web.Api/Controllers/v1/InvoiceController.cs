using Core.Application.Contracts.Features.Invoice.Commands.Add;
using Core.Application.Contracts.Features.Invoice.Commands.Delete;
using Core.Application.Contracts.Features.Invoice.Commands.Update;
using Core.Application.Contracts.Features.Invoice.Query.Get;
using Core.Application.Contracts.Features.Invoice.Query.GetAll;
using Core.Application.Contracts.Features.Invoice.Query.GetAllForDatatable;
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
        public async Task<IActionResult> Create([FromForm] CreateInvoiceCommand addInvoiceCommand)
        {
            var response = await Mediator.Send(addInvoiceCommand);
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
        public async Task<IActionResult> Update([FromForm] UpdateInvoiceCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
        ///// <summary>
        ///// get all client
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IReadOnlyList<GetAllInvoiceQueryViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllInvoiceQuery query = new GetAllInvoiceQuery { };
            var clients = await Mediator.Send(query);
            return Ok(clients);
        }

        ///// <summary>
        ///// get all client for datatable
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllForDatatableInvoiceQueryViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllForDatatable(GetAllForDatatableInvoiceQuery query)
        {
            //GetAllForDatatableClientQuery query = new GetAllForDatatableClientQuery { };
            var response = await Mediator.Send(query);
            return Ok(response);
        }



        ///// <summary>
        ///// details of client
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetInvoiceQueryViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int id)
        {
            GetInvoiceQuery query = new GetInvoiceQuery { Id = id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        ///// <summary>
        ///// Delete a client
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(DeleteInvoiceCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}
