Funcionalidade: Gerenciamento de Produtos de Lanchonete
    Como um usu�rio da API
    Eu quero gerenciar produtos de uma lanchonete

Cen�rio: Obter todos os produtos
    Dado que eu adicionei um produto com o nome "Hamb�rguer Especial" e pre�o "25.50"
    Quando eu solicito a lista de produtos
    Ent�o eu devo receber uma lista contendo o produto "Hamb�rguer Especial"

Cen�rio: Adicionar um novo produto
    Quando que eu adiciono um produto com o nome "Batata Frita Grande" e pre�o "15.00"
    Ent�o o produto "Batata Frita Grande" deve ser adicionado com sucesso

Cen�rio: Obter produto por ID
    Dado que eu adicionei um produto com o nome "Milkshake de Chocolate" e pre�o "12.00"
    Quando eu solicito o produto pelo seu ID
    Ent�o eu devo receber o produto "Milkshake de Chocolate"

Cen�rio: Atualizar um produto existente
    Dado que eu adicionei um novo produto com o nome "Caf� Pequeno"
    Quando eu atualizo o produto "Caf� Pequeno" para ter o nome "Caf� Grande" e pre�o "6.00"
    Quando eu solicito o produto pelo seu ID
    Ent�o eu devo receber o produto com o nome "Caf� Grande"

Cen�rio: Excluir um produto
    Dado que eu adicionei o produto com o nome "Sundae de Morango"
    Quando eu excluo o produto "Sundae de Morango"
    Ent�o o produto "Sundae de Morango" n�o deve mais existir