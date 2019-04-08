using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole.EntityFrameworkPractice
{
    class EntityFrameworkPractice
    {
        static void Main()
        {
            //AsyncUpdateData();

            Console.ReadLine();
        }

        public void AddData()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EntityDB>());

            EntityDB db = new EntityDB();

            for (int i = 0; i < 5; i++)
            {
                db.Experience.Add(new Experience
                {
                    ID = i,
                    Exp = i,
                    UserLevel = (short)i,
                    //CreatedDate = DateTime.Now,
                    LastUpdatedUserID = (short)i,
                    LastUpdatedDate = DateTime.Now,
                    CreatedUserID = 999
                });
            }

            db.SaveChanges();

            var d = db.Experience.ToList();
            foreach (var t in d)
                Console.WriteLine(t.ID);
            Console.ReadKey();


        }

        public static async Task AsyncAddData()
        {
            using (var db = new EntityDB())
            {

                for (int i = 0; i < 5; i++)
                {
                    db.Experience.Add(new Experience
                    {
                        ID = i,
                        Exp = i,
                        UserLevel = (short)i,
                        CreatedDate = DateTime.Now,
                        LastUpdatedUserID = (short)i,
                        LastUpdatedDate = DateTime.Now,
                        CreatedUserID = 999
                    });
                }
                Console.WriteLine("Calling SaveChanges.");
                await db.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");

                // Query for all blogs ordered by name 
                Console.WriteLine("Executing query.");
                var blogs = await (from b in db.Experience
                                   orderby b.ID
                                   select b).ToListAsync();

                // Write all blogs out to Console 
                Console.WriteLine("Query completed with following results:");
                foreach (var blog in blogs)
                {
                    Console.WriteLine(" - " + blog.ID);
                }
            }
        }

        public static async Task AsyncUpdateData()
        {
            using (var db = new EntityDB())
            {
                var blogs = await (from b in db.Experience
                                   orderby b.ID
                                   select b).ToListAsync();

                // Write all blogs out to Console 
                Console.WriteLine("Query completed with following results:");
                foreach (var blog in blogs)
                {
                    blog.LastUpdatedDate = DateTime.Now;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
