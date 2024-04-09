# language: pt

Funcionalidade: Gerenciamento de Categorias
    Como um usu�rio da API
    Eu quero gerenciar categorias

Cen�rio: Obter todas as categorias
    Dado que eu adicionei uma categoria com o nome "Comida"
    Quando eu solicito a lista de categorias
    Ent�o eu devo receber uma lista contendo "Comida"

Cen�rio: Adicionar uma nova categoria
    Quando eu adiciono uma categoria com o nome "Doces"
    Ent�o a categoria "Doces" deve ser adicionada com sucesso

Cen�rio: Obter categoria por ID
    Dado que eu adicionei uma categoria com o nome "Molhos"
    Quando eu solicito a categoria pelo seu ID
    Ent�o eu devo receber a categoria "Molhos"

Cen�rio: Atualizar uma categoria existente
    Dado que eu adicionei uma categoria com o nome "Oriental"
    E eu atualizo a categoria "Oriental" para ter o nome "Comida Japonesa"
    Quando eu solicito a categoria pelo seu ID
    Ent�o eu devo receber a categoria com o nome "Comida Japonesa"

Cen�rio: Excluir uma categoria
    Dado que eu adicionei uma categoria com o nome "Comida"
    Quando eu excluo a categoria "Comida"
    Ent�o a categoria "Comida" n�o deve mais existir