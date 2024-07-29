using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyHousingSolutionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesAPIController : ControllerBase
    {
        IPropertyRepository propertyRepository;
        // private readonly IMapper _mapper;
        public PropertiesAPIController(IPropertyRepository prodrepo)
        {
            propertyRepository = prodrepo;
        }


        // GET: api/<BuyerAPIController>
        [HttpGet]
        public ActionResult<IEnumerable<Property>> Get()
        {
            var items = propertyRepository.GetAllProperty();
            return Ok(items);
        }


        // GET api/<BuyerAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Property> Get(int id)
        {
            var item = propertyRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("BuyOption")]
        public ActionResult<IEnumerable<Property>> GetPropertyBuyOption(string BuyOption)
        {
            var qResult = propertyRepository.GetPropertyBuyOption(BuyOption);
            if (qResult == null)
            {
                return NotFound();
            }
            return Ok(qResult);

        }

        [HttpGet("SellerId")]
        public ActionResult<IEnumerable<Property>> GetPropertySellerId(string name)
        {
            var qResult = propertyRepository.GetPropertySellerId(name);
            if (qResult == null)
            {
                return NotFound();
            }
            return Ok(qResult);

        }
        [HttpGet("SellerId/IsActive")]
        public ActionResult<IEnumerable<Property>> GetPropertySellerIdIsActive(int SellerId, bool IsActive)
        {
            var qResult = propertyRepository.GetPropertySellerIdIsActive(SellerId, IsActive);
            if (qResult == null)
            {
                return NotFound();
            }
            return Ok(qResult);

        }

        //----------------------------------------------------------
        [HttpGet("IsActive")]
        public ActionResult<IEnumerable<Property>> GetPropertyIsActive(bool IsActive)
        {
            var qResult = propertyRepository.GetPropertyIsActive(IsActive);
            if (qResult == null)
            {
                return NotFound();
            }
            return Ok(qResult);
        }


        [HttpGet("PriceHighToLow")]
        public ActionResult<IEnumerable<Property>> GetPropertyByPriceHighToLow()
        {
            var qResult = propertyRepository.GetPropertyByPriceHighToLow();
            if (qResult == null)
            {
                return NotFound();
            }
            return Ok(qResult);

        }

        //----------------------

        [HttpGet("PriceLowToHigh")]
        public ActionResult<IEnumerable<Property>> GetPropertyByPriceLowToHigh()
        {
            var qResult = propertyRepository.GetPropertyByPriceLowToHigh();
            if (qResult == null)
            {
                return NotFound();
            }
            return Ok(qResult);

        }


        //	[Route("api/ActiveStatus")]
        [HttpPut("ActiveStatus")]
        public ActionResult changeactivestatus(int id)
        {
            propertyRepository.UpdatePropertyIsActive(id);
            return Ok();
        }

        [HttpPut]
        [Route("admins/VerifyProperty")]
        public ActionResult VerifyProperty(int id)
        {
            var flag = propertyRepository.VerifyProperty(id);
            if (flag)
            {
                return Ok();
            }
            return BadRequest();
        }


        // POST api/<BuyerAPIController>
        [HttpPost]
        public ActionResult Post([FromBody] Property property)
        {
            propertyRepository.Add(property);
            return Ok(property);
        }

        // PUT api/<BuyerAPIController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Property value)
        {
            propertyRepository.Update(id, value);
            return Ok();
        }

        // DELETE api/<BuyerAPIController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            propertyRepository.Delete(id);
            return Ok();
        }
    }
}