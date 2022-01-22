using APICondominio.DAO;
using APICondominio.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APICondominio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImovelController : Controller
    {
        // GET: api/<PessoaController>
        [HttpGet]
        public ActionResult Get()
        {
            var imovelTeste = new ImovelModel()
            {
                Endereco = "Avenida Brasil",
                NomeCondominio = "Sol",
                Numero = "123",
                Proprietario = "Raimundo da Silva"
            };

            Criar(imovelTeste);

            return Ok(imovelTeste);
        }

        // GET api/<PessoaController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var model = BuscarPeloId(id);

                if (model != null)
                    return Ok(model);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("BuscarTodos")]
        public ActionResult GetTodos()
        {
            try
            {
                return Ok(BuscarTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Deletar/{id}")]
        public ActionResult Deletar(int id)
        {
            try
            {
                DeletarPeloId(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<PessoaController>
        [HttpPost]
        [Route("Gravar")]
        [EnableCors("MyPolicy")]
        public ActionResult Post(ImovelModel model)
        {
            if (model != null)
            {
                try
                {
                    if (model.ID > 0)
                        Atualizar(model);
                    else
                        model = Criar(model);

                   return Ok(model);
                }
                catch (Exception ex)
                {
                    StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }

        private void Atualizar(ImovelModel model)
        {
            new ImovelDAO().Atualizar(model);
        }

        private ImovelModel Criar(ImovelModel model)
        {
            int id = new ImovelDAO().Criar(model);
            model.ID = id;
            return model;
        }

        private ImovelModel BuscarPeloId(int id)
        {
            return new ImovelDAO().BuscarPeloId(id);
        }

        private List<ImovelModel> BuscarTodos()
        {
            return new ImovelDAO().BuscarTodos();
        }

        private void DeletarPeloId(int id)
        {
            new ImovelDAO().Deletar(id);
        }

    }
}
