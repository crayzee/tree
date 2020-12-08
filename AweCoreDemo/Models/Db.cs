using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Microsoft.EntityFrameworkCore;

namespace AweCoreDemo.Models
{
    
    public static class Db
    {       
        public static IList<Meal> Meals = new List<Meal>();
        public static IList<Category> Categories = new List<Category>();

        public static IEnumerable<TreeNode> TreeNodes => treeNodes.Values.Where(o => !o.IsDeleted);

        private static readonly ConcurrentDictionary<int, TreeNode> treeNodes = new ConcurrentDictionary<int, TreeNode>();

        private static int gid = 0;
        private static readonly object lockObj = new object();

        public static T Insert<T>(T o) where T : Entity
        {
            lock (lockObj)
            {
                
                o.Id = gid += 1;
            }

            o.DateCreated = DateTime.UtcNow;

            var dict = (ConcurrentDictionary<int, T>)Dict<T>();

            if (dict.TryAdd(o.Id, o))
            {
                return o;
            }

            throw new AwesomeDemoException("can not add new item");
        }

        public static T Get<T>(int? id) where T : Entity
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return Get<T>(id.Value);
        }

        public static Task<T> GetAsync<T>(int? id) where T : Entity
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return Task.Run(() => Get<T>(id.Value));
        }

        public static T Get<T>(int id) where T : Entity
        {
            T entity;
            var list = List<T>();
            if (list != null)
            {
                entity = ((IList<T>)list).SingleOrDefault(o => o.Id == id);
            }
            else
            {
                var dict = (ConcurrentDictionary<int, T>)Dict<T>();
                dict.TryGetValue(id, out entity);
            }

            if (entity == null || entity.IsDeleted) throw new EntityMissingException("this item doesn't exist anymore");
            return entity;
        }

        public static IEnumerable<T> Set<T>() where T : Entity
        {
            IEnumerable<T> set;
            var list = List<T>();
            if (list != null)
            {
                set = ((IList<T>)list);
            }
            else
            {
                var dict = (ConcurrentDictionary<int, T>)Dict<T>();
                set = dict.Values;
            }

            return set;
        }

        public static void Update<T>(T o) where T : Entity
        {
            var entity = Get<T>(o.Id);
            entity.InjectFrom(o);
        }

        public static void Delete<T>(int id) where T : Entity
        {
            if (Set<T>().Count(o => !o.IsDeleted) < 10)
            {
                RestoreItems();
            }

            var ent = Get<T>(id);
            ent.DateDeleted = DateTime.UtcNow;
            ent.IsDeleted = true;
        }

       public static void RestoreItems()
        {
            Action<IEnumerable<Entity>> restore = entities =>
            {
                foreach (var o in entities)
                {
                    if (o.IsDeleted)
                    {
                        o.IsDeleted = false;
                    }
                }
            };

            restore(treeNodes.Values);
        }

        private static object List<T>()
        {
            return List(typeof(T));
        }

        private static object List(Type type)
        {
            if (type == typeof(Meal)) return Meals;
            if (type == typeof(Category)) return Categories;
            return null;
        }

        private static object Dict<TEntity>() where TEntity : Entity
        {
            var type = typeof(TEntity);

            if (type == typeof(TreeNode)) return treeNodes;

            return null;
        }

        private static void add<T>(T o) where T : Category
        {
            //o.DateCreated = DateTime.UtcNow;
            //o.Id = Id;
            var list = (IList<T>)List<T>();
            list.Add(o);
        }

        static Db()
        {
            add(new Category { Id = 11, ParentId = 0, Level = 1, Name = "Директор" });

            add(new Category { Id = 21, ParentId = 11, Level = 2, Name = "Департамент маркетинга" });
            add(new Category { Id = 31, ParentId = 11, Level = 2, Name = "Финансовый департамент" });
            add(new Category { Id = 41, ParentId = 11, Level = 2, Name = "IT департамент" });

            add(new Category { Id = 51, ParentId = 31, Level = 3, Name = "Бухгалтерский отдел" });
            add(new Category { Id = 61, ParentId = 41,  Level = 3,Name = "QA отдел" });

            add(new Category { Id = 71, ParentId = 61, Level = 4, Name = "Сектор Net Core" });  
            add(new Category { Id = 111, ParentId = 61, Level = 4, Name = "Сектор Python" });
            add(new Category { Id = 131, ParentId = 61, Level = 4, Name = "Сектор C Sharp" });

            add(new Category { Id = 331, ParentId = 21,  Level = 3,Name = "Отдел маркетинга" });  
            add(new Category { Id = 332, ParentId = 21,  Level = 3,Name = "Отдел аналитики" }); 
            add(new Category { Id = 333, ParentId = 21,  Level = 3,Name = "Отдел диджитал" }); 

            add(new Category { Id = 400, ParentId = 333,  Level = 4,Name = "Сектор Google Ads" }); 
            add(new Category { Id = 401, ParentId = 333,  Level = 4,Name = "Сектор Yandex Direct" }); 

            add(new Category { Id = 404, ParentId = 401,  Level = 5,Name = "Группа директологов" });

            //private MyWebApiContext db = new MyWebApiContext();

            var node = Insert(new TreeNode());
            var first_node = Categories.Where(p => p.ParentId == 0).ToList();
            node.Name = first_node[0].Name;

            FillNode(node, 2);
        }

        private static readonly Random R = new Random(); 

        private static void FillNode(TreeNode parent, int level)
        {
            var nodeCount = Categories.Where( o => o.Level == level).Count();
            var leafNames = Categories.Where( o => o.Level == level).ToList();           
            
            for (int i = 0; i < nodeCount; i++)            
            {
                var node = Insert(new TreeNode { Parent = parent });                            
                node.Id = leafNames[i].Id;                            
                node.Name = leafNames[i].Name;

                if ( level >= 3 && parent.Id != leafNames[i].ParentId)
                {
                    node.IsDeleted = true;
                }
                
                FillNode(node, level + 1);                                    
            }
        }

        private static T Rnd<T>(ICollection<T> list)
        {
            return list.ToArray()[R.Next(0, list.Count)];
        }
    }
}