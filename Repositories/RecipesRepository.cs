using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class RecipesRepository
  {

    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Recipe> GetAll()
    {
      string sql = @"
      SELECT * FROM recipes;
      ";
      return _db.Query<Recipe>(sql).ToList();
    }

    internal Recipe GetById(int recipeId)
    {
      string sql = @"
      SELECT
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a on r.creatorId = a.id
      WHERE r.id = @recipeId;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (r, a) =>
      {
        r.Writer = a;
        return r;
      }, new { recipeId }).FirstOrDefault();
    }

    internal Recipe Post(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO recipes(creatorId, name)
      VALUES(@CreatorId, @Name);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }

    internal List<Recipe> GetRecipesByAccount(string userId)
    {
      string sql = "SELECT * FROM recipes r WHERE r.creator.Id = @userId";
      return _db.Query<Recipe>(sql, new { userId }).ToList();
    }
    internal void RemoveRecipe(int recipeId)
    {
      string updateSql = @"
      UPDATE players
      SET
      recipeId = null
      WHERE recipeId = @recipeId;
      ";
      var updatedRows = _db.Execute(updateSql, new { recipeId });

      string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new { recipeId });
      if (affectedRows == 0)
      {
        throw new Exception("Sorry");
      }
    }
  }
}