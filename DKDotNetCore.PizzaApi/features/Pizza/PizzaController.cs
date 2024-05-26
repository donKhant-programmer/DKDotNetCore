using DKDotNetCore.PizzaApi.Db;
using DKDotNetCore.PizzaApi.Queries;
using DKDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DKDotNetCore.PizzaApi.features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DappperService _dappperService;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dappperService = new DappperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtrasAsync()
        {
            var lst = await _appDbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest) 
        {
            var ItemPizza = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var totalPrice = ItemPizza.Price;

            if (orderRequest.Extras.Length > 0)
            {
                // select * from Table_PizzaExtra where PizzaExtraId in (1,3,4)
                var lstExtra = await _appDbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                totalPrice += lstExtra.Sum(x => x.Price);
            }

            string invoiceNo = DateTime.Now.ToString("yyMMddHHmmss");

            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = totalPrice,
            };

            List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.Extras.Select(x => new PizzaOrderDetailModel()
            {
                PizzaExtraId = x,
                PizzaOrderInvoiceNo = invoiceNo,
            }).ToList();

            await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _appDbContext.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thanks for your order! Enjoy your pizza!",
                TotalAmount = totalPrice,
            };

            return Ok(response);
        }

        //[HttpGet("Order/{invoiceNo}")]
        //public async Task<IActionResult> GetOrder(string invoiceNo)
        //{
        //    var item = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
        //    var lst = await _appDbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();

        //    return Ok(new
        //    {
        //        Order = item,
        //        OrderDetail = lst,
        //    });
        //}

        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var item = _dappperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>(
                PizzaQuery.PizzaOrderQuery,
                new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var lst = _dappperService.Query<PizzaOrderInvoiceDetailModel>(
                PizzaQuery.PizzaOrderDetailQuery,
                new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var response = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst
            };

            return Ok(response);
        }
    }
}
