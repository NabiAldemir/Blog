﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using System.Reflection.Metadata;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public List<Blog> GetBlogListWithCategory()
        {
           return _blogDal.GetListWithCategory();
        }
        public List<Blog> GetBlogListWithCategoryWriter(int id)
        {
            return _blogDal.GetListWithCategoryByWriter(id);
        }
        public Blog TGetById(int id)
        {
            return _blogDal.GetById(id);
        }
        public List<Blog> GetBlogByID(int id)
        {
            return _blogDal.GetListAll(x => x.BlogID == id);
        }

        public List<Blog> GetList()
        {
            return _blogDal.GetListAll();
        }
        public List<Blog> GetLast4Image()
        {
            return _blogDal.GetListAll().Take(4).ToList();
        }
        public List<Blog> GetLast3Blog()
        {
            return _blogDal.GetListAll().Take(3).ToList();
        }
        public List<Blog> GetBlogListForWriter(int id)
        {
            return _blogDal.GetListAll(x=>x.WriterID == id);
        }

        public void TAdd(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }
    }
}
