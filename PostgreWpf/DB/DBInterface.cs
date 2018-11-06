 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreWpf
{
    class DBInterface
    {
        DBDriver dbDriver;
        public DBInterface(String server, String port, String userId, String password, String db)
        {
            dbDriver = new DBDriver("localhost", "5432", "postgres", "123qwe", "postgres");
        }

        public void CreateFilmsTable()
        {
            String createQuery = "CREATE TABLE films(" +
                "fid SERIAL,title varchar(255) NOT NULL,budget integer, script text, did integer NOT NULL," +
                "PRIMARY KEY(fid)," +
                "FOREIGN KEY(did) REFERENCES directors(did) ON DELETE CASCADE" +
                ")";
            dbDriver.ExecuteQuery(createQuery);
        }

        public void CreateActorsTable()
        {
            String createQuery = "CREATE TABLE actors(aid SERIAL,firstname varchar(255),secondname varchar(255), experience integer, PRIMARY KEY(aid))";
            dbDriver.ExecuteQuery(createQuery);
        }

        public void CreateDirectorsTable()
        {
            String createQuery = "CREATE TABLE directors(did SERIAL,firstname varchar(255),secondname varchar(255), isCertified boolean, PRIMARY KEY(did))";
            dbDriver.ExecuteQuery(createQuery);
        }

        public void CreateFilmstoActorsTable()
        {
            String createQuery = "CREATE TABLE rel(fid integer, aid integer)";
            dbDriver.ExecuteQuery(createQuery);
        }

        public DataTable GetAllFilms()
        {
            String getQuery = "SELECT * FROM films";
            return dbDriver.ExecuteQueryAndReturn(getQuery);
        }

        public DataTable GetAllActors()
        {
            String getQuery = "SELECT * FROM actors";
            return dbDriver.ExecuteQueryAndReturn(getQuery);
        }

        public DataTable GetAllDirectors()
        {
            String getQuery = "SELECT * FROM directors";
            return dbDriver.ExecuteQueryAndReturn(getQuery);
        }

        public void InsertFilm(Film film)
        {      
            String insertQuery = String.Format(
                "INSERT INTO films(fid,title,budget,script,did) VALUES(DEFAULT,'{0}',{1},'{2}',{3})",
                film.Title, film.Budget, film.Script, film.Did
                );
            dbDriver.ExecuteQuery(insertQuery);
        }

        public void InsertActor(Actor actor)
        {
            String insertQuery = String.Format(
                "INSERT INTO actors(aid,firstname,secondname,experience) VALUES(DEFAULT,'{0}','{1}',{2})",
                actor.FirstName, actor.SecondName, actor.Experience
                );
            dbDriver.ExecuteQuery(insertQuery);
        }

        public void InsertDirector(Director director)
        {
            String insertQuery = String.Format(
                "INSERT INTO directors(did,firstname,secondname, isCertified) VALUES(DEFAULT,'{0}','{1}',{2})",
                director.FirstName, director.SecondName, director.IsCertified
                );
            Console.WriteLine(insertQuery);
            dbDriver.ExecuteQuery(insertQuery);
        }

        public void DeleteFilm(int id)
        {
            String deleteQuery = String.Format(
                            "DELETE FROM films WHERE " +
                            "fid = {0}", id
                            );
            dbDriver.ExecuteQuery(deleteQuery);
        }

        public void DeleteActor(int id)
        {
            String deleteQuery = String.Format(
                            "DELETE FROM actors WHERE " +
                            "aid = {0}", id
                            );
            dbDriver.ExecuteQuery(deleteQuery);
        }

        public void DeleteDirector(int id)
        {
            String deleteQuery = String.Format(
                "DELETE FROM directors WHERE " +
                "did = {0}", id
                );
            dbDriver.ExecuteQuery(deleteQuery);
        }

        public void UpdateFilm(Film film)
        {
            Console.WriteLine(film.Fid);
            String updateQuery = String.Format("UPDATE films SET title='{0}',budget={1},script='{2}',did={3} WHERE fid={4}", 
                film.Title, film.Budget, film.Script, film.Did, film.Fid);
            dbDriver.ExecuteQuery(updateQuery);
        }

        public void UpdateActor(Actor actor)
        {
            String updateQuery = String.Format("UPDATE actors SET firstname='{0}',secondname='{1}',experience={2}" +
                " WHERE aid={3}",
                actor.FirstName, actor.SecondName, actor.Experience, actor.Aid);
            dbDriver.ExecuteQuery(updateQuery);
        }

        public void UpdateDirector(Director director)
        {
            String updateQuery = String.Format("UPDATE directors SET firstname='{0}',secondname='{1}', isCertified={2}" +
                " WHERE did={3}",
                director.FirstName, director.SecondName,director.IsCertified, director.Did);
            dbDriver.ExecuteQuery(updateQuery);
        }

        public DataTable SearchScript(String searchRequest)
        {
            String searchQuery = String.Format("SELECT * from films WHERE (to_tsvector(script) " +
                "@@ to_tsquery('{0}'))", searchRequest);
            return dbDriver.ExecuteQueryAndReturn(searchQuery);
        }

        public DataTable SearchTitle_Director(int budget, String director)
        {
            String joinQuery = String.Format("SELECT * FROM (SELECT * FROM films WHERE budget='{0}') films JOIN" +
                " (SELECT * FROM directors WHERE isCertified='{1}') directors ON films.did=directors.did",
                budget,director);
            return dbDriver.ExecuteQueryAndReturn(joinQuery);
        }

        public void DropFilms()
        {
            String dropQuery = "DROP TABLE films";
            dbDriver.ExecuteQuery(dropQuery);
        }
        public void DropActors()
        {
            String dropQuery = "DROP TABLE actors";
            dbDriver.ExecuteQuery(dropQuery);
        }
        public void DropDirectors()
        {
            String dropQuery = "DROP TABLE directors";
            dbDriver.ExecuteQuery(dropQuery);
        }
        public void DropRels()
        {
            String dropQuery = "DROP TABLE rel";
            dbDriver.ExecuteQuery(dropQuery);
        }
        public void Close()
        {
            dbDriver.CloseConnection();
        }

    }
}
