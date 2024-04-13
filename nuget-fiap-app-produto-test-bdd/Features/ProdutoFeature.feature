Funcionalidade: Gerenciamento de Produtos de Lanchonete
    Como um usuário da API
    Eu quero gerenciar produtos de uma lanchonete

Cenário: Obter todos os produtos
    Dado que eu adicionei um produto com o nome "Hambúrguer Especial" e preço "25.50"
    Quando eu solicito a lista de produtos
    Então eu devo receber uma lista contendo o produto "Hambúrguer Especial"

Cenário: Adicionar um novo produto
    Quando que eu adiciono um produto com o nome "Batata Frita Grande" e preço "15.00"
    Então o produto "Batata Frita Grande" deve ser adicionado com sucesso

Cenário: Obter produto por ID
    Dado que eu adicionei um produto com o nome "Milkshake de Chocolate" e preço "12.00"
    Quando eu solicito o produto pelo seu ID
    Então eu devo receber o produto "Milkshake de Chocolate"

Cenário: Atualizar um produto existente
    Dado que eu adicionei um novo produto com o nome "Café Pequeno"
    Quando eu atualizo o produto "Café Pequeno" para ter o nome "Café Grande" e preço "6.00"
    Quando eu solicito o produto pelo seu ID
    Então eu devo receber o produto com o nome "Café Grande"

Cenário: Excluir um produto
    Dado que eu adicionei o produto com o nome "Sundae de Morango"
    Quando eu excluo o produto "Sundae de Morango"
    Então o produto "Sundae de Morango" não deve mais existir