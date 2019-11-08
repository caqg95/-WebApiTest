using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiTest.Models.DATA;
namespace WebApiTest.Controllers
{
    [RoutePrefix("Api/Products")]
    public class ProductsController : ApiController
    {
        NORTHWINDEntities db = new NORTHWINDEntities();


        // GET: Products
        [HttpGet]
        [Route("LitadoProducto")]
        public IEnumerable<PRODUCTM> GetProducts()
        {
            try
            {
                List<PRODUCTM> productom = new List<PRODUCTM>();
                var p = db.PRODUCTS.ToList();
                foreach (var item in p)
                {
                    PRODUCTM itemp = new PRODUCTM
                    {
                        PRODUCTID = item.PRODUCTID,
                        PRODUCTNAME = item.PRODUCTNAME,
                        SUPPLIERID = item.SUPPLIERID,
                        SUPPLIERNAME = item.SUPPLIER.COMPANYNAME,
                        CATEGORYID = item.CATEGORYID,
                        CATEGORYNAME = item.CATEGORy.CATEGORYNAME,
                        QUANTITYPERUNITY = item.QUANTITYPERUNITY,
                        UNITPRICE = item.UNITPRICE,
                        UNITSINSTOCKS = item.UNITSINSTOCKS,
                        UNITSONORDER = item.UNITSONORDER,
                        REORDERLEVEL = item.REORDERLEVEL,
                        DISCONTINUED = item.DISCONTINUED
                    };
                    productom.Add(itemp);
                }
                return productom;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("LitadoProductoID/{ID}")]
        public IHttpActionResult GetProductsID(int ID)
        {
            PRODUCTM productom = new PRODUCTM();
            try
            {
                var P = db.PRODUCTS.Find(ID);
                if (P == null)
                {
                    return NotFound();
                }
                PRODUCTM itemp = new PRODUCTM
                {
                    PRODUCTID = P.PRODUCTID,
                    PRODUCTNAME = P.PRODUCTNAME,
                    SUPPLIERID = P.SUPPLIERID,
                    SUPPLIERNAME = P.SUPPLIER.COMPANYNAME,
                    CATEGORYID = P.CATEGORYID,
                    CATEGORYNAME = P.CATEGORy.CATEGORYNAME,
                    QUANTITYPERUNITY = P.QUANTITYPERUNITY,
                    UNITPRICE = P.UNITPRICE,
                    UNITSINSTOCKS = P.UNITSINSTOCKS,
                    UNITSONORDER = P.UNITSONORDER,
                    REORDERLEVEL = P.REORDERLEVEL,
                    DISCONTINUED = P.DISCONTINUED
                };
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(productom);
        }

        [HttpGet]
        [Route("InsertProducto")]
        public IHttpActionResult PostProducto(PRODUCT data)
        {
            string message = string.Empty;
            if (data != null)
            {
                try
                {
                    db.PRODUCTS.Add(data);
                    if (db.SaveChanges() > 0)
                    {
                        message = "Producto guardado con exito";
                    }
                    else
                    {
                        message = "faild";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                message = "faild";
            }
            return Ok(message);
        }

        [HttpPut]
        [Route("UpdateProducto")]
        public IHttpActionResult PutProducto(PRODUCT data)
        {
            string message = string.Empty;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PRODUCT productosave = new PRODUCT();
                productosave = db.PRODUCTS.Find(data.PRODUCTID);
                if (productosave != null)
                {
                    productosave.PRODUCTNAME = data.PRODUCTNAME;
                }

                if (db.SaveChanges() > 0)
                {
                    message = "Producto Actualizado";
                }
                else
                {
                    message = "faild";
                }
            }
            catch
            {
                throw;
            }
            return Ok(message);
        }

        [HttpDelete]
        [Route("DeleteProducto")]
        public IHttpActionResult DeleteProducto(PRODUCT data)
        {
            string message = string.Empty;
            var productodb = db.PRODUCTS.Find(data.PRODUCTID);
            if (productodb == null)
            {
                return NotFound();
            }

            db.PRODUCTS.Remove(productodb);
            if (db.SaveChanges() > 0)
            {
                message = "Producto Eliminado";
            }
            else
            {
                message = "faild";
            }

            return Ok(message);
        }


        [HttpGet]
        [Route("LitadoCategoria")]
        public IEnumerable<CATEGORyM> GetCategoria()
        {
            try
            {
                List<CATEGORyM> categorym = new List<CATEGORyM>();
                var p = db.CATEGORIES.ToList();
                foreach (var item in p)
                {
                    CATEGORyM itemp = new CATEGORyM
                    {
                        CATEGORYID = item.CATEGORYID,
                        CATEGORYNAME = item.CATEGORYNAME,
                        DESCRIPTION = item.DESCRIPTION,
                        PICTURE = item.PICTURE,
                    };
                    categorym.Add(itemp);
                }
                return categorym;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public partial class PRODUCTM
    {
        public int PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public int SUPPLIERID { get; set; }
        public string SUPPLIERNAME { get; set; }
        public int CATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
        public decimal QUANTITYPERUNITY { get; set; }
        public decimal UNITPRICE { get; set; }
        public decimal UNITSINSTOCKS { get; set; }
        public decimal UNITSONORDER { get; set; }
        public int REORDERLEVEL { get; set; }
        public bool DISCONTINUED { get; set; }
    }
    public partial class CATEGORyM
    {
        public int CATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public byte[] PICTURE { get; set; }
    }
}

