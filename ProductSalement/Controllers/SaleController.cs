using Microsoft.AspNetCore.Mvc;
using ProductSalement.Models;
using ProductSalement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductSalement.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private IRepository<Sale> _iSales;
        private IRepository<Buyer> _iBuyer;
        private IRepository<SalesPoint> _iSalesPoint;
        private IRepository<Product> _iProduct;

        public SaleController(IRepository<Sale> iSales, IRepository<Buyer> iBuyer, IRepository<SalesPoint> iSalesPoint, IRepository<Product> iProductPoint)
        {
            _iSales = iSales;
            _iBuyer = iBuyer;
            _iSalesPoint = iSalesPoint;
            _iProduct = iProductPoint;
        }

        /// <summary>
        /// Осуществление продажи
        /// </summary>
        /// <param name="salesPointId">Номер точки продажи</param>
        /// <param name="buyerId">Номер покупателя</param>
        /// <param name="sales">Список продаж</param>
        [HttpPost]
        public ActionResult Sale(int salesPointId, int? buyerId, List<SalesData> sales)
        {
            var salesPoint = _iSalesPoint.Get(salesPointId);
            if (salesPoint == null)
            {
                return NotFound("Точка продажи товара не найдена");
            }
            int totalAmount = 0;
            ProvidedProduct providedProduct = null;
            Sale sale = null;

            foreach (SalesData saleData in sales)
            {
                if (saleData.ProductQuantity <= 0)
                {
                    return UnprocessableEntity("Количество покупаемого товара не может быть меньше или равно нулю");
                }
                providedProduct = salesPoint.ProvidedProducts.FirstOrDefault(x => x.ProductId == saleData.ProductId);
                if (providedProduct != null)
                {
                    if (providedProduct.ProductQuantity < saleData.ProductQuantity)
                    {
                        return UnprocessableEntity($"На точке продажи нету такого количество товара: {providedProduct.Product.Name}, осталось: {providedProduct.ProductQuantity}");
                    }
                    providedProduct.ProductQuantity -= saleData.ProductQuantity;
                    saleData.ProductAmount = providedProduct.Product.Price * saleData.ProductQuantity;
                    totalAmount += saleData.ProductAmount;
                    _iSalesPoint.Update(salesPoint);
                    sale = CreateSale(salesPointId, buyerId, sales, totalAmount);
                    if (buyerId != null)
                    {
                        var buyer = _iBuyer.Get((int)buyerId);
                        if (buyer != null)
                        {
                            buyer.Sales.Add(sale);
                            _iBuyer.Update(buyer);
                            return Ok(sale);
                        }
                    }
                }
                else
                {
                    return NotFound("Один из продуктов в списке покупок отсутствуют на точке продажи, попробуйте еще раз.");
                }

            }
            return Ok(sale);
        }


        /// <summary>
        /// Тестовый метод для заполнения базы данных тестовыми данными.
        /// Выполните один раз
        /// </summary>
        [HttpGet]
        [Route("SeedWithTestData")]
        public ActionResult SeedWithTestData()
        {
            //Продукты
            var product = new Product("Крем для рук", 100);
            var product2 = new Product("Шампунь", 60);
            var product3 = new Product("Дезодорант", 10);
            _iProduct.Create(product);
            _iProduct.Create(product2);
            _iProduct.Create(product3);
            //Покупатели
            var buyer = new Buyer("Владимир");
            var buyer2 = new Buyer("Николай");
            var buyer3 = new Buyer("Артем");
            _iBuyer.Create(buyer);
            _iBuyer.Create(buyer2);
            _iBuyer.Create(buyer3);
            //Поставки продуктов
            List<ProvidedProduct> providedProducts = new List<ProvidedProduct>()
            {
                new ProvidedProduct(1, 50),
                new ProvidedProduct(2, 45),
                new ProvidedProduct(3, 23)
            };
            //Точки продаж
            var salesPoint = new SalesPoint("Магнит косметик", providedProducts);
            _iSalesPoint.Create(salesPoint);
            return Ok(salesPoint);
        }

        private Sale CreateSale(int salesPointId, int? buyerId, List<SalesData> sales, int totalAmount)
        {
            Sale sale = new Sale(DateTime.Now, DateTime.Now.ToLongTimeString(), salesPointId, buyerId, sales, totalAmount);
            _iSales.Create(sale);
            return sale;
        }
    }
}
