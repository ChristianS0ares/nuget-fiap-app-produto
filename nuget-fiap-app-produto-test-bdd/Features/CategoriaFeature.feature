@bdd
Funcionalidade: Gerenciamento de Categorias
    Como um usuário da API
    Eu quero gerenciar categorias

@bdd
Cenário: Obter todas as categorias
    Dado que eu adicionei uma categoria com o nome "Comida"
    Quando eu solicito a lista de categorias
    Então eu devo receber uma lista contendo "Comida"

@bdd
Cenário: Adicionar uma nova categoria
    Quando eu adiciono uma categoria com o nome "Doces"
    Então a categoria "Doces" deve ser adicionada com sucesso

@bdd
Cenário: Obter categoria por ID
    Dado que eu adicionei uma categoria com o nome "Molhos"
    Quando eu solicito a categoria pelo seu ID
    Então eu devo receber a categoria "Molhos"

@bdd
Cenário: Atualizar uma categoria existente
    Dado que eu adicionei uma categoria com o nome "Oriental"
    Quando eu atualizo a categoria "Oriental" para ter o nome "Comida Japonesa"
    Quando eu solicito a categoria pelo seu ID
    Então eu devo receber a categoria com o nome "Comida Japonesa"

@bdd
Cenário: Excluir uma categoria
    Dado que eu adicionei uma categoria com o nome "Comida"
    Quando eu excluo a categoria "Comida"
    Então a categoria "Comida" não deve mais existir