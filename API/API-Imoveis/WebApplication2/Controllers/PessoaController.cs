using APICondominio.DAO;
using APICondominio.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICondominio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        // GET: api/<PessoaController>
        [HttpGet]
        public ActionResult Get()
        {
            var pessoaTeste = new PessoaModel()
            {
                Email = "mariadasilva@gmail.com",
                Genero = Genero.Feminino,
                Idade = 57,
                Nome = "Maria Da Silva Sauro",
                Telefone = "(47 9999-4434)",
            };

            //Criar(pessoaTeste);

            return Ok(pessoaTeste);
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
        public ActionResult Post(PessoaModel model)
        {
            if (model != null)
            {
                try
                {
                    if (model.ID > 0)
                        Atualizar(model);
                    else
                        model = Criar(model);

                   return  Ok(model);
                }
                catch (Exception ex)
                {
                    StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }

        private void Atualizar(PessoaModel model)
        {
            new PessoaDAO().Atualizar(model);
        }

        private PessoaModel Criar(PessoaModel model)
        {
            int id = new PessoaDAO().Criar(model);
            model.ID = id;
            return model;
        }

        private PessoaModel BuscarPeloId(int id)
        {
            return new PessoaDAO().BuscarPeloId(id);
        }

        private List<PessoaModel> BuscarTodos()
        {
            return new PessoaDAO().BuscarTodos();
        }

        private void DeletarPeloId(int id)
        {
            new PessoaDAO().Deletar(id);
        }

    }
}
