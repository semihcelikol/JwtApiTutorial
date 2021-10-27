using JwtApiTutorial.Library;
using JwtApiTutorial.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _config { get; set; }

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Get([FromBody] TokenRequest request)
        {
            TokenResponse ret = new TokenResponse();
            
            UserRepository userRepository = new UserRepository(_config);

            try
            {
                User user = userRepository.Get(request.Username, request.Password);

                if (user == null)
                {
                    ret.Success = false;
                    ret.Message = "Kullanıcı adı ya da şifre geçerli değildir.";

                    return BadRequest(ret);
                }

                ret = TokenGenerator.GenerateToken(user,
                                                   _config["Application:Secret"],
                                                   _config["Application:Audience"],
                                                   _config["Application:Issuer"]);

                if (String.IsNullOrEmpty(ret.Value))
                {
                    ret.Success = false;
                    ret.Message = "Token üretilemedi";

                    return BadRequest(ret);
                }
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.Message = ex.ToString();
            }

            return Ok(ret);
        }
    }
}
