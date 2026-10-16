using ApiFornecedores.Entities;
using ApiFornecedores.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFornecedores.Controllers
{
    [Route("api/v1/fornecedores")]
    [ApiController]
    public class FornecedorController (FornecedorRepository fornecedorRepository)
        : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, object> request)
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = request["nome"].ToString();
            fornecedor.Cnpj = request["cnpj"].ToString();
            fornecedor.Email = request["email"].ToString();
            fornecedor.Telefone = request["telefone"].ToString();

            fornecedorRepository.Cadastrar(fornecedor);

            return Ok(new
            {
                mensagem = "Fornecedor cadastrado com sucesso!",
                dados = fornecedor
            });
        }        
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Dictionary<string, object> request)
        {
            var fornecedor = fornecedorRepository.ObterFornecedor(id);
            if (fornecedor == null)
            {
                return NotFound(
                    new { mensagem = "Fornecedor não encontrado para edição." });
            }

            fornecedor.Nome = request["nome"].ToString();
            fornecedor.Cnpj = request["cnpj"].ToString();
            fornecedor.Email = request["email"].ToString();
            fornecedor.Telefone = request["telefone"].ToString();

            fornecedorRepository.Editar(fornecedor);

            return Ok(new
            {
                mensagem = "Fornecedor atualizado com sucesso!",
                dados = fornecedor
            });

        }        
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var fornecedor = fornecedorRepository.ObterFornecedor(id);
            if (fornecedor == null)
            {
                return NotFound(
                    new { mensagem = "Fornecedor não encontrado para exclusão" });
            }

            fornecedorRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Fornecedor excluído com sucesso!"
            });
        }        
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var fornecedores = fornecedorRepository.ListarTodos();
            return Ok(fornecedores);
        }        
        
        [HttpGet("{id}")]
        public IActionResult GetOne(Guid id)
        {
            var fornecedor = fornecedorRepository.ObterFornecedor(id);

            return Ok(fornecedor);
        }


    }
}
