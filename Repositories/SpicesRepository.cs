using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Repositories
{
  public class SpicesRepository
  {
    private readonly IDbConnection _db;
    public SpicesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Spice> GetAll()
    {
      string sql = "SELECT * FROM spices";
      return _db.Query<Spice>(sql).ToList();
    }

    internal Spice GetById(int spiceId)
    {
      string sql = @"
      SELECT
      s.*,
      r.*,
      a.*
      FROM spices s
      LEFT JOIN recipes r on r.id = s.recipeId
      LEFT JOIN accounts a on a.id = r.creatorId
      WHERE s.id = @spiceId;
      ";
      return _db.Query<Spice, Recipe, Account, Spice>(sql, (s, r, a) =>
      {
        if (r != null)
        {
          r.Writer = a;
          s.Recipe = r;
        }
        return s;
      }, new { spiceId }).FirstOrDefault();
    }

    internal List<Spice> GetSpicesByRecipe(int recipeId)
    {
      string sql = @"
      SELECT * FROM spices s WHERE s.recipeId = @recipeId
      ";
      return _db.Query<Spice>(sql, new { recipeId }).ToList();
    }

    internal ActionResult<Spice> Update(Spice foundSpice)
    {
      string sql = @"
      UPDATE spices
      SET
      recipeId = @RecipeId
      WHERE id = @Id;
      ";
      var rowsAffected = _db.Execute(sql, foundSpice);
      if (rowsAffected == 0)
      {
        throw new Exception("Failure");
      }
      return foundSpice;
    }
    internal object GetSpicesByAccount(string userId)
    {
      string sql = @"
      SELECT
      s.*,
      r.*
      FROM recipes r
      JOIN spices s on s.recipeId = r.id
      WHERE r.creatorId = @userId;
      ";
      return _db.Query<Spice, Recipe, Spice>(sql, (s, r) =>
      {
        s.Recipe = r;
        return s;
      }, new { userId }).ToList();
    }
  }

}