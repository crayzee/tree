using System;
using System.Collections.Generic;
using System.Linq;

namespace AweCoreDemo.Models
{
    public class Entity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateDeleted { get; set; }
    }



    public class Category
    {
        public int Id {get;set;}
        public int ParentId {get;set;}
        //public ParentCategory ParentCategory {get;set;}
        public int Level {get;set;}
        public string Name { get; set; }
    }

    public class Meal : Entity
    {
        public Category Category { get; set; }
        public int ParentId {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class TreeNode : Entity
    {
        public string Name { get; set; }

        public TreeNode Parent { get; set; }
    }

    

   

    public class ParentCategory : Entity
    {
        public Category Category { get; set; }

    }

    
}