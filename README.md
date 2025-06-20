# Aplicativo em Microsserviços para Controle de Empréstimo de Livros

Este projeto é uma API desenvolvida para controle de empréstimo de livros, em que será possível cadastrar livros, leitores e empréstimos. O sistema será estruturado em três microsserviços distintos, cada um responsável por uma área funcional específica.

* Microsserviço 1: Livros
* Microsserviço 2: Leitores
* Microsserviço 3: Empréstimos


## Lógica de Negócio Principal
Essa arquitetura em microsserviços permite que cada parte do sistema seja desenvolvida, implantada e escalada de forma independente, facilitando a manutenção e evolução do software.

* Registro de Empréstimo: Ao registrar um empréstimo, o microsserviço de Empréstimos interage com o microsserviço de Livros para marcar o livro como "emprestado".
* Validação de Devolução: Antes de permitir um novo empréstimo, o microsserviço de Empréstimos verifica no microsserviço de Livros se o livro já foi devolvido.
* Consulta de Livros Emprestados: O microsserviço de Empréstimos fornece uma funcionalidade para listar todos os livros atualmente emprestados a um determinado leitor.




## Microsserviço de Empréstimo

**Funcionalidade:** Gerenciar os empréstimos de livros.

**Ações:**

* Registrar um novo empréstimo, marcando o livro como emprestado.
* Validar se um livro já foi devolvido.
* Listar os livros emprestados para um determinado leitor.


## Rotas da API 


__1. POST /api/emprestimo/criarEmprestimo__

Cria um novo empréstimo no sistema.

__2. PATCH /api/emprestimo/{id}/devolucao__

Faz a devolução do livro.

__3. GET /api/emprestimo/leitor/{id}__

Busca os empréstimos feitos por um leitor, indicado pelo id do leitor.

__4. GET /api/emprestimo/id__

Busca os emprestimos pelo id.

__5. GET /api/emprestimo/leitor/{idLeitor}/emprestimo-com-livros__

Busca todos os emprestimos já feitos pelo leitor.


