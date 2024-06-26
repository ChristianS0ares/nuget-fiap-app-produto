﻿using nuget_fiap_app_produto_common.Models;
using nuget_fiap_app_produto_repository.DB;
using Dapper;
using nuget_fiap_app_produto_common.Interfaces.Repository;

namespace nuget_fiap_app_produto_repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private RepositoryDB _session;

        public CategoriaRepository(RepositoryDB session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            string sql = "SELECT id, nome FROM public.tbl_categoria";

            var categorias = await _session.Connection.QueryAsync<Categoria>(sql);
            return categorias;
        }

        public async Task<Categoria> GetById(int id)
        {
            string sql = "SELECT id, nome FROM public.tbl_categoria WHERE id = @id";

            var categoria = await _session.Connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { id });
            return categoria;
        }

        public async Task<int> Create(Categoria categoria)
        {
            string sql = "INSERT INTO public.tbl_categoria (nome) VALUES (@nome) RETURNING id";

            int categoriaId = await _session.Connection.ExecuteScalarAsync<int>(sql, categoria);
            return categoriaId;
        }

        public async Task<bool> Update(Categoria categoria)
        {
            string sql = "UPDATE public.tbl_categoria SET nome = @nome WHERE id = @id";

            int rowsAffected = await _session.Connection.ExecuteAsync(sql, categoria);
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "DELETE FROM public.tbl_categoria WHERE id = @id";

            int rowsAffected = await _session.Connection.ExecuteAsync(sql, new { id });
            return rowsAffected > 0;
        }
    }
}
