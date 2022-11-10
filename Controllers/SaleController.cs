using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_API.Context;
using Payment_API.Enums;
using Payment_API.Models;
using Payment_API.Validations;

namespace Payment_API.Controllers
{
    [ApiController]
    [Route("api-payment/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ContextOrganizer _context;

        public SaleController(ContextOrganizer context)
        {
            _context = context;
        }

        // Encomendar a venda
        [HttpPost("AddingOrderSale")]
        public IActionResult AddingSale(OrderSale orderSale)
        {
            int countItems = 0;

            foreach (var item in orderSale.Products)
            {
                countItems += 1;
            }

            if (countItems < 1)
            {
                return BadRequest("Solicitação do pedido realizada com sucesso!");
            }

            orderSale.Status = StatusTransitions.Awaiting_Payment;
            orderSale.CurrentDate = DateTime.Now;

            _context.Sellers.Add(orderSale.Seller);

            decimal Amount = 0;

            foreach (var item in orderSale.Products)
            {
                if (item.Price <= 0 || item.Inventory <= 0)
                {
                    return BadRequest("Preço e estoque não podem ser iguais a zero ou menores que zero!");
                }

                Amount += item.Price * item.Inventory;
                _context.Products.Add(item);
            }

            orderSale.Amount = Amount;
            _context.OrderSales.Add(orderSale);
            _context.SaveChanges();

            return Ok(orderSale);
        }

        // Status de atualização
        [HttpPut("UpdateStatusTransitions")]
        public IActionResult UpdateStatusTransitions(int idOrderSale, StatusTransitions newStatus)
        {
            var orderSaleDatabase = _context.OrderSales.FirstOrDefault(os => os.Id == idOrderSale);

            if (orderSaleDatabase == null)
            {
                return NotFound("Não foi possível encontrar o pedido solicitado!");
            }

            var statusValidation = Validations.ValidationStatus.ValidationStatusCurrent(orderSaleDatabase.Status, newStatus);

            if (statusValidation)
            {
                orderSaleDatabase.Status = newStatus;
                _context.SaveChanges();

                return Ok("O status do pedido foi alterado com sucesso!");
            }
            else
            {
                return BadRequest("O status informado é inválido!");
            }
        }

        // Procurar o produto
        [HttpGet("GetOrderSaleId/{id}")]
        public IActionResult GetOrderSaleId(int id)
        {
            var orderSaleDatabase = _context.OrderSales.FirstOrDefault(os => os.Id == id);

            if (orderSaleDatabase == null)
            {
                return NotFound("Não foi possível encontrar o produto!");
            }

            orderSaleDatabase.Products = _context.Products.Where(os => os.SellerId == id).ToList();
            orderSaleDatabase.Seller = _context.Sellers.FirstOrDefault(os => os.Id == orderSaleDatabase.SellerId);

            return Ok(orderSaleDatabase);
        }
    }
}