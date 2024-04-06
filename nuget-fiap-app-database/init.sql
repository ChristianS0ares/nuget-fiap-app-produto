-- Usar o banco de dados criado
\c postgres;

CREATE TABLE TBL_CATEGORIA (
    ID serial PRIMARY KEY,
    NOME VARCHAR(255)
);

CREATE TABLE TBL_PRODUTO (
    ID serial PRIMARY KEY,
    NOME VARCHAR(255),
    PRECO NUMERIC(10, 2),
    DESCRICAO TEXT,
    URL_IMAGEM VARCHAR(255),
    ID_CATEGORIA INT REFERENCES TBL_CATEGORIA(ID)
);


INSERT INTO TBL_CATEGORIA (NOME) VALUES
    ('Lanche'),
    ('Acompanhamento'),
    ('Bebida'),
    ('Sobremesa');
		
-- Inserir 30 registros fictícios na tabela TBL_PRODUTO com URLs fictícias de imagens
INSERT INTO TBL_PRODUTO (NOME, PRECO, DESCRICAO, URL_IMAGEM, ID_CATEGORIA) VALUES
    ('Hambúrguer', 9.99, 'Hambúrguer delicioso', 'https://example.com/imagem1.jpg', 1),
    ('Batata Frita', 4.99, 'Batata frita crocante', 'https://example.com/imagem2.jpg', 2),
    ('Coca-Cola', 2.99, 'Refrigerante refrescante', 'https://example.com/imagem3.jpg', 3),
    ('Sundae de Chocolate', 3.99, 'Sobremesa de chocolate', 'https://example.com/imagem4.jpg', 4),
    ('Salada Caesar', 7.99, 'Salada de frango caesar', 'https://example.com/imagem5.jpg', 2),
    ('Pizza de Pepperoni', 12.99, 'Pizza com pepperoni', 'https://example.com/imagem6.jpg', 1),
    ('Onion Rings', 5.99, 'Anéis de cebola fritos', 'https://example.com/imagem7.jpg', 2),
    ('Sprite', 2.99, 'Refrigerante de limão-limão', 'https://example.com/imagem8.jpg', 3),
    ('Cheesecake de Morango', 4.99, 'Cheesecake com morangos', 'https://example.com/imagem9.jpg', 4),
    ('Taco de Frango', 8.99, 'Taco de frango picante', 'https://example.com/imagem10.jpg', 1),
    ('Milkshake de Baunilha', 3.99, 'Milkshake de baunilha cremoso', 'https://example.com/imagem11.jpg', 3),
    ('Creme Brulee', 6.99, 'Sobremesa de creme queimado', 'https://example.com/imagem12.jpg', 4),
    ('Wrap de Alface', 7.99, 'Wrap de frango em folha de alface', 'https://example.com/imagem13.jpg', 2),
    ('Água Mineral', 1.99, 'Água mineral pura', 'https://example.com/imagem14.jpg', 3),
    ('Torta de Maçã', 5.99, 'Torta de maçã caseira', 'https://example.com/imagem15.jpg', 4),
    ('Muffin de Blueberry', 3.49, 'Muffin com blueberries', 'https://example.com/imagem16.jpg', 4),
    ('Cachorro-Quente', 6.99, 'Cachorro-quente clássico', 'https://example.com/imagem17.jpg', 1),
    ('Fritas de Queijo', 5.49, 'Fritas de queijo derretido', 'https://example.com/imagem18.jpg', 2),
    ('Pepsi', 2.99, 'Refrigerante de cola', 'https://example.com/imagem19.jpg', 3),
    ('Bolo de Chocolate', 4.99, 'Bolo de chocolate decadente', 'https://example.com/imagem20.jpg', 4),
    ('Sushi de Salmão', 11.99, 'Sushi de salmão fresco', 'https://example.com/imagem21.jpg', 1),
    ('Sorvete de Baunilha', 4.49, 'Sorvete de baunilha cremoso', 'https://example.com/imagem22.jpg', 4),
    ('Sanduíche de Frango', 7.49, 'Sanduíche de frango grelhado', 'https://example.com/imagem23.jpg', 1),
    ('Tiramisu', 6.99, 'Sobremesa de café italiana', 'https://example.com/imagem24.jpg', 4),
    ('Nuggets de Frango', 4.99, 'Nuggets de frango crocantes', 'https://example.com/imagem25.jpg', 2),
    ('Fanta', 2.99, 'Refrigerante de laranja', 'https://example.com/imagem26.jpg', 3),
    ('Bolo de Morango', 4.99, 'Bolo de morango fresco', 'https://example.com/imagem27.jpg', 4),
    ('Tacos de Peixe', 9.99, 'Tacos de peixe com molho de pimenta', 'https://example.com/imagem28.jpg', 1),
    ('Sorvete de Chocolate', 4.49, 'Sorvete de chocolate indulgente', 'https://example.com/imagem29.jpg', 4),
    ('Sanduíche Vegetariano', 7.49, 'Sanduíche vegetariano saudável', 'https://example.com/imagem30.jpg', 1);