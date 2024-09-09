using Microsoft.AspNetCore.Mvc;
using SysSeguridadG05.EN;
using SysSeguridadG05.BL;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SysSeguridadG05.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL usuarioBl = new UsuarioBL();
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<Usuario>>  Get()
        {
            try
            {
                Usuario  usuario = new Usuario();
                return await usuarioBl.ObtenerTodosAsync();
            }catch (Exception ex)
            {
            
                return null;
            }
                
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<Usuario> Get(int id)
        {
            try
            {
                return await usuarioBl.ObtenerPorIdAsync(new Usuario {Id = id});
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var  strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                usuarioBl.CrearAsync(usuario);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                if(usuario.Id == id)
                {
                    await usuarioBl.ModificarAsync(usuario);
                    return Ok();
                }
                else{
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await usuarioBl.DeleteAsync(new Usuario {Id = id});
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Usuario>> Buscar([FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var  strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                var usuarios = await usuarioBl.BuscarIncluirRolAsync(usuario);
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task<Usuario> Login([FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var  strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                var usuario_Auth = await usuarioBl.LoginAsync(usuario);
                if(usuario_Auth != null && usuario_Auth.Id > 0 && usuario_Auth.Login == usuario.Login)
                {
                    return usuario_Auth;
                }
                else
                return new Usuario();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("CambiarPassword")]
        public async Task<ActionResult> CambiarPassword([FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var  strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                var usuario_Auth = await usuarioBl.CambiarPasswordAsync(usuario, usuario.ConfirmPassword_aux);
                return Ok();
            }
            catch (Exception)
            {
                
                throw ;
            }
        }
    }
}