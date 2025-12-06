using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }
        [HttpPost]
        [Route("list")]
        public List<State> List()
        {
            List<State> states = new List<State>();

            try
            {
                
                states = _stateService.GetAllState();
            }
            catch (Exception ex)
            {
            }
            return states.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public State Select(int id)
        {
            State objState = new State();
            try
            {
                
                objState = _stateService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objState;
        }

        [HttpPost]
        [Route("save")]
        public StateResponse SaveData(State stateData)
        {
            StateResponse res = new StateResponse();

            try
            {
                
                Int32 ID = _stateService.SaveData(stateData);

                res.flag = "1";
                res.message = "Success";
                res.data = _stateService.GetItems(ID);

            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public StateResponse Delete(int id)
        {
            StateResponse res = new StateResponse();

            try
            {
                

                _stateService.DeleteState(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _stateService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
    }
}
