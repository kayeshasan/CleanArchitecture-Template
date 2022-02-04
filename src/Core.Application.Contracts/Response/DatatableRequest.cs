
//using Core.Application.Contracts.Features.ServiceProvider.Queries.GetAllForDatatable;
using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Response
{
    public class Dt_Search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class Dt_Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Dt_Search search { get; set; }
    }

    public class Dt_Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class Dt_Query
    {
        public int draw { get; set; }
        //public List<Dt_Column> columns { get; set; }
        //public List<Dt_Order> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Dt_Search search { get; set; }
    }

    public class Dt_Root
    {
        public Dt_Query query { get; set; }     
    }
}
